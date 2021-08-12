using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Tipfeler.Gui {
    internal static class Helper {

        #region Toolstrip renderer

        public static readonly ToolStripProfessionalRenderer ToolstripRenderer = new ToolStripBorderlessProfessionalRenderer();
        private class ToolStripBorderlessProfessionalRenderer : ToolStripProfessionalRenderer {
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) {
                //base.OnRenderToolStripBorder(e);
            }
        }

        #endregion


        #region Toolstrip images

        internal static void ScaleToolstrip(params ToolStrip[] toolstrips) {
            var sizeAndSet = GetSizeAndSet(toolstrips);
            var size = sizeAndSet.Item1;
            var set = sizeAndSet.Item2;

            var resources = Tipfeler.Gui.Properties.Resources.ResourceManager;
            foreach (var toolstrip in toolstrips) {
                toolstrip.ImageScalingSize = new Size(size, size);
                foreach (ToolStripItem item in toolstrip.Items) {
                    if (item.Image != null) { //update only those already having image
                        Bitmap bitmap = null;
                        if (!string.IsNullOrEmpty(item.Name)) {
                            bitmap = resources.GetObject(item.Name + set) as Bitmap;
                        }
                        if ((bitmap == null) && !string.IsNullOrEmpty(item.Tag as string)) {
                            bitmap = resources.GetObject(item.Tag + set) as Bitmap;
                        }
                        item.ImageScaling = ToolStripItemImageScaling.None;
#if DEBUG
                        item.Image = (bitmap != null) ? new Bitmap(bitmap, size, size) : new Bitmap(size, size, PixelFormat.Format8bppIndexed);
#else
                        if (bitmap != null) { item.Image = new Bitmap(bitmap, size, size) ; }
#endif
                    }

                    var toolstripSplitButton = item as ToolStripSplitButton;
                    if (toolstripSplitButton != null) { ScaleToolstrip(toolstripSplitButton.DropDown); }
                }
            }
        }

        private static Tuple<int, string> GetSizeAndSet(params Control[] controls) {
            using (var g = controls[0].CreateGraphics()) {
                var scale = Math.Max(Math.Max(g.DpiX, g.DpiY), 96.0) / 96.0;

                if (scale < 1.5) {
                    return new Tuple<int, string>(16, "_16");
                } else if (scale < 2) {
                    return new Tuple<int, string>(24, "_24");
                } else if (scale < 3) {
                    return new Tuple<int, string>(32, "_32");
                } else {
                    var base32 = 16 * scale / 32;
                    var base48 = 16 * scale / 48;
                    if ((base48 - (int)base48) < (base32 - (int)base32)) {
                        return new Tuple<int, string>(48 * (int)base48, "_48");
                    } else {
                        return new Tuple<int, string>(32 * (int)base32, "_32");
                    }
                }
            }
        }

        #endregion

    }
}
