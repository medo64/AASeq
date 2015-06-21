using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    partial class DocumentControl {

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Left: {
                        if (this.SelectedEndpoint != null) {
                            var nextIndex = this.Document.Endpoints.IndexOf(this.SelectedEndpoint) - 1;
                            if (nextIndex >= 0) {
                                this.SelectedEndpoint = this.Document.Endpoints[nextIndex];
                            }
                        } else if (this.Document.Endpoints.Count > 0) { //select first endpoint if nothing was selected
                            this.SelectedEndpoint = this.Document.Endpoints[0];
                        }
                    } return true;

                case Keys.Right: {
                        if (this.SelectedEndpoint != null) {
                            var nextIndex = this.Document.Endpoints.IndexOf(this.SelectedEndpoint) + 1;
                            if (nextIndex < this.Document.Endpoints.Count) {
                                this.SelectedEndpoint = this.Document.Endpoints[nextIndex];
                            }
                        } else if (this.Document.Endpoints.Count > 0) { //select last endpoint if nothing was selected
                            this.SelectedEndpoint = this.Document.Endpoints[this.Document.Endpoints.Count - 1];
                        }
                    } return true;

                case Keys.Up: {
                        if (this.SelectedInteraction != null) {
                            var nextIndex = this.Document.Interactions.IndexOf(this.SelectedInteraction) - 1;
                            if (nextIndex >= 0) {
                                this.SelectedInteraction = this.Document.Interactions[nextIndex];
                            }
                        } else if (this.Document.Interactions.Count > 0) { //select first interaction if nothing was selected
                            this.SelectedInteraction = this.Document.Interactions[0];
                        }
                    } return true;

                case Keys.Down: {
                        if (this.SelectedInteraction != null) {
                            var nextIndex = this.Document.Interactions.IndexOf(this.SelectedInteraction) + 1;
                            if (nextIndex < this.Document.Interactions.Count) {
                                this.SelectedInteraction = this.Document.Interactions[nextIndex];
                            }
                        } else if (this.Document.Interactions.Count > 0) { //select first interaction if nothing was selected
                            this.SelectedInteraction = this.Document.Interactions[0];
                        }
                    } return true;

                case Keys.Escape: {
                        this.SelectedEndpoint = null;
                        this.SelectedInteraction = null;
                    } return true;

                case (Keys)93: { //ContextMenu key
                        var pair = FindPairInSenseList(this.LastSelection);
                        if ((pair != null) && pair.IsMajor) {
                            var location = this.PointToScreen(new Point(pair.Rectangle.Right - pair.Rectangle.Width / 3, pair.Rectangle.Bottom - LookAndFeel.Screen.Spacing.Bottom));
                            if (this.SelectedEndpoint == pair.Item) {
                                mnxEndpoint.Show(location);
                            } else if (this.SelectedInteraction == pair.Item) {
                                mnxInteraction.Show(location);
                            }
                        }
                    } return true;

                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private Point currentMouseLocation = Point.Empty;
        private SensePair dragPair;
        private Point dragOrigin;
        private bool dragInProgress;

        protected override void OnMouseDown(MouseEventArgs e) {
            var location = e.Location;
            location.Offset(-this.AutoScrollPosition.X, -this.AutoScrollPosition.Y);

            var pair = FindPairInSenseList(location);
            if (pair != null) {
                var endpoint = pair.Item as Endpoint;
                var interaction = pair.Item as Interaction;
                if (endpoint != null) {
                    this.SelectedEndpoint = endpoint;
                } else if (interaction != null) {
                    this.SelectedInteraction = interaction;
                }
            } else {
                this.SelectedEndpoint = null;
                this.SelectedInteraction = null;
            }

            switch (e.Button) {
                case MouseButtons.Left: { //dragging
                        this.dragInProgress = false; //don't start drag immediatelly
                        if (pair != null) {
                            this.dragOrigin = e.Location;
                            this.dragPair = pair;
                        }
                    } break;

                case MouseButtons.Right: { //menu
                        var menuLocation = this.PointToScreen(e.Location);
                        if (pair == null) {
                            mnxAdd.Show(menuLocation);
                        } else {
                            if (this.SelectedEndpoint == pair.Item) {
                                mnxEndpoint.Show(menuLocation);
                            } else if (this.SelectedInteraction == pair.Item) {
                                mnxInteraction.Show(menuLocation);
                            }
                        }
                    } break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            var location = e.Location;
            location.Offset(-this.AutoScrollPosition.X, -this.AutoScrollPosition.Y);

            switch (e.Button) {
                case MouseButtons.Left: { //dragging
                        if (this.dragPair != null) {
                            if (dragPair.IsMajor) { //moving endpoint/interaction
                                this.dragInProgress |= IsDraged(this.dragOrigin, e.Location); //once drag is in progress, it is always in progress (otherwise it won't show drag over origin)
                                if (this.dragInProgress) {
                                    var newCursor = Cursors.Default;
                                    var origEndpoint = this.dragPair.Item as Endpoint;
                                    var origInteraction = this.dragPair.Item as Interaction;

                                    if (origEndpoint != null) {
                                        var destEndpoint = FindEndpointDragDestination(e.Location);
                                        if ((destEndpoint == null) || origEndpoint.Equals(destEndpoint)) {
                                            newCursor = Cursors.No;
                                        } else {
                                            var indexOrig = this.Document.Endpoints.IndexOf(origEndpoint);
                                            var indexDest = this.Document.Endpoints.IndexOf(destEndpoint);
                                            newCursor = (indexOrig > indexDest) ? Cursors.PanWest : Cursors.PanEast;
                                        }
                                    } else if (origInteraction != null) {
                                        var destInteraction = FindInteractionDragDestination(e.Location);
                                        if ((destInteraction == null) || origInteraction.Equals(destInteraction)) {
                                            newCursor = Cursors.No;
                                        } else {
                                            var indexOrig = this.Document.Interactions.IndexOf(origInteraction);
                                            var indexDest = this.Document.Interactions.IndexOf(destInteraction);
                                            newCursor = (indexOrig > indexDest) ? Cursors.PanNorth : Cursors.PanSouth;
                                        }
                                    }

                                    this.Cursor = newCursor;
                                }
                            } else { //maybe dragging new message
                                var origEndpoint = this.dragPair.Item as Endpoint;
                                if (origEndpoint != null) {
                                    this.dragInProgress |= IsDraged(this.dragOrigin, e.Location); //once drag is in progress, it is always in progress (otherwise it won't show drag over origin)
                                    if (this.dragInProgress) {
                                        var newCursor = Cursors.NoMoveHoriz;
                                        var destEndpoint = FindEndpointDragDestination(e.Location);
                                        if ((destEndpoint != null) && !origEndpoint.Equals(destEndpoint)) {
                                            var indexOrig = this.Document.Endpoints.IndexOf(origEndpoint);
                                            var indexDest = this.Document.Endpoints.IndexOf(destEndpoint);
                                            newCursor = (indexOrig > indexDest) ? Cursors.PanWest : Cursors.PanEast;
                                        }
                                        this.Cursor = newCursor;
                                    }
                                }
                            }
                        }
                    } break;
            }

            this.currentMouseLocation = location;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Left: { //dropping
                        if (this.dragInProgress) {
                            if (this.dragPair.IsMajor) {
                                var origEndpoint = this.dragPair.Item as Endpoint;
                                var origInteraction = this.dragPair.Item as Interaction;

                                if (origEndpoint != null) {
                                    var destEndpoint = FindEndpointDragDestination(e.Location);
                                    if ((destEndpoint != null) && !origEndpoint.Equals(destEndpoint)) {
                                        var indexOrig = this.Document.Endpoints.IndexOf(origEndpoint);
                                        var indexDest = this.Document.Endpoints.IndexOf(destEndpoint);
                                        this.Document.Endpoints.MoveItem(indexOrig, indexDest);
                                        this.Invalidate();
                                    }
                                } else if (origInteraction != null) {
                                    var destInteraction = FindInteractionDragDestination(e.Location);
                                    if ((destInteraction != null) && !origInteraction.Equals(destInteraction)) {
                                        var indexOrig = this.Document.Interactions.IndexOf(origInteraction);
                                        var indexDest = this.Document.Interactions.IndexOf(destInteraction);
                                        this.Document.Interactions.MoveItem(indexOrig, indexDest);
                                        this.Invalidate();
                                    }
                                }
                            } else { //maybe create a message
                                var origEndpoint = this.dragPair.Item as Endpoint;
                                var destEndpoint = FindEndpointDragDestination(e.Location);
                                if ((origEndpoint != null) && (destEndpoint != null) && !origEndpoint.Equals(destEndpoint)) {
                                    //find closest interaction above current position
                                    SensePair insertBeforePair = null;
                                    foreach (var pair in this.clickSenseList) {
                                        if ((pair.Item is Interaction) && (pair.Rectangle.Top > e.Location.Y)) {
                                            if ((insertBeforePair == null) || (insertBeforePair.Rectangle.Top > pair.Rectangle.Top)) {
                                                insertBeforePair = pair;
                                            }
                                        }
                                    }
                                    var insertBefore = (insertBeforePair != null) ? (Interaction)insertBeforePair.Item : null;
                                    using (var frm = new MessageForm(this.Document, null, insertBefore, origEndpoint, destEndpoint)) {
                                        if (frm.ShowDialog(this) == DialogResult.OK) {
                                            this.SelectedInteraction = frm.SelectedInteraction;
                                            this.Invalidate();
                                        }
                                    }
                                }
                            }
                            this.dragInProgress = false;
                        }
                        this.dragOrigin = Point.Empty;
                        this.dragPair = null;
                        this.Cursor = Cursors.Default;
                    } break;

            }
        }


        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            var location = e.Location;
            location.Offset(-this.AutoScrollPosition.X, -this.AutoScrollPosition.Y);

            var pair = FindPairInSenseList(location);
            if (pair != null) {
                var endpoint = pair.Item as Endpoint;
                var interaction = pair.Item as Interaction;
                if (endpoint != null) {
                    this.SelectedEndpoint = endpoint;
                    mnxEndpointProperties_Click(null, null);
                } else if (interaction != null) {
                    this.SelectedInteraction = interaction;
                    mnxInteractionProperties_Click(null, null);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            this.currentMouseLocation = Point.Empty;
        }

    }
}
