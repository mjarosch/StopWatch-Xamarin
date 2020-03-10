using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Stopwatch
{
    public class MainViewController : UIViewController, IUITableViewDataSource
    {
        private int _time = 0;
        private int _lapTime = 0;
        private NSTimer _timer;
        private List<int> _laps = new List<int>();

        // UI Elements
        private RoundButton _lapButton;
        private RoundButton _resetButton;
        private UIStackView _stackView;
        private RoundButton _startButton;
        private RoundButton _stopButton;
        private UILabel _timeLabel;
        private UITableView _lapsTable;

        public MainViewController()
        {
            //this.View.BackgroundColor = UIColor.Purple;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            BuildView();

            UpdateLabel();

            //_lapsTable.RegisterClassForCellReuse(typeof(UITableViewCell), "ContentCell");
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);

            _stackView.Axis = StackViewAxisForTraitCollection(TraitCollection);
        }

        private void BuildView()
        {
            _stackView = new UIStackView()
            {
                Alignment = UIStackViewAlignment.Fill,
                Axis = StackViewAxisForTraitCollection(TraitCollection),
                Distribution = UIStackViewDistribution.FillEqually,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            this.View.AddSubview(_stackView);

            _stackView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;
            _stackView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor).Active = true;
            _stackView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor).Active = true;
            _stackView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor).Active = true;

            _stackView.AddArrangedSubview(this.BuildTimerView());
            _stackView.AddArrangedSubview(this.BuildLapView());
        }

        private UIView BuildTimerView()
        {
            var view = new UIView()
            {
                //BackgroundColor = UIColor.Cyan,
            };

            _timeLabel = new UILabel()
            {
                AdjustsFontSizeToFitWidth = true,
                Font = UIFont.MonospacedDigitSystemFontOfSize(85, UIFontWeight.Thin),
                Lines = 1,
                MinimumScaleFactor = 0.5f,
                TextAlignment = UITextAlignment.Center,
                TextColor = UIColor.LightTextColor,
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            view.AddSubview(_timeLabel);

            _timeLabel.TopAnchor.ConstraintEqualTo(view.TopAnchor).Active = true;
            _timeLabel.BottomAnchor.ConstraintEqualTo(view.BottomAnchor, -60).Active = true;
            _timeLabel.LeadingAnchor.ConstraintEqualTo(view.LeadingAnchor).Active = true;
            _timeLabel.TrailingAnchor.ConstraintEqualTo(view.TrailingAnchor).Active = true;

            _startButton = CreateButton(view, "Start", UIColor.FromRGB(81, 206, 105), UIColor.FromRGB(32, 88, 37), UIColor.FromRGB(24, 52, 31));
            _startButton.TrailingAnchor.ConstraintEqualTo(view.TrailingAnchor, -16).Active = true;

            _stopButton = CreateButton(view, "Stop", UIColor.FromRGB(254, 50, 25), UIColor.FromRGB(88, 38, 31), UIColor.FromRGB(52, 30, 36));
            _stopButton.TrailingAnchor.ConstraintEqualTo(view.TrailingAnchor, -16).Active = true;

            _lapButton  = CreateButton(view, "Lap", UIColor.LightTextColor, UIColor.LightGray, UIColor.DarkGray);
            _lapButton.LeadingAnchor.ConstraintEqualTo(view.LeadingAnchor, 16).Active = true;

            _resetButton = CreateButton(view, "Reset", UIColor.LightTextColor, UIColor.LightGray, UIColor.DarkGray);
            _resetButton.LeadingAnchor.ConstraintEqualTo(view.LeadingAnchor, 16).Active = true;

            _stopButton.WidthAnchor.ConstraintEqualTo(_startButton.WidthAnchor).Active = true;
            _lapButton.WidthAnchor.ConstraintEqualTo(_startButton.WidthAnchor).Active = true;
            _resetButton.WidthAnchor.ConstraintEqualTo(_startButton.WidthAnchor).Active = true;

            _lapButton.SetFillColor(UIColor.FromRGB(21, 21, 21), UIControlState.Disabled);

            _lapButton.Hidden = false;
            _lapButton.Enabled = false;
            _resetButton.Hidden = true;
            _stopButton.Hidden = true;

            _startButton.TouchUpInside += StartButton_TouchUpInside;
            _stopButton.TouchUpInside += StopButton_TouchUpInside;
            _lapButton.TouchUpInside += LapButton_TouchUpInside;
            _resetButton.TouchUpInside += ResetButton_TouchUpInside;

            return view;
        }

        private UIView BuildLapView()
        {
            _lapsTable = new UITableView()
            {
                BackgroundColor = UIColor.Black,
                DataSource = this,
                SeparatorColor = UIColor.FromRGB(64, 64, 64),
            };

            return _lapsTable;
        }

        private void StartButton_TouchUpInside(object sender, EventArgs e)
        {
            _resetButton.Hidden = true;
            _lapButton.Hidden = false;
            _lapButton.Enabled = true;
            _startButton.Hidden = true;
            _stopButton.Hidden = false;

            if (_laps.Count == 0)
            {
                _laps.Add(0);
                _lapsTable.ReloadData();
            }

            _timer = NSTimer.CreateScheduledTimer(0.01, true, _ =>
            {
                _time += 1;
                _lapTime += 1;
                _laps[_laps.Count - 1] = _lapTime;

                UpdateLabel();
            });
            NSRunLoop.Current.AddTimer(_timer, NSRunLoopMode.Common);
        }

        private void StopButton_TouchUpInside(object sender, EventArgs e)
        {
            _lapButton.Hidden = true;
            _resetButton.Hidden = false;
            _stopButton.Hidden = true;
            _startButton.Hidden = false;

            _timer.Invalidate();
            _timer = null;
        }

        private void ResetButton_TouchUpInside(object sender, EventArgs e)
        {
            _resetButton.Hidden = true;
            _lapButton.Hidden = false;
            _lapButton.Enabled = false;

            _time = 0;
            _lapTime = 0;
            _laps.Clear();

            _lapsTable.ReloadData();

            UpdateLabel();
        }

        private void LapButton_TouchUpInside(object sender, EventArgs e)
        {
            _laps[_laps.Count - 1] = _lapTime;
            _laps.Add(0);
            _lapTime = 0;
            _lapsTable.ReloadData();
        }

        private RoundButton CreateButton(UIView container, string titleText, UIColor titleColor, UIColor normalFillColor, UIColor hightlightedFillColor)
        {
            var button = new RoundButton()
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            button.TitleLabel.Font = UIFont.SystemFontOfSize(17);
            button.SetTitle(titleText, UIControlState.Normal);
            button.SetTitleColor(titleColor, UIControlState.Normal);
            button.SetFillColor(normalFillColor, UIControlState.Normal);
            button.SetFillColor(hightlightedFillColor, UIControlState.Highlighted);
            container.AddSubview(button);

            button.WidthAnchor.ConstraintEqualTo(button.HeightAnchor).Active = true;
            button.BottomAnchor.ConstraintEqualTo(container.BottomAnchor).Active = true;

            return button;
        }

        private UILayoutConstraintAxis StackViewAxisForTraitCollection(UITraitCollection traitCollection)
        {
            return TraitCollection.VerticalSizeClass == UIUserInterfaceSizeClass.Compact ? UILayoutConstraintAxis.Horizontal : UILayoutConstraintAxis.Vertical;
        }

        private void UpdateLabel()
        {
            _timeLabel.Text = FormatTime(_time);
            var cell = _lapsTable.CellAt(NSIndexPath.FromRowSection(0, 0));
            if (cell != null)
            {
                cell.DetailTextLabel.Text = FormatTime(_lapTime);
            }
        }

        private string FormatTime(int time)
        {
            var hundredth = time % 100;
            var seconds = (time / 100) % 60;
            var minutes = (time / 6000) % 60;
            var hours = time / 360000;
            if (hours > 0)
            {
                return $"{hours}:{minutes:00}:{seconds:00}.{hundredth:00}";
            }
            else
            {
                return $"{minutes:00}:{seconds:00}.{hundredth:00}";
            }
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            return _laps.Count;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("ContentCell");
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, "ContentCell")
                {
                    BackgroundColor = UIColor.Black,
                };
                cell.TextLabel.TextColor = UIColor.LightTextColor;
                cell.DetailTextLabel.Font = UIFont.MonospacedDigitSystemFontOfSize(cell.DetailTextLabel.Font.PointSize, UIFontWeight.Regular);
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            }

            cell.TextLabel.Text = $"Lap {_laps.Count - indexPath.Row}";
            cell.DetailTextLabel.Text = FormatTime(_laps[_laps.Count - indexPath.Row - 1]);

            return cell;
        }
    }
}
