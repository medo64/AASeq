using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Clamito.Gui {
    partial class DocumentControl {

        protected override void OnPaint(PaintEventArgs e) {
            if (e == null) { return; }

            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            clickSenseList.Clear();
            var newAutoScrollMinSize = PaintDocument(
                e.Graphics,
                font: Font,
                view: LookAndFeel.Screen,
                startingSize: ClientRectangle.Size,
                senseList: clickSenseList
            );

            if (AutoScrollMinSize != newAutoScrollMinSize) { AutoScrollMinSize = newAutoScrollMinSize; }
            if (nextVisible != null) {
                AutoScrollPosition = new Point(0, 0);
                EnsureVisible(nextVisible);
            }
        }

        private Size PaintDocument(Graphics g, Font font, LookAndFeel view, Size startingSize, List<SensePair> senseList) {
            g.CompositingQuality = view.CompositingQuality;
            g.SmoothingMode = view.SmoothingMode;

            if (view.BackColor.HasValue) { g.Clear(view.BackColor.Value); }
            if (Document == null) { return new Size(0, 0); }

            var maxWidth = (view.Margins.Left > startingSize.Width) ? view.Margins.Left : startingSize.Width;
            var maxHeight = (view.Margins.Top > startingSize.Height) ? view.Margins.Top : startingSize.Height;

            int left;
            var top = view.Margins.Top;

            var fontSizeF = g.MeasureString("MXW", font);
            var fontSize = new Size((int)Math.Ceiling(fontSizeF.Width / 3), (int)Math.Ceiling(fontSizeF.Height));


            //Calculate endpoint locations
            var endpoints = Document.Endpoints;
            if (endpoints.Count == 0) {
                g.DrawString("No endpoints", Font, view.Endpoint.NormalInk.LightBrush, new Rectangle(0, top, maxWidth, fontSize.Height + view.Spacing.Vertical), view.Endpoint.TitleFormat);
                return new Size(maxWidth, top + fontSize.Height + view.Spacing.Vertical);
            }

            var endpointWidth = fontSize.Width * view.Endpoint.TotalCharCount;
            var totalWidth = view.Margins.Left + (endpointWidth * endpoints.Count) + (view.Spacing.Horizontal * (endpoints.Count - 1)) + view.Margins.Right;
            var leftPrefix = (startingSize.Width - totalWidth) / 2;
            if (leftPrefix < 0) { leftPrefix = 0; }
            left = leftPrefix + view.Margins.Left + endpointWidth / 2;
            var endpointsX = new Dictionary<Endpoint, Int32>();
            for (int i = 0; i < endpoints.Count; i++) {
                endpointsX.Add(endpoints[i], left);
                left += endpointWidth + view.Spacing.Horizontal;
            }
            left = totalWidth;

            var endpointTitleTop = top;
            var endpointLineTop = endpointTitleTop + fontSize.Height + view.Spacing.Vertical;

            if (left > maxWidth) { maxWidth = left; }


            //Draw endpoints
            foreach (var item in endpointsX) {
                var endpoint = item.Key;
                var x = item.Value;

                var sensePairs = PaintEndpoint(endpoint, g, view.Endpoint, view.Endpoint.NormalInk, x, endpointTitleTop, maxHeight - view.Margins.Bottom, font, fontSize);
                if (senseList != null) { senseList.AddRange(sensePairs); }

                if (endpoint.Equals(SelectedEndpoint)) {
                    PaintEndpoint(endpoint, g, view.Endpoint, view.Endpoint.SelectedInk, x, endpointTitleTop, maxHeight - view.Margins.Bottom, font, fontSize);
                } else {
                    foreach (var pair in sensePairs) {
                        if (pair.Rectangle.Contains(currentMouseLocation)) {
                            PaintEndpoint(endpoint, g, view.Endpoint, view.Endpoint.HighlightInk, x, endpointTitleTop, maxHeight - view.Margins.Bottom, font, fontSize);
                            break;
                        }
                    }
                }

                if (senseList != null) { senseList.AddRange(sensePairs); }
            }
            top += fontSize.Height + view.Spacing.Vertical * 2;

            if (top > maxHeight) { maxHeight = top; }


            //Draw interactions
            top = endpointLineTop;
            var interactionHeight = view.Spacing.Vertical * 6 + fontSize.Height;

            foreach (var interaction in Document.Interactions) {
                var middle = top + interactionHeight / 2;
                if (interaction.Kind == InteractionKind.Command) {
                    var command = (Command)interaction;

                    var srcX = (maxWidth - endpointWidth) / 2;
                    var dstX = srcX + endpointWidth;

                    var sensePairs = PaintCommand(command, g, view.Interaction.NormalInk, srcX, dstX, middle, font, fontSize);
                    if (senseList != null) { senseList.AddRange(sensePairs); }

                    if (interaction.Equals(SelectedInteraction)) {
                        PaintCommand(command, g, view.Interaction.SelectedInk, srcX, dstX, middle, font, fontSize);
                    } else {
                        foreach (var pair in sensePairs) {
                            if (pair.Rectangle.Contains(currentMouseLocation)) {
                                PaintCommand(command, g, view.Interaction.HighlightInk, srcX, dstX, middle, font, fontSize);
                                break;
                            }
                        }
                    }
                } else if (interaction.IsMessage) {
                    var message = (Message)interaction;

                    var srcX = endpointsX[message.Source];
                    var dstX = endpointsX[message.Destination];

                    var sensePairs = PaintMessage(message, g, view.Interaction.NormalInk, srcX, dstX, middle, font, fontSize);
                    if (senseList != null) { senseList.AddRange(sensePairs); }

                    if (interaction.Equals(SelectedInteraction)) {
                        PaintMessage(message, g, view.Interaction.SelectedInk, srcX, dstX, middle, font, fontSize);
                    } else {
                        foreach (var pair in sensePairs) {
                            if (pair.Rectangle.Contains(currentMouseLocation)) {
                                PaintMessage(message, g, view.Interaction.HighlightInk, srcX, dstX, middle, font, fontSize);
                                break;
                            }
                        }
                    }
                }
                top += interactionHeight;
            }


            return new Size(maxWidth, maxHeight);
        }

        private static IEnumerable<SensePair> PaintEndpoint(Endpoint endpoint, Graphics g, DisplayStyle style, DisplayInk ink, int x, int y, int maxHeight, Font font, Size charSize) {
            var endpointText = string.IsNullOrEmpty(endpoint.Caption) ? endpoint.Name : endpoint.Caption;
            var size = g.MeasureString(endpointText, font, charSize.Width * style.MaxCharCount, style.TitleFormat).ToSize();
            var rect = new Rectangle(x - size.Width / 2 - charSize.Width / 2, y, size.Width + charSize.Width, charSize.Height);
            var senseRect = new Rectangle(rect.Left, rect.Top, rect.Width + 1, rect.Height + 1);

            g.FillRectangle(ink.BackBrush, rect);

            g.DrawLine(ink.Pen, x - charSize.Width, y + charSize.Height, x + charSize.Width, y + charSize.Height); //horizontal
            if (endpoint.ProtocolName == null) {
                var thickPen = new Pen(ink.Pen.Brush, ink.Pen.Width * 2);
                g.DrawLine(thickPen, x, y + charSize.Height, x, maxHeight); //vertical
            } else {
                g.DrawLine(ink.Pen, x, y + charSize.Height, x, maxHeight); //vertical
            }
            var senseVRect = new Rectangle(x - charSize.Width / 4 - 1, y + charSize.Height - 1, charSize.Width / 2, maxHeight - y - charSize.Height + 2);

            g.DrawString(endpointText, font, ink.Brush, rect, style.TitleFormat);

            return new SensePair[] {
                new SensePair(senseRect, endpoint, isMajor: true),
                new SensePair(senseVRect, endpoint, isMajor: false) 
            };
        }

        private static IEnumerable<SensePair> PaintCommand(Command command, Graphics g, DisplayInk ink, int sourceX, int destinationX, int middleY, Font font, Size fontSize) {
            var arrowX = fontSize.Width;
            var sensePairs = new List<SensePair>();

            { //text
                var title = command.ToString();
                var maxRect = new Rectangle(Math.Min(sourceX, destinationX) + fontSize.Width, middleY - fontSize.Height, Math.Abs(destinationX - sourceX) - fontSize.Width * 2, fontSize.Height);
                var size = g.MeasureString(title, font, maxRect.Size, LookAndFeel.Screen.Interaction.TitleFormat).ToSize();
                var centerX = (sourceX + destinationX) / 2;
                var rect = new Rectangle(centerX - size.Width / 2 - arrowX / 2, maxRect.Top, size.Width + arrowX, maxRect.Height);
                g.FillRectangle(ink.LightBrush, rect);
                g.DrawString(title, font, ink.Brush, maxRect, LookAndFeel.Screen.Interaction.TitleFormat);
                sensePairs.Add(new SensePair(rect, command, isMajor: true));
            }

            //description
            if (!string.IsNullOrWhiteSpace(command.Caption)) {
                var maxRect = new Rectangle(Math.Min(sourceX, destinationX) + fontSize.Width, middleY, Math.Abs(destinationX - sourceX) - fontSize.Width * 2, fontSize.Height);
                var size = g.MeasureString(command.Caption, font, maxRect.Size, LookAndFeel.Screen.Interaction.TitleFormat).ToSize();
                var centerX = (sourceX + destinationX) / 2;
                var rect = new Rectangle(centerX - size.Width / 2 - arrowX / 2, maxRect.Top, size.Width + arrowX, maxRect.Height);
                g.FillRectangle(ink.BackBrush, rect);
                g.DrawString(command.Caption, font, ink.LightBrush, maxRect, LookAndFeel.Screen.Interaction.TitleFormat);
                sensePairs.Add(new SensePair(rect, command, isMajor: false));
            }

            return sensePairs.AsReadOnly();
        }

        private static IEnumerable<SensePair> PaintMessage(Message message, Graphics g, DisplayInk ink, int sourceX, int destinationX, int middleY, Font font, Size fontSize) {
            var arrowX = fontSize.Width;
            var arrowY = fontSize.Height / 4;
            var penHalfWidth = (int)ink.Pen.Width / 2;
            var sensePairs = new List<SensePair>();

            { //text
                var maxRect = new Rectangle(Math.Min(sourceX, destinationX) + fontSize.Width, middleY - fontSize.Height, Math.Abs(destinationX - sourceX) - fontSize.Width * 2, fontSize.Height);
                var size = g.MeasureString(message.Name, font, maxRect.Size, LookAndFeel.Screen.Interaction.TitleFormat).ToSize();
                var centerX = (sourceX + destinationX) / 2;
                var rect = new Rectangle(centerX - size.Width / 2 - arrowX / 2, maxRect.Top, size.Width + arrowX, maxRect.Height);
                g.FillRectangle(ink.BackBrush, rect);
                g.DrawString(message.Name, font, ink.Brush, maxRect, LookAndFeel.Screen.Interaction.TitleFormat);
                sensePairs.Add(new SensePair(rect, message, isMajor: true));
            }

            //description
            if (!string.IsNullOrWhiteSpace(message.Caption)) {
                var maxRect = new Rectangle(Math.Min(sourceX, destinationX) + fontSize.Width, middleY, Math.Abs(destinationX - sourceX) - fontSize.Width * 2, fontSize.Height);
                var size = g.MeasureString(message.Caption, font, maxRect.Size, LookAndFeel.Screen.Interaction.TitleFormat).ToSize();
                var centerX = (sourceX + destinationX) / 2;
                var rect = new Rectangle(centerX - size.Width / 2 - arrowX / 2, maxRect.Top, size.Width + arrowX, maxRect.Height);
                g.FillRectangle(ink.BackBrush, rect);
                g.DrawString(message.Caption, font, ink.LightBrush, maxRect, LookAndFeel.Screen.Interaction.TitleFormat);
                sensePairs.Add(new SensePair(rect, message, isMajor: false));
            }

            //line
            if (sourceX < destinationX) { //arrows facing left
                g.DrawLine(ink.Pen, sourceX, middleY, destinationX - arrowX, middleY);
                g.FillPolygon(ink.Brush, new Point[] {
                    new Point(destinationX, middleY),
                    new Point(destinationX - arrowX, middleY + arrowY),
                    new Point(destinationX - arrowX, middleY - arrowY - penHalfWidth),
                    new Point(destinationX, middleY - penHalfWidth),
                }, FillMode.Winding);
            } else { //arrows facing right
                g.DrawLine(ink.Pen, sourceX, middleY, destinationX + arrowX, middleY);
                g.FillPolygon(ink.Brush, new Point[] {
                    new Point(destinationX, middleY),
                    new Point(destinationX + fontSize.Width, middleY + arrowY),
                    new Point(destinationX + fontSize.Width, middleY - arrowY - penHalfWidth),
                    new Point(destinationX, middleY - penHalfWidth),
                }, FillMode.Winding);
            }
            var senseLineRect = new Rectangle(Math.Min(sourceX, destinationX), middleY - fontSize.Height / 4 - 1, Math.Abs(destinationX - sourceX), fontSize.Height / 2);
            sensePairs.Add(new SensePair(senseLineRect, message, isMajor: false));

            return sensePairs.AsReadOnly();
        }

    }
}
