using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Clamito.Gui {
    partial class DocumentControl {

        private class LookAndFeel : IDisposable {

            private LookAndFeel() { }

            public CompositingQuality CompositingQuality { get; private set; }
            public SmoothingMode SmoothingMode { get; private set; }

            public Margins Margins { get; private set; }
            public Padding Spacing { get; private set; }
            public Color? BackColor { get; private set; }

            public DisplayStyle Flow { get; private set; }
            public DisplayStyle Endpoint { get; private set; }
            public DisplayStyle Interaction { get; private set; }


            private static LookAndFeel _screen = new LookAndFeel() {
                CompositingQuality = CompositingQuality.HighQuality,
                SmoothingMode = SmoothingMode.HighSpeed,

                Margins = new Margins(SystemInformation.FrameBorderSize.Width * 2, SystemInformation.FrameBorderSize.Width * 2, SystemInformation.FrameBorderSize.Height * 2, SystemInformation.FrameBorderSize.Height * 2),
                Spacing = new Padding(SystemInformation.FrameBorderSize.Width, SystemInformation.FrameBorderSize.Height, SystemInformation.FrameBorderSize.Width, SystemInformation.FrameBorderSize.Height),

                Endpoint = new DisplayStyle(SystemColors.ControlText, SystemColors.Control,
                                            BlendLow(Color.FromArgb(240, SystemColors.Highlight)), SystemColors.Control,
                                            BlendLow(Color.FromArgb(224, SystemColors.Highlight)), SystemColors.Control,
                                            penWidth: 2,
                                            titleFormat: new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near, Trimming = StringTrimming.EllipsisCharacter },
                                            textFormat: null,
                                            maxCharCount: 16,
                                            paddingCharCount: 2
                                           ),

                Flow = new DisplayStyle(SystemColors.ControlText, SystemColors.Control,
                                        SystemColors.ControlText, BlendHigh(Color.FromArgb(64, SystemColors.Highlight)),
                                        SystemColors.ControlText, BlendHigh(Color.FromArgb(128, SystemColors.Highlight)),
                                        penWidth: 1,
                                        titleFormat: new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter },
                                        textFormat: null,
                                        maxCharCount: 16,
                                        paddingCharCount: 0
                                       ),

                Interaction = new DisplayStyle(SystemColors.ControlText, SystemColors.Control,
                                            BlendLow(Color.FromArgb(240, SystemColors.Highlight)), SystemColors.Control,
                                            BlendLow(Color.FromArgb(224, SystemColors.Highlight)), SystemColors.Control,
                                            penWidth: 2,
                                            titleFormat: new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near, Trimming = StringTrimming.EllipsisCharacter },
                                            textFormat: new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Far, Trimming = StringTrimming.EllipsisCharacter },
                                            maxCharCount: 0,
                                            paddingCharCount: 0
                                           ),
            };
            public static LookAndFeel Screen { get { return LookAndFeel._screen; } }


            public void Dispose() {
            }
        }


        private class DisplayStyle : IDisposable {

            public DisplayStyle(Color normalColor, Color normalBackColor, Color highlightColor, Color highlightBackColor, Color selectedColor, Color selectedBackColor, int penWidth, StringFormat titleFormat, StringFormat textFormat, int maxCharCount, int paddingCharCount) {
                this.NormalInk = new DisplayInk(normalColor, normalBackColor, penWidth);
                this.HighlightInk = new DisplayInk(highlightColor, highlightBackColor, penWidth);
                this.SelectedInk = new DisplayInk(selectedColor, selectedBackColor, penWidth);
                this.TitleFormat = titleFormat;
                this.TextFormat = textFormat;
                this.MaxCharCount = maxCharCount;
                this.PaddingCharCount = paddingCharCount;
            }


            public DisplayInk NormalInk { get; private set; }
            public DisplayInk HighlightInk { get; private set; }
            public DisplayInk SelectedInk { get; private set; }

            public StringFormat TitleFormat { get; private set; }
            public StringFormat TextFormat { get; private set; }

            public int MaxCharCount { get; private set; }
            public int PaddingCharCount { get; private set; }
            public int TotalCharCount { get { return this.MaxCharCount + PaddingCharCount; } }


            public void Dispose() {
                if (this.NormalInk != null) { this.NormalInk.Dispose(); }
                if (this.HighlightInk != null) { this.HighlightInk.Dispose(); }
                if (this.SelectedInk != null) { this.SelectedInk.Dispose(); }
            }
        }


        private class DisplayInk : IDisposable {

            public DisplayInk(Color foreColor, Color backColor, int penWidth) {
                this.Pen = new Pen(foreColor, penWidth);
                this.Brush = new SolidBrush(foreColor);

                var lightForeColor = BlendHigh(Color.FromArgb(128, foreColor));
                this.LightPen = new Pen(lightForeColor, penWidth);
                this.LightBrush = new SolidBrush(lightForeColor);

                this.BackPen = new Pen(backColor, penWidth);
                this.BackBrush = new SolidBrush(backColor);
            }


            public Pen Pen { get; private set; }
            public Brush Brush { get; private set; }
            public Pen LightPen { get; private set; }
            public Brush LightBrush { get; private set; }
            public Pen BackPen { get; private set; }
            public Brush BackBrush { get; private set; }


            public void Dispose() {
                if (this.Pen != null) { this.Pen.Dispose(); }
                if (this.Brush != null) { this.Brush.Dispose(); }
                if (this.LightPen != null) { this.LightPen.Dispose(); }
                if (this.LightBrush != null) { this.LightBrush.Dispose(); }
                if (this.BackPen != null) { this.BackPen.Dispose(); }
                if (this.BackBrush != null) { this.BackBrush.Dispose(); }
            }
        }

    }
}
