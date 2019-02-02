// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace StopWatch
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Reset { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StartStop { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel timerDisplay { get; set; }

        [Action ("Reset_UpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Reset_UpInside (UIKit.UIButton sender);

        [Action ("StartStop_UpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StartStop_UpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (Reset != null) {
                Reset.Dispose ();
                Reset = null;
            }

            if (StartStop != null) {
                StartStop.Dispose ();
                StartStop = null;
            }

            if (timerDisplay != null) {
                timerDisplay.Dispose ();
                timerDisplay = null;
            }
        }
    }
}