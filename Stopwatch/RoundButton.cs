using System;
using System.Collections.Generic;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Stopwatch
{
    [Register("RoundButton")]
    [DesignTimeVisible(true)]
    public class RoundButton : UIButton
    {
        private Dictionary<UIControlState, UIColor> _fillValues = new Dictionary<UIControlState, UIColor>();
        private CALayer _outerCircleLayer = new CALayer();
        private CALayer _innerCircleLayer = new CALayer();
        private UIColor _defaultFillColor = UIColor.White;

        public RoundButton(IntPtr handle) : base(handle) { }

        public override void AwakeFromNib()
        {
            Initialize();
        }

        public UIColor FillColor
        {
            get => GetFillColorForState(base.State);
        }

        [Export("FillColorForNormal")]
        [Browsable(true)]
        public UIColor FillColorForNormal
        {
            get => GetFillColorForState(UIControlState.Normal);
            set => SetFillColorForState(UIControlState.Normal, value);
        }

        [Export("FillColorForHighlighted")]
        [Browsable(true)]
        public UIColor FillColorForHighlighted
        {
            get => GetFillColorForState(UIControlState.Highlighted);
            set => SetFillColorForState(UIControlState.Highlighted, value);
        }

        [Export("FillColorForSelected")]
        [Browsable(true)]
        public UIColor FillColorForSelected
        {
            get => GetFillColorForState(UIControlState.Selected);
            set => SetFillColorForState(UIControlState.Selected, value);
        }

        [Export("FillColorForDisabled")]
        [Browsable(true)]
        public UIColor FillColorForDisabled
        {
            get => GetFillColorForState(UIControlState.Disabled);
            set => SetFillColorForState(UIControlState.Disabled, value);
        }

        public override bool Selected
        {
            get => base.Selected;
            set
            {
                base.Selected = value;

                UpdateCircleColorForState();
            }
        }

        public override bool Highlighted
        {
            get => base.Highlighted;
            set
            {
                base.Highlighted = value;

                UpdateCircleColorForState();
            }
        }

        public override bool Enabled
        {
            get => base.Enabled;
            set
            {
                base.Enabled = value;

                UpdateCircleColorForState();
            }
        }

        private void Initialize()
        {
            _outerCircleLayer.BorderWidth = 2.0f;
            Layer.InsertSublayer(_outerCircleLayer, 0);

            _innerCircleLayer.MasksToBounds = true;
            Layer.InsertSublayer(_innerCircleLayer, 0);

            UpdateCircleColorForState();
        }

        public override void LayoutSubviews()
        {
            var center = new CGPoint(Bounds.GetMidX(), Bounds.GetMidY());
            var outerFrame = Bounds.Inset(2.0f, 2.0f);
            var innerFrame = outerFrame.Inset(3.0f, 3.0f);

            _outerCircleLayer.Frame = outerFrame;
            _outerCircleLayer.CornerRadius = outerFrame.Size.Width / 2.0f;
            _outerCircleLayer.Position = center;

            _innerCircleLayer.Frame = innerFrame;
            _innerCircleLayer.CornerRadius = innerFrame.Size.Width / 2.0f;
            _innerCircleLayer.Position = center;

            base.LayoutSubviews();
        }

        private void UpdateCircleColorForState()
        {
            var fillColor = FillColor.CGColor;
            _outerCircleLayer.BorderColor = fillColor;
            _innerCircleLayer.BackgroundColor = fillColor;
        }

        private UIColor GetFillColorForState(UIControlState state)
        {
            if (_fillValues.ContainsKey(state))
            {
                return _fillValues[state];
            }

            if (_fillValues.ContainsKey(UIControlState.Normal))
            {
                return _fillValues[UIControlState.Normal];
            }

            return _defaultFillColor;
        }

        private void SetFillColorForState(UIControlState state, UIColor color)
        {
            if (color != null)
            {
                _fillValues[state] = color;
            }
            else
            {
                _fillValues.Remove(state);
            }

            UpdateCircleColorForState();
        }
    }
}
