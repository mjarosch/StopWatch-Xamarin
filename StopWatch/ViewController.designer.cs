// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Stopwatch
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Stopwatch.RoundButton LapButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView LapsTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Stopwatch.RoundButton ResetButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Stopwatch.RoundButton StartButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Stopwatch.RoundButton StopButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TimerDisplay { get; set; }

        [Action ("LapButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void LapButton_TouchUpInside (Stopwatch.RoundButton sender);

        [Action ("ResetButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ResetButton_TouchUpInside (Stopwatch.RoundButton sender);

        [Action ("StartButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StartButton_TouchUpInside (Stopwatch.RoundButton sender);

        [Action ("StopButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StopButton_TouchUpInside (Stopwatch.RoundButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (LapButton != null) {
                LapButton.Dispose ();
                LapButton = null;
            }

            if (LapsTable != null) {
                LapsTable.Dispose ();
                LapsTable = null;
            }

            if (ResetButton != null) {
                ResetButton.Dispose ();
                ResetButton = null;
            }

            if (StartButton != null) {
                StartButton.Dispose ();
                StartButton = null;
            }

            if (StopButton != null) {
                StopButton.Dispose ();
                StopButton = null;
            }

            if (TimerDisplay != null) {
                TimerDisplay.Dispose ();
                TimerDisplay = null;
            }
        }
    }
}