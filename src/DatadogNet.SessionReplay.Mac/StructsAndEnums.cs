using ObjCRuntime;

namespace DatadogSessionReplay
{
	[Native]
	public enum DDImagePrivacyLevel : long
	{
		MaskNonBundledOnly = 0,
		MaskAll = 1,
		MaskNone = 2
	}

	[Native]
	public enum DDImagePrivacyLevelOverride : long
	{
		None = 0,
		MaskNone = 1,
		MaskNonBundledOnly = 2,
		MaskAll = 3
	}

	[Native]
	public enum DDTextAndInputPrivacyLevel : long
	{
		MaskSensitiveInputs = 0,
		MaskAllInputs = 1,
		MaskAll = 2
	}

	[Native]
	public enum DDTextAndInputPrivacyLevelOverride : long
	{
		None = 0,
		MaskSensitiveInputs = 1,
		MaskAllInputs = 2,
		MaskAll = 3
	}

	[Native]
	public enum DDTouchPrivacyLevel : long
	{
		Show = 0,
		Hide = 1
	}

	[Native]
	public enum DDTouchPrivacyLevelOverride : long
	{
		None = 0,
		Show = 1,
		Hide = 2
	}
}
