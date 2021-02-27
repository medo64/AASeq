using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class DocumentControl : ScrollableControl {

        public DocumentControl() {
            this.InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            Helper.ScaleToolstrip(mnxAdd, mnxEndpoint, mnxInteraction);

            this.AutoScroll = true;
        }


        private Document _document;
        public Document Document {
            get { return this._document; }
            set {
                this._selectedEndpoint = null;
                this._selectedInteraction = null;
                this._document = value;
                this.AutoScrollPosition = new Point(0, 0);
                this.Invalidate();
                this.OnChanged(new EventArgs());
                this._lastSelection = null;
                if (this._document != null) {
                    this._document.Changed += delegate(object sender, EventArgs e) {
                        this.OnChanged(new EventArgs());
                    };
                }
            }
        }

        private Endpoint _selectedEndpoint;
        public Endpoint SelectedEndpoint {
            get { return this._selectedEndpoint; }
            set {
                if (value != null) { this._selectedInteraction = null; }
                this._selectedEndpoint = value;
                EnsureVisible(value);
                this.nextVisible = value;
                this.Invalidate();
                this.OnChanged(new EventArgs());
                this.LastSelection = value;
            }
        }

        private Interaction _selectedInteraction;
        public Interaction SelectedInteraction {
            get { return this._selectedInteraction; }
            set {
                if (value != null) { this._selectedEndpoint = null; }
                this._selectedInteraction = value;
                EnsureVisible(value);
                this.Invalidate();
                this.OnChanged(new EventArgs());
                this.LastSelection = value;
            }
        }

        private Object _lastSelection;
        public Object LastSelection {
            get { return this._lastSelection; }
            set {
                if (value != null) { this._lastSelection = value; }
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
            var ev = this.Changed;
            if (ev != null) { ev(this, e); }
        }

        #endregion


        #region Helper

        private readonly List<SensePair> clickSenseList = new List<SensePair>();


        private class SensePair {
            public SensePair(Rectangle rectangle, object Item, bool isMajor = false) {
                this.Rectangle = rectangle;
                this.Item = Item;
                this.IsMajor = isMajor;
            }

            public Rectangle Rectangle { get; private set; }
            public Object Item { get; private set; }
            public bool IsMajor { get; private set; }
        }


        private SensePair FindPairInSenseList(Point location, int tolerance = 0) {
            if (this.clickSenseList != null) {
                foreach (var pair in this.clickSenseList) {
                    if ((location.X >= pair.Rectangle.Left - tolerance) && (location.X <= pair.Rectangle.Right + tolerance) && (location.Y >= pair.Rectangle.Top - tolerance) && (location.Y <= pair.Rectangle.Bottom + tolerance)) {
                        return pair;
                    }
                }
            }
            return null;
        }

        private SensePair FindPairInSenseList(object item) {
            if (this.clickSenseList != null) {
                foreach (var pair in this.clickSenseList) {
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
                this.nextVisible = null;

                var left = -this.AutoScrollPosition.X;
                var right = left + this.ClientRectangle.Width;
                var top = -this.AutoScrollPosition.Y;
                var bottom = top + this.ClientRectangle.Height;

                if (pair.Rectangle.Right > right) { left = pair.Rectangle.Right - this.ClientRectangle.Width + LookAndFeel.Screen.Spacing.Horizontal * 3; }
                if (pair.Rectangle.Left < left) { left = pair.Rectangle.Left - LookAndFeel.Screen.Spacing.Horizontal * 3; }
                if (pair.Rectangle.Bottom > bottom) { top = pair.Rectangle.Bottom - this.ClientRectangle.Height + LookAndFeel.Screen.Spacing.Vertical * 3; }
                if (pair.Rectangle.Top < top) { top = pair.Rectangle.Top - LookAndFeel.Screen.Spacing.Vertical * 3; }

                if (-this.AutoScrollPosition.X != left) { this.AutoScrollPosition = new Point(left, top); }
            } else {
                this.nextVisible = selectedObject;
            }
        }


        private bool IsDraged(Point origin, Point current) {
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
