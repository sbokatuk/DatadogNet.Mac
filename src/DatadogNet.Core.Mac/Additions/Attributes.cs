// Nullable annotations are enabled per file rather than for the project: the generated
// binding sources are not written against a nullable context, and switching the whole
// project over would bury real warnings here under hundreds of generated ones.
#nullable enable

using System;
using System.Collections.Generic;
using Foundation;

namespace DatadogCore
{
	/// <summary>
	/// Converts C# attribute dictionaries into the <c>NSDictionary&lt;NSString, NSObject&gt;</c>
	/// the Datadog API takes.
	/// </summary>
	/// <remarks>
	/// Lives in DatadogCore because RUM, Logs and Trace all need it and all depend on Core.
/// <para>
/// Almost every RUM and Logs call ends in an attributes parameter, and the bound signature
	/// requires one even when there is nothing to attach - so the raw binding forces
	/// <c>new NSDictionary&lt;NSString, NSObject&gt;()</c> at hundreds of call sites, and forces
	/// callers to hand-wrap every value in an <c>NSObject</c>. These helpers are what the
	/// convenience overloads across these packages are built on.
	/// </para>
	/// </remarks>
	public static class DatadogAttributes
	{
		/// <summary>
		/// The empty attribute dictionary, allocated once.
		/// </summary>
		/// <remarks>
		/// Shared rather than allocated per call. It is never handed to callers and the Datadog SDK
		/// copies attributes into its own event payloads rather than retaining this instance, so
		/// there is nothing to mutate it.
		/// </remarks>
		public static NSDictionary<NSString, NSObject> Empty { get; } = new NSDictionary<NSString, NSObject> ();

		/// <summary>Converts a C# dictionary to the Objective-C form, mapping null to empty.</summary>
		/// <param name="attributes">The attributes, or <see langword="null"/> for none.</param>
		/// <exception cref="ArgumentException">
		/// A value has no Objective-C representation. Supported are the primitive numeric types,
		/// <see cref="bool"/>, <see cref="string"/>, <see cref="DateTime"/>, any
		/// <see cref="NSObject"/>, and arrays or nested dictionaries of those.
		/// </exception>
		public static NSDictionary<NSString, NSObject> From (IReadOnlyDictionary<string, object?>? attributes)
		{
			if (attributes is null || attributes.Count == 0)
				return Empty;

			var keys = new NSString[attributes.Count];
			var values = new NSObject[attributes.Count];

			var index = 0;
			foreach (var pair in attributes) {
				keys[index] = new NSString (pair.Key);
				values[index] = ToNSObject (pair.Value, pair.Key);
				index++;
			}

			return NSDictionary<NSString, NSObject>.FromObjectsAndKeys (values, keys, keys.Length);
		}

		/// <summary>Converts a sequence of pairs to the Objective-C form.</summary>
		/// <remarks>
		/// Lets a call site read <c>Attributes (("cart.size", 3), ("checkout.step", "payment"))</c>
		/// without building a dictionary first.
		/// </remarks>
		public static NSDictionary<NSString, NSObject> From (params (string Key, object? Value)[]? attributes)
		{
			if (attributes is null || attributes.Length == 0)
				return Empty;

			var keys = new NSString[attributes.Length];
			var values = new NSObject[attributes.Length];

			for (var index = 0; index < attributes.Length; index++) {
				keys[index] = new NSString (attributes[index].Key);
				values[index] = ToNSObject (attributes[index].Value, attributes[index].Key);
			}

			return NSDictionary<NSString, NSObject>.FromObjectsAndKeys (values, keys, keys.Length);
		}

		/// <summary>
		/// Converts a single value into the <see cref="NSObject"/> the Datadog SDK expects.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="key">
		/// The attribute name. Used only to name the value in any error, so that a rejected
		/// attribute can be found without guessing which one it was.
		/// </param>
		/// <exception cref="ArgumentException">The value has no Objective-C representation.</exception>
		/// <remarks>
		/// Several members take a bare value rather than a dictionary —
		/// <c>DDRUMMonitor.AddAttributeForKey</c>, <c>AddFeatureFlagEvaluationWithName</c>,
		/// <c>AddViewAttributeForKey</c>, <c>DDLogger.AddAttributeForKey</c> — and they all need the
		/// same conversion <see cref="From(IReadOnlyDictionary{string, object})"/> applies per
		/// entry. Without this a caller either hand-wraps the value, which is what
		/// <see cref="From(IReadOnlyDictionary{string, object})"/> exists to avoid, or round-trips a
		/// one-element dictionary to get at the result.
		/// </remarks>
		public static NSObject ToNSObject (object? value, string key)
		{
			// NSNull rather than skipping the key: "this attribute was explicitly empty" and "this
			// attribute was not set" are different things in a RUM event, and dropping the key
			// would silently turn the first into the second.
			return value switch {
				null => NSNull.Null,
				NSObject native => native,
				string text => new NSString (text),
				bool flag => new NSNumber (flag),
				int number => new NSNumber (number),
				long number => new NSNumber (number),
				short number => new NSNumber (number),
				byte number => new NSNumber (number),
				uint number => new NSNumber (number),
				ulong number => new NSNumber (number),
				ushort number => new NSNumber (number),
				sbyte number => new NSNumber (number),
				float number => new NSNumber (number),
				double number => new NSNumber (number),
				// NSNumber has no decimal overload, and narrowing to double would lose precision
				// silently on a value someone chose decimal for on purpose.
				decimal number => new NSDecimalNumber (number.ToString (System.Globalization.CultureInfo.InvariantCulture)),
				DateTime timestamp => (NSDate) timestamp.ToUniversalTime (),
				DateTimeOffset timestamp => (NSDate) timestamp.UtcDateTime,
				Guid identifier => new NSString (identifier.ToString ()),
				Enum enumeration => new NSString (enumeration.ToString ()),
				IReadOnlyDictionary<string, object?> nested => From (nested),
				System.Collections.IEnumerable sequence => ToArray (sequence, key),
				_ => throw new ArgumentException (
					$"Attribute '{key}' is a {value.GetType ()}, which has no Objective-C representation. " +
					"Convert it to a string, a number, a bool, a DateTime, or an NSObject first.",
					nameof (value)),
			};
		}

		static NSObject ToArray (System.Collections.IEnumerable sequence, string key)
		{
			var items = new List<NSObject> ();
			foreach (var item in sequence)
				items.Add (ToNSObject (item, key));

			return NSArray.FromNSObjects (items.ToArray ());
		}
	}
}
