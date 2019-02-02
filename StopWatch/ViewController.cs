using System;
using Foundation;
using UIKit;

namespace StopWatch
{
    public partial class ViewController : UIViewController
    {
        int _time;
        NSTimer _timer;

        partial void Reset_UpInside(UIButton sender)
        {
            _time = 0;
            Reset.Enabled = false;
            UpdateLabel();
        }

        partial void StartStop_UpInside(UIButton sender)
        {
            Reset.Enabled = true;

            if (_timer == null)
            {
                StartStop.SetTitle("Stop", UIControlState.Normal);
                _timer = NSTimer.CreateRepeatingScheduledTimer(0.01, Timer_Tick);
            }
            else
            {
                StartStop.SetTitle("Start", UIControlState.Normal);
                _timer.Invalidate();
                _timer = null;
            }
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void Timer_Tick(NSTimer timer)
        {
            _time++;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            var hundredth = _time % 100;
            var seconds = (_time / 100) % 60;
            var minutes = (_time / 6000) % 60;
            var hours = _time / 360000;
            timerDisplay.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, hundredth);
        }
    }
}
