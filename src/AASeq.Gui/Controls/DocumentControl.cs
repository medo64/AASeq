using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AASeq;

namespace AASeq.Gui {
    internal partial class DocumentControl : ScrollableControl {

        public DocumentControl() {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.StandardClick, true);
            SetStyle(ControlStyles.StandardDoubleClick, true);
            SetStyle(ControlStyles.UserPaint, true);

            Helper.ScaleToolstrip(mnxAdd, mnxEndpoint, mnxInteraction);

            AutoScroll = true;
        }


        private Document _document;
        public Document Document {
            get { return _document; }
            set {
                _selectedEndpoint = null;
                _selectedInteraction = null;
                _document = value;
                AutoScrollPosition = new Point(0, 0);
                Invalidate();
                OnChanged(new EventArgs());
                _lastSelection = null;
                if (_document != null) {
                    _document.Changed += delegate(object sender, EventArgs e) {
                        OnChanged(new EventArgs());
                    };
                }
            }
        }

        private Endpoint _selectedEndpoint;
        public Endpoint SelectedEndpoint {
            get { return _selectedEndpoint; }
            set {
                if (value != null) { _selectedInteraction = null; }
                _selectedEndpoint = value;
                EnsureVisible(value);
                nextVisible = value;
                Invalidate();
                OnChanged(new EventArgs());
                LastSelection = value;
            }
        }

        private Interaction _selectedInteraction;
        public Interaction SelectedInteraction {
            get { return _selectedInteraction; }
            set {
                if (value != null) { _selectedEndpoint = null; }
                _selectedInteraction = value;
                EnsureVisible(value);
                Invalidate();
                OnChanged(new EventArgs());
                LastSelection = value;
            }
        }

        private Object _lastSelection;
        public Object LastSelection {
            get { return _lastSelection; }
            set {
                if (value != null) { _lastSelection = value; }
            }
        }

        private Object nextVisible = null;


        #region Events

        /// <summary>
        /// Occurs when there is a change.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Raises Changed event.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected void OnChanged(EventArgs e) {
            var ev = Changed;
            if (ev != null) { ev(this, e); }
        }

        #endregion


        #region Helper

        private readonly List<SensePair> clickSenseList = new List<SensePair>();


        private class SensePair {
            public SensePair(Rectangle rectangle, object item, bool isMajor = false) {
                Rectangle = rectangle;
                Item = item;
                IsMajor = isMajor;
            }

            public Rectangle Rectangle { get; private set; }
            public Object Item { get; private set; }
            public bool IsMajor { get; private set; }
        }


        private SensePair FindPairInSenseList(Point location, int tolerance = 0) {
            if (clickSenseList != null) {
                foreach (var pair in clickSenseList) {
                    if ((location.X >= pair.Rectangle.Left - tolerance) && (location.X <= pair.Rectangle.Right + tolerance) && (location.Y >= pair.Rectangle.Top - tolerance) && (location.Y <= pair.Rectangle.Bottom + tolerance)) {
                        return pair;
                    }
                }
            }
            return null;
        }

        private SensePair FindPairInSenseList(object item) {
            if (clickSenseList != null) {
                foreach (var pair in clickSenseList) {
                    if (pair.Item.Equals(item)) {
                        return pair;
                    }
                }
            }
            return null;
        }


        private Endpoint FindEndpointDragDestination(Point currentLocation) {
            var dragDistance = (SystemInformation.DragSize.Height + SystemInformation.DragSize.Width) / 2;
            var pair = FindPairInSenseList(currentLocation, dragDistance * 4);
            return (pair != null) ? pair.Item as Endpoint : null;
        }

        private Interaction FindInteractionDragDestination(Point currentLocation) {
            var dragDistance = (SystemInformation.DragSize.Height + SystemInformation.DragSize.Width) / 2;
            var pair = FindPairInSenseList(currentLocation, dragDistance * 2);
            return (pair != null) ? pair.Item as Interaction : null;
        }


        private void EnsureVisible(object selectedObject) {
            var pair = FindPairInSenseList(selectedObject);
            if (pair != null) {
                nextVisible = null;

                var left = -AutoScrollPosition.X;
                var right = left + ClientRectangle.Width;
                var top = -AutoScrollPosition.Y;
                var bottom = top + ClientRectangle.Height;

                if (pair.Rectangle.Right > right) { left = pair.Rectangle.Right - ClientRectangle.Width + LookAndFeel.Screen.Spacing.Horizontal * 3; }
                if (pair.Rectangle.Left < left) { left = pair.Rectangle.Left - LookAndFeel.Screen.Spacing.Horizontal * 3; }
                if (pair.Rectangle.Bottom > bottom) { top = pair.Rectangle.Bottom - ClientRectangle.Height + LookAndFeel.Screen.Spacing.Vertical * 3; }
                if (pair.Rectangle.Top < top) { top = pair.Rectangle.Top - LookAndFeel.Screen.Spacing.Vertical * 3; }

                if (-AutoScrollPosition.X != left) { AutoScrollPosition = new Point(left, top); }
            } else {
                nextVisible = selectedObject;
            }
        }


        private static bool IsDraged(Point origin, Point current) {
            var distance = Math.Sqrt(Math.Pow(current.X - origin.X, 2) + Math.Pow(current.Y - origin.Y, 2));
            return (distance > (SystemInformation.DragSize.Height + SystemInformation.DragSize.Width) / 2);
        }

        private static Color BlendHigh(Color color) { //removes transparency toward white
            var r = 255 - (((255 - color.R) * color.A) / 255);
            var g = 255 - (((255 - color.G) * color.A) / 255);
            var b = 255 - (((255 - color.B) * color.A) / 255);
            return Color.FromArgb(255, r, g, b);
        }

        private static Color BlendLow(Color color) { //removes transparency toward black
            var r = ((color.R * color.A) / 255);
            var g = ((color.G * color.A) / 255);
            var b = ((color.B * color.A) / 255);
            return Color.FromArgb(255, r, g, b);
        }

        #endregion

    }
}
