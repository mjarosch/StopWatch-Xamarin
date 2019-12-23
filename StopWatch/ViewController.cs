using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Stopwatch
{
    public partial class ViewController : UIViewController, IUITableViewDataSource
    {
        int _time = 0;
        int _lastLapTime = 0;
        NSTimer _timer;
        List<int> _laps = new List<int>();

        public ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        partial void StartButton_TouchUpInside(RoundButton sender)
        {
            ResetButton.Hidden = true;
            LapButton.Hidden = false;
            LapButton.Enabled = true;
            StartButton.Hidden = true;
            StopButton.Hidden = false;

            _timer = NSTimer.CreateScheduledTimer(0.01, true, _ =>
            {
                _time += 1;
                UpdateLabel();
            });
            NSRunLoop.Current.AddTimer(_timer, NSRunLoopMode.Common);
        }

        partial void StopButton_TouchUpInside(RoundButton sender)
        {
            LapButton.Hidden = true;
            ResetButton.Hidden = false;
            StopButton.Hidden = true;
            StartButton.Hidden = false;

            _timer.Invalidate();
            _timer = null;
        }

        partial void ResetButton_TouchUpInside(RoundButton sender)
        {
            ResetButton.Hidden = true;
            LapButton.Hidden = false;
            LapButton.Enabled = false;

            _time = 0;
            _lastLapTime = 0;
            _laps.Clear();

            LapsTable.ReloadData();

            UpdateLabel();
        }

        partial void LapButton_TouchUpInside(RoundButton sender)
        {
            _laps.Add(_time - _lastLapTime);
            _lastLapTime = _time;


            LapsTable.ReloadData();
        }

        private void UpdateLabel()
        {
            TimerDisplay.Text = FormatTime(_time);
        }

        private string FormatTime(int time)
        {
            var hundredth = time % 100;
            var seconds = (time / 100) % 60;
            var minutes = (time / 6000) % 60;
            var hours = time / 360000;
            if (hours > 0) {
                return $"{hours}:{minutes:00}:{seconds:00}.{hundredth:00}";
            } else {
                return $"{minutes:00}:{seconds:00}.{hundredth:00}";
            }
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return _laps.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("ContentCell", indexPath);

            cell.TextLabel.Text = $"Lap {_laps.Count - indexPath.Row}";
            cell.DetailTextLabel.Text = FormatTime(_laps[_laps.Count - indexPath.Row - 1]);

            return cell;
        }
    }
}
