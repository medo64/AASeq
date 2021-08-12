using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Tipfeler.Gui {
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
                NormalInk = new DisplayInk(normalColor, normalBackColor, penWidth);
                HighlightInk = new DisplayInk(highlightColor, highlightBackColor, penWidth);
                SelectedInk = new DisplayInk(selectedColor, selectedBackColor, penWidth);
                TitleFormat = titleFormat;
                TextFormat = textFormat;
                MaxCharCount = maxCharCount;
                PaddingCharCount = paddingCharCount;
            }


            public DisplayInk NormalInk { get; private set; }
            public DisplayInk HighlightInk { get; private set; }
            public DisplayInk SelectedInk { get; private set; }

            public StringFormat TitleFormat { get; private set; }
            public StringFormat TextFormat { get; private set; }

            public int MaxCharCount { get; private set; }
            public int PaddingCharCount { get; private set; }
            public int TotalCharCount { get { return MaxCharCount + PaddingCharCount; } }


            public void Dispose() {
                if (NormalInk != null) { NormalInk.Dispose(); }
                if (HighlightInk != null) { HighlightInk.Dispose(); }
                if (SelectedInk != null) { SelectedInk.Dispose(); }
            }
        }


        private class DisplayInk : IDisposable {

            public DisplayInk(Color foreColor, Color backColor, int penWidth) {
                Pen = new Pen(foreColor, penWidth);
                Brush = new SolidBrush(foreColor);

                var lightForeColor = BlendHigh(Color.FromArgb(128, foreColor));
                LightPen = new Pen(lightForeColor, penWidth);
                LightBrush = new SolidBrush(lightForeColor);

                BackPen = new Pen(backColor, penWidth);
                BackBrush = new SolidBrush(backColor);
            }


            public Pen Pen { get; private set; }
            public Brush Brush { get; private set; }
            public Pen LightPen { get; private set; }
            public Brush LightBrush { get; private set; }
            public Pen BackPen { get; private set; }
            public Brush BackBrush { get; private set; }


            public void Dispose() {
                if (Pen != null) { Pen.Dispose(); }
                if (Brush != null) { Brush.Dispose(); }
                if (LightPen != null) { LightPen.Dispose(); }
                if (LightBrush != null) { LightBrush.Dispose(); }
                if (BackPen != null) { BackPen.Dispose(); }
                if (BackBrush != null) { BackBrush.Dispose(); }
            }
        }

    }
}
