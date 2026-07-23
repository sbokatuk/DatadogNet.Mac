// Nullable annotations are enabled per file rather than for the project: the generated binding
// sources are not written against a nullable context, and switching the whole project over would
// bury real warnings here under hundreds of generated ones.
#nullable enable

using System;
using System.Collections.Generic;
using Foundation;

namespace DatadogCore
{
	public partial class DDDatadog
	{
		/// <summary>Sets the current user, so events can be attributed to them.</summary>
		/// <param name="userId">Your identifier for the user. Required from dd-sdk-ios 2.24.0.</param>
		/// <param name="name">The user's display name, if you have one.</param>
		/// <param name="email">The user's email address, if you have one.</param>
		/// <param name="extraInfo">Any further attributes to attach to the user.</param>
		/// <remarks>
		/// The bound <c>SetUserInfoWithUserId</c> takes all four arguments and no defaults,
		/// so setting only an id means passing two nulls and an empty dictionary.
		/// </remarks>
		public static void SetUserInfo (
			string userId,
			string? name = null,
			string? email = null,
			IReadOnlyDictionary<string, object?>? extraInfo = null)
		{
			if (userId is null)
				throw new ArgumentNullException (nameof (userId));

			SetUserInfoWithUserId (userId, name, email, DatadogAttributes.From (extraInfo));
		}

		/// <summary>Sets the account the user belongs to.</summary>
		/// <remarks>Added upstream in 2.29.0. Account details propagate to Logs, RUM and Trace.</remarks>
		public static void SetAccountInfo (
			string accountId,
			string? name = null,
			IReadOnlyDictionary<string, object?>? extraInfo = null)
		{
			if (accountId is null)
				throw new ArgumentNullException (nameof (accountId));

			SetAccountInfoWithAccountId (accountId, name, DatadogAttributes.From (extraInfo));
		}

		/// <summary>Adds attributes to the current user without replacing the ones already set.</summary>
		public static void AddUserExtraInfo (IReadOnlyDictionary<string, object?> extraInfo)
		{
			if (extraInfo is null)
				throw new ArgumentNullException (nameof (extraInfo));

			AddUserExtraInfo (DatadogAttributes.From (extraInfo));
		}

		/// <summary>Sets tracking consent from a C# enum.</summary>
		/// <remarks>
		/// <see cref="DDTrackingConsent"/> is bound as a class with three static factory methods
		/// rather than an enum, because that is how it crosses from Swift into Objective-C. That
		/// makes it awkward to store, compare or switch on, so <see cref="TrackingConsent"/>
		/// mirrors it as a real enum.
		/// </remarks>
		public static void SetTrackingConsent (TrackingConsent consent)
		{
			SetTrackingConsentWithConsent (consent switch {
				TrackingConsent.Granted => DDTrackingConsent.Granted (),
				TrackingConsent.NotGranted => DDTrackingConsent.NotGranted (),
				TrackingConsent.Pending => DDTrackingConsent.Pending (),
				_ => throw new ArgumentOutOfRangeException (nameof (consent), consent, "Unknown tracking consent."),
			});
		}
	}

	/// <summary>Whether the user has consented to data being collected and sent to Datadog.</summary>
	/// <remarks>
	/// A C# mirror of <see cref="DDTrackingConsent"/>, which is a class of static factory methods
	/// rather than an enum. See <see cref="DDDatadog.SetTrackingConsent"/>.
	/// </remarks>
	public enum TrackingConsent
	{
		/// <summary>Data is collected and sent to Datadog.</summary>
		Granted,

		/// <summary>No data is collected.</summary>
		NotGranted,

		/// <summary>
		/// Data is collected and held on the device, but not sent. It is sent if consent later
		/// becomes <see cref="Granted"/>, and discarded if it becomes <see cref="NotGranted"/>.
		/// </summary>
		Pending,
	}
}
