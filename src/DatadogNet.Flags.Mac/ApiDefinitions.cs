// DatadogFlags exposes no Objective-C API in dd-sdk-ios 3.14.0.
//
// Every type in it is Swift-only, so there is nothing for Objective Sharpie to project and nothing
// a C# caller can invoke. The package exists to ship the native framework - so that the module is
// present if another Datadog framework starts linking it, and so that adding the API later is a
// version bump rather than a new package id.
//
// This file is deliberately empty apart from the namespace: a binding project does not build
// without an ObjcBindingApiDefinition.

namespace DatadogFlags
{
}
