// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MvvmCross.Plugins.Validation.Demo.Touch.Views
{
	[Register ("FirstView")]
	partial class FirstView
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSubmit { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LblAge { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel LblName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TxtAge { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField TxtName { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnSubmit != null) {
				btnSubmit.Dispose ();
				btnSubmit = null;
			}
			if (LblAge != null) {
				LblAge.Dispose ();
				LblAge = null;
			}
			if (LblName != null) {
				LblName.Dispose ();
				LblName = null;
			}
			if (TxtAge != null) {
				TxtAge.Dispose ();
				TxtAge = null;
			}
			if (TxtName != null) {
				TxtName.Dispose ();
				TxtName = null;
			}
		}
	}
}
