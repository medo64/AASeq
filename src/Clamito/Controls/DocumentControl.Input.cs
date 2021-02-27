using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    partial class DocumentControl {

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Left:
                    {
                        if (SelectedEndpoint != null) {
                            var nextIndex = Document.Endpoints.IndexOf(SelectedEndpoint) - 1;
                            if (nextIndex >= 0) {
                                SelectedEndpoint = Document.Endpoints[nextIndex];
                            }
                        } else if (Document.Endpoints.Count > 0) { //select first endpoint if nothing was selected
                            SelectedEndpoint = Document.Endpoints[0];
                        }
                    }
                    return true;

                case Keys.Right:
                    {
                        if (SelectedEndpoint != null) {
                            var nextIndex = Document.Endpoints.IndexOf(SelectedEndpoint) + 1;
                            if (nextIndex < Document.Endpoints.Count) {
                                SelectedEndpoint = Document.Endpoints[nextIndex];
                            }
                        } else if (Document.Endpoints.Count > 0) { //select last endpoint if nothing was selected
                            SelectedEndpoint = Document.Endpoints[Document.Endpoints.Count - 1];
                        }
                    }
                    return true;

                case Keys.Up:
                    {
                        if (SelectedInteraction != null) {
                            var nextIndex = Document.Interactions.IndexOf(SelectedInteraction) - 1;
                            if (nextIndex >= 0) {
                                SelectedInteraction = Document.Interactions[nextIndex];
                            }
                        } else if (Document.Interactions.Count > 0) { //select first interaction if nothing was selected
                            SelectedInteraction = Document.Interactions[0];
                        }
                    }
                    return true;

                case Keys.Down:
                    {
                        if (SelectedInteraction != null) {
                            var nextIndex = Document.Interactions.IndexOf(SelectedInteraction) + 1;
                            if (nextIndex < Document.Interactions.Count) {
                                SelectedInteraction = Document.Interactions[nextIndex];
                            }
                        } else if (Document.Interactions.Count > 0) { //select first interaction if nothing was selected
                            SelectedInteraction = Document.Interactions[0];
                        }
                    }
                    return true;

                case Keys.Escape:
                    {
                        SelectedEndpoint = null;
                        SelectedInteraction = null;
                    }
                    return true;

                case (Keys)93:
                    { //ContextMenu key
                        var pair = FindPairInSenseList(LastSelection);
                        if ((pair != null) && pair.IsMajor) {
                            var location = PointToScreen(new Point(pair.Rectangle.Right - pair.Rectangle.Width / 3, pair.Rectangle.Bottom - LookAndFeel.Screen.Spacing.Bottom));
                            if (SelectedEndpoint == pair.Item) {
                                mnxEndpoint.Show(location);
                            } else if (SelectedInteraction == pair.Item) {
                                mnxInteraction.Show(location);
                            }
                        }
                    }
                    return true;

                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private Point currentMouseLocation = Point.Empty;
        private SensePair dragPair;
        private Point dragOrigin;
        private bool dragInProgress;

        protected override void OnMouseDown(MouseEventArgs e) {
            var location = e.Location;
            location.Offset(-AutoScrollPosition.X, -AutoScrollPosition.Y);

            var pair = FindPairInSenseList(location);
            if (pair != null) {
                var endpoint = pair.Item as Endpoint;
                var interaction = pair.Item as Interaction;
                if (endpoint != null) {
                    SelectedEndpoint = endpoint;
                } else if (interaction != null) {
                    SelectedInteraction = interaction;
                }
            } else {
                SelectedEndpoint = null;
                SelectedInteraction = null;
            }

            switch (e.Button) {
                case MouseButtons.Left:
                    { //dragging
                        dragInProgress = false; //don't start drag immediatelly
                        if (pair != null) {
                            dragOrigin = e.Location;
                            dragPair = pair;
                        }
                    }
                    break;

                case MouseButtons.Right:
                    { //menu
                        var menuLocation = PointToScreen(e.Location);
                        if (pair == null) {
                            mnxAdd.Show(menuLocation);
                        } else {
                            if (SelectedEndpoint == pair.Item) {
                                mnxEndpoint.Show(menuLocation);
                            } else if (SelectedInteraction == pair.Item) {
                                mnxInteraction.Show(menuLocation);
                            }
                        }
                    }
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            var location = e.Location;
            location.Offset(-AutoScrollPosition.X, -AutoScrollPosition.Y);

            switch (e.Button) {
                case MouseButtons.Left:
                    { //dragging
                        if (dragPair != null) {
                            if (dragPair.IsMajor) { //moving endpoint/interaction
                                dragInProgress |= IsDraged(dragOrigin, e.Location); //once drag is in progress, it is always in progress (otherwise it won't show drag over origin)
                                if (dragInProgress) {
                                    var newCursor = Cursors.Default;
                                    var origEndpoint = dragPair.Item as Endpoint;
                                    var origInteraction = dragPair.Item as Interaction;

                                    if (origEndpoint != null) {
                                        var destEndpoint = FindEndpointDragDestination(e.Location);
                                        if ((destEndpoint == null) || origEndpoint.Equals(destEndpoint)) {
                                            newCursor = origEndpoint.Equals(destEndpoint) || (Document.Endpoints.Count < 2) ? Cursors.No : Cursors.NoMoveHoriz;
                                        } else {
                                            var indexOrig = Document.Endpoints.IndexOf(origEndpoint);
                                            var indexDest = Document.Endpoints.IndexOf(destEndpoint);
                                            newCursor = (indexOrig > indexDest) ? Cursors.PanWest : Cursors.PanEast;
                                        }
                                    } else if (origInteraction != null) {
                                        var destInteraction = FindInteractionDragDestination(e.Location);
                                        if ((destInteraction == null) || origInteraction.Equals(destInteraction)) {
                                            newCursor = origInteraction.Equals(destInteraction) || (Document.Interactions.Count < 2) ? Cursors.No : Cursors.NoMoveVert;
                                        } else {
                                            var indexOrig = Document.Interactions.IndexOf(origInteraction);
                                            var indexDest = Document.Interactions.IndexOf(destInteraction);
                                            newCursor = (indexOrig > indexDest) ? Cursors.PanNorth : Cursors.PanSouth;
                                        }
                                    }

                                    Cursor = newCursor;
                                }
                            } else { //maybe dragging new message
                                var origEndpoint = dragPair.Item as Endpoint;
                                if (origEndpoint != null) {
                                    dragInProgress |= IsDraged(dragOrigin, e.Location); //once drag is in progress, it is always in progress (otherwise it won't show drag over origin)
                                    if (dragInProgress) {
                                        var newCursor = (Document.Endpoints.Count >= 2) ? Cursors.NoMoveHoriz : Cursors.No;
                                        var destEndpoint = FindEndpointDragDestination(e.Location);
                                        if ((destEndpoint != null) && !origEndpoint.Equals(destEndpoint)) {
                                            var indexOrig = Document.Endpoints.IndexOf(origEndpoint);
                                            var indexDest = Document.Endpoints.IndexOf(destEndpoint);
                                            newCursor = (indexOrig > indexDest) ? Cursors.PanWest : Cursors.PanEast;
                                        }
                                        Cursor = newCursor;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            currentMouseLocation = location;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Left:
                    { //dropping
                        if (dragInProgress) {
                            if (dragPair.IsMajor) {
                                var origEndpoint = dragPair.Item as Endpoint;
                                var origInteraction = dragPair.Item as Interaction;

                                if (origEndpoint != null) {
                                    var destEndpoint = FindEndpointDragDestination(e.Location);
                                    if ((destEndpoint != null) && !origEndpoint.Equals(destEndpoint)) {
                                        var indexOrig = Document.Endpoints.IndexOf(origEndpoint);
                                        var indexDest = Document.Endpoints.IndexOf(destEndpoint);
                                        Document.Endpoints.MoveItem(indexOrig, indexDest);
                                        Invalidate();
                                    }
                                } else if (origInteraction != null) {
                                    var destInteraction = FindInteractionDragDestination(e.Location);
                                    if ((destInteraction != null) && !origInteraction.Equals(destInteraction)) {
                                        var indexOrig = Document.Interactions.IndexOf(origInteraction);
                                        var indexDest = Document.Interactions.IndexOf(destInteraction);
                                        Document.Interactions.MoveItem(indexOrig, indexDest);
                                        Invalidate();
                                    }
                                }
                            } else { //maybe create a message
                                var origEndpoint = dragPair.Item as Endpoint;
                                var destEndpoint = FindEndpointDragDestination(e.Location);
                                if ((origEndpoint != null) && (destEndpoint != null) && !origEndpoint.Equals(destEndpoint)) {
                                    //find closest interaction above current position
                                    SensePair insertBeforePair = null;
                                    foreach (var pair in clickSenseList) {
                                        if ((pair.Item is Interaction) && (pair.Rectangle.Top > e.Location.Y)) {
                                            if ((insertBeforePair == null) || (insertBeforePair.Rectangle.Top > pair.Rectangle.Top)) {
                                                insertBeforePair = pair;
                                            }
                                        }
                                    }
                                    var insertBefore = (insertBeforePair != null) ? (Interaction)insertBeforePair.Item : null;
                                    using (var frm = new MessageForm(Document, null, insertBefore, origEndpoint, destEndpoint)) {
                                        if (frm.ShowDialog(this) == DialogResult.OK) {
                                            SelectedInteraction = frm.SelectedInteraction;
                                            Invalidate();
                                        }
                                    }
                                }
                            }
                            dragInProgress = false;
                        }
                        dragOrigin = Point.Empty;
                        dragPair = null;
                        Cursor = Cursors.Default;
                    }
                    break;

            }
        }


        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            var location = e.Location;
            location.Offset(-AutoScrollPosition.X, -AutoScrollPosition.Y);

            var pair = FindPairInSenseList(location);
            if (pair != null) {
                var endpoint = pair.Item as Endpoint;
                var interaction = pair.Item as Interaction;
                if (endpoint != null) {
                    SelectedEndpoint = endpoint;
                    mnxEndpointProperties_Click(null, null);
                } else if (interaction != null) {
                    SelectedInteraction = interaction;
                    mnxInteractionProperties_Click(null, null);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e) {
            base.OnMouseLeave(e);
            currentMouseLocation = Point.Empty;
        }

    }
}
