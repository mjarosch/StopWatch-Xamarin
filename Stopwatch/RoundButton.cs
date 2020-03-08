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

        public RoundButton()
        {
            Initialize();
        }

        public RoundButton(IntPtr handle) : base(handle) { }

        public override void AwakeFromNib()
        {
            Initialize();
        }

        [Export("fillColorForState:")]
        public UIColor FillColor(UIControlState controlState)
        {
            if (_fillValues.ContainsKey(controlState))
            {
                return _fillValues[controlState];
            }

            if (_fillValues.ContainsKey(UIControlState.Normal))
            {
                return _fillValues[UIControlState.Normal];
            }

            return _defaultFillColor;
        }

        [Export("setFillColor:forState:")]
        public void SetFillColor(UIColor fillColor, UIControlState controlState)
        {
            if (fillColor != null)
            {
                _fillValues[controlState] = fillColor;
            }
            else
            {
                _fillValues.Remove(controlState);
            }

            UpdateCircleColorForState();
        }

        [Export("FillColorForNormal")]
        [Browsable(true)]
        public UIColor FillColorForNormal
        {
            get => FillColor(UIControlState.Normal);
            set => SetFillColor(value, UIControlState.Normal);
        }

        [Export("FillColorForHighlighted")]
        [Browsable(true)]
        public UIColor FillColorForHighlighted
        {
            get => FillColor(UIControlState.Highlighted);
            set => SetFillColor(value, UIControlState.Highlighted);
        }

        [Export("FillColorForSelected")]
        [Browsable(true)]
        public UIColor FillColorForSelected
        {
            get => FillColor(UIControlState.Selected);
            set => SetFillColor(value, UIControlState.Selected);
        }

        [Export("FillColorForDisabled")]
        [Browsable(true)]
        public UIColor FillColorForDisabled
        {
            get => FillColor(UIControlState.Disabled);
            set => SetFillColor(value, UIControlState.Disabled);
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

            base.ContentEdgeInsets = new UIEdgeInsets(16, 16, 16, 16);

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
            var fillColor = FillColor(base.State).CGColor;
            _outerCircleLayer.BorderColor = fillColor;
            _innerCircleLayer.BackgroundColor = fillColor;
        }
    }
}
