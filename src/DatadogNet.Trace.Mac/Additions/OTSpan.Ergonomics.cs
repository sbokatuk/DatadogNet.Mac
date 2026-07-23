// Nullable annotations are enabled per file rather than for the project: the generated binding
// sources are not written against a nullable context, and switching the whole project over would
// bury real warnings here under hundreds of generated ones.
#nullable enable

using System;
using System.Collections.Generic;
using System.Globalization;
using DatadogCore;
using Foundation;

namespace DatadogTrace
{
	/// <summary>Which propagation formats to write.</summary>
	/// <remarks>
	/// A flags enum of this repository's own, because the bound <c>DDTracingHeaderType</c> is an
	/// Objective-C class of static singletons rather than an enum — Swift's enum did not survive the
	/// projection — so it cannot be combined or compared in the way choosing several formats needs.
	/// </remarks>
	[Flags]
	public enum OTHeaderFormats
	{
		/// <summary>Datadog's own <c>x-datadog-*</c> headers.</summary>
		Datadog = 1,

		/// <summary>W3C Trace Context: <c>traceparent</c> and <c>tracestate</c>.</summary>
		TraceContext = 2,

		/// <summary>B3 single header.</summary>
		B3 = 4,

		/// <summary>B3 multiple headers.</summary>
		B3Multi = 8,
	}

	/// <summary>
	/// Reading a span's identity and describing its outcome, in a form C# can call directly.
	/// </summary>
	/// <remarks>
	/// Extension methods rather than a partial class, because <c>OTSpan</c> is a
	/// <c>[Protocol]</c> — the usable form is the <c>IOTSpan</c> interface, which has no partial
	/// declaration to extend.
	/// </remarks>
	public static class OTSpanExtensions
	{
		/// <summary>The Datadog-format headers the ids are read out of.</summary>
		/// <remarks>
		/// <c>OTSpanContext</c> declares nothing but <c>forEachBaggageItem</c> — there is no
		/// <c>traceID</c> or <c>spanID</c> on the protocol or on any bound type, and 3.x did not
		/// change that. Injecting into a Datadog-format writer and reading what comes back is the
		/// only route to them from Objective-C.
		/// </remarks>
		const string TraceIdHeader = "x-datadog-trace-id";

		const string SpanIdHeader = "x-datadog-parent-id";

		const string TagsHeader = "x-datadog-tags";

		/// <summary>The propagation tag holding the high 64 bits of a 128-bit trace id.</summary>
		const string HighOrderBitsTag = "_dd.p.tid";

		/// <summary>
		/// The trace id, rendered the way Datadog's own instrumentation renders it.
		/// </summary>
		/// <param name="span">The span.</param>
		/// <param name="tracer">The tracer that started it, usually <c>DDTracer.Shared ()</c>.</param>
		/// <returns>32 lowercase hexadecimal characters, or an empty string if there is no trace.</returns>
		/// <remarks>
		/// Matches <c>DatadogTraceId.toHexString()</c> on Android, which is what Datadog's own
		/// <c>DatadogInterceptor</c> writes as <c>_dd.trace_id</c> when it links a RUM resource to
		/// its APM trace. Anything else produces a string the backend will not correlate on.
		/// <para>
		/// The id arrives in two pieces and has to be reassembled: <c>x-datadog-trace-id</c> carries
		/// the low 64 bits in decimal, and the high 64 travel separately as <c>_dd.p.tid</c> inside
		/// <c>x-datadog-tags</c>. Reading only the former yields a decimal string that names half of
		/// a different-looking id — which is a real mistake that shipped in a consumer of this
		/// package before it was caught.
		/// </para>
		/// </remarks>
		public static string GetTraceId (this IOTSpan span, IOTTracer tracer)
		{
			var headers = InjectDatadogHeaders (span, tracer);

			headers.TryGetValue (TraceIdHeader, out var lowOrderBits);
			headers.TryGetValue (TagsHeader, out var tags);

			if (!ulong.TryParse (lowOrderBits?.Trim (), NumberStyles.None, CultureInfo.InvariantCulture, out var low))
				return string.Empty;

			// An absent _dd.p.tid means 128-bit ids are off, which is Android's DD64bTraceId case:
			// the high half is genuinely zero rather than unknown, and Datadog still pads it out to
			// the full 32 characters.
			var high = ReadHighOrderBits (tags);

			return high.ToString ("x16", CultureInfo.InvariantCulture)
				+ low.ToString ("x16", CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// The span id, rendered the way Datadog's own instrumentation renders it.
		/// </summary>
		/// <param name="span">The span.</param>
		/// <param name="tracer">The tracer that started it.</param>
		/// <returns>The decimal form, or an empty string if there is no trace.</returns>
		/// <remarks>
		/// Decimal, and deliberately not hexadecimal like <see cref="GetTraceId"/>. The asymmetry is
		/// Datadog's wire format: <c>_dd.span_id</c> is written as <c>String.valueOf(long)</c>.
		/// </remarks>
		public static string GetSpanId (this IOTSpan span, IOTTracer tracer)
		{
			InjectDatadogHeaders (span, tracer).TryGetValue (SpanIdHeader, out var spanId);

			return spanId ?? string.Empty;
		}

		/// <summary>
		/// Writes the trace headers for <paramref name="span"/> into a dictionary.
		/// </summary>
		/// <param name="span">The span to propagate.</param>
		/// <param name="tracer">The tracer that started it.</param>
		/// <param name="headerTypes">
		/// Which formats to write. Defaults to Datadog and W3C trace context, which is what most
		/// backends accept between them.
		/// </param>
		/// <remarks>
		/// The bound API needs a dance that is not obvious from the signatures: you construct a
		/// writer, hand it to <c>Inject</c> as though it were the carrier, and then read the headers
		/// back off the writer. There is also one writer type per format — unlike Android, where the
		/// formats are a property of the tracer and one call writes all of them — so several formats
		/// means several round trips, merged here.
		/// <para>
		/// <c>TraceContextInjection.All</c> so the headers still go out for a dropped trace, which is
		/// what lets the receiving service stitch the request together even when nothing is stored.
		/// </para>
		/// </remarks>
		public static IDictionary<string, string> InjectHeaders (
			this IOTSpan span,
			IOTTracer tracer,
			OTHeaderFormats headerTypes = OTHeaderFormats.Datadog | OTHeaderFormats.TraceContext)
		{
			if (span is null)
				throw new ArgumentNullException (nameof (span));
			if (tracer is null)
				throw new ArgumentNullException (nameof (tracer));

			var headers = new Dictionary<string, string> (StringComparer.OrdinalIgnoreCase);

			if (headerTypes.HasFlag (OTHeaderFormats.Datadog))
				Merge (headers, Write (span, tracer, new DDHTTPHeadersWriter (DDTraceContextInjection.All)));

			if (headerTypes.HasFlag (OTHeaderFormats.TraceContext))
				Merge (headers, Write (span, tracer, new DDW3CHTTPHeadersWriter (DDTraceContextInjection.All)));

			if (headerTypes.HasFlag (OTHeaderFormats.B3))
				Merge (headers, Write (span, tracer,
					new DDB3HTTPHeadersWriter (DDInjectEncoding.Single, DDTraceContextInjection.All)));

			if (headerTypes.HasFlag (OTHeaderFormats.B3Multi))
				Merge (headers, Write (span, tracer,
					new DDB3HTTPHeadersWriter (DDInjectEncoding.Multiple, DDTraceContextInjection.All)));

			return headers;
		}

		/// <summary>Marks the span as failed, from a .NET exception.</summary>
		/// <remarks>
		/// <c>SetErrorWithKind</c> takes the three fields separately, and a caller who passes only
		/// the message gets a span marked as an error with nothing in the APM error panel to act on.
		/// </remarks>
		public static void SetError (this IOTSpan span, Exception exception)
		{
			if (span is null)
				throw new ArgumentNullException (nameof (span));
			if (exception is null)
				throw new ArgumentNullException (nameof (exception));

			span.SetErrorWithKind (
				exception.GetType ().FullName ?? exception.GetType ().Name,
				exception.Message,
				exception.StackTrace);
		}

		/// <summary>Attaches a set of log fields to the span.</summary>
		/// <remarks>
		/// The generated overload takes an <see cref="NSDictionary"/>, so every call site otherwise
		/// repeats the conversion <see cref="DatadogAttributes"/> already does.
		/// </remarks>
		public static void Log (this IOTSpan span, IReadOnlyDictionary<string, object?> fields)
		{
			if (span is null)
				throw new ArgumentNullException (nameof (span));
			if (fields is null)
				throw new ArgumentNullException (nameof (fields));

			span.Log (DatadogAttributes.From (fields));
		}

		static IDictionary<string, string> InjectDatadogHeaders (IOTSpan span, IOTTracer tracer)
		{
			if (span is null)
				throw new ArgumentNullException (nameof (span));
			if (tracer is null)
				throw new ArgumentNullException (nameof (tracer));

			var headers = new Dictionary<string, string> (StringComparer.OrdinalIgnoreCase);

			Merge (headers, Write (span, tracer, new DDHTTPHeadersWriter (DDTraceContextInjection.All)));

			return headers;
		}

		static NSDictionary<NSString, NSString> Write (IOTSpan span, IOTTracer tracer, DDHTTPHeadersWriter writer)
		{
			tracer.Inject (span.Context, OT.FormatTextMap, writer, out _);
			return writer.TraceHeaderFields;
		}

		static NSDictionary<NSString, NSString> Write (IOTSpan span, IOTTracer tracer, DDW3CHTTPHeadersWriter writer)
		{
			tracer.Inject (span.Context, OT.FormatTextMap, writer, out _);
			return writer.TraceHeaderFields;
		}

		static NSDictionary<NSString, NSString> Write (IOTSpan span, IOTTracer tracer, DDB3HTTPHeadersWriter writer)
		{
			tracer.Inject (span.Context, OT.FormatTextMap, writer, out _);
			return writer.TraceHeaderFields;
		}

		static void Merge (IDictionary<string, string> headers, NSDictionary<NSString, NSString> fields)
		{
			foreach (var key in fields.Keys)
				headers[key.ToString ()] = fields[key].ToString ();
		}

		/// <summary>Reads <c>_dd.p.tid</c> out of the propagation tags.</summary>
		static ulong ReadHighOrderBits (string? tags)
		{
			if (string.IsNullOrEmpty (tags))
				return 0;

			foreach (var pair in tags!.Split (',')) {
				var separator = pair.IndexOf ('=');

				if (separator < 0 || pair.Substring (0, separator).Trim () != HighOrderBitsTag)
					continue;

				var value = pair.Substring (separator + 1).Trim ();

				// Datadog writes exactly sixteen hex characters. Anything else is a tag this does
				// not understand, and a half-parsed value would name a real-looking trace that
				// nothing ever reported - worse than reporting no high bits at all.
				return value.Length == 16
					&& ulong.TryParse (value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var high)
						? high
						: 0;
			}

			return 0;
		}
	}
}
