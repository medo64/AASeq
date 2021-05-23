using System;
using System.Windows.Forms;

namespace Tipfeler.Gui {
    internal class TextBoxControl : TextBox {

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Control | Keys.A:
                    SelectAll();
                    return true;

                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        protected override void OnGotFocus(EventArgs e) {
            base.OnGotFocus(e);
            SelectAll();
        }

    }
}
