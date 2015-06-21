using System;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal class FieldCollectionControl : TextBoxControl {

        public FieldCollectionControl() {
            this.AcceptsReturn = true;
            this.AcceptsTab = true;
            this.Multiline = true;
            this.ScrollBars = ScrollBars.Both;
            this.WordWrap = false;

            this.IsOK = true;
        }


        private FieldCollection _content;
        public FieldCollection Content {
            get {
                if (this._content == null) { this._content = new FieldCollection(); }
                return this._content;
            }
            set {
                this._content = value;
                this.Text = this.Content.ToString();
            }
        }


        public bool IsOK { get; private set; }
        public string ErrorText { get; private set; }


        protected override void OnKeyPress(KeyPressEventArgs e) {
            base.OnKeyPress(e);

            if (e.KeyChar == '\t') {
                this.SelectedText = "    ";
                e.Handled = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);

            if (e.KeyData == Keys.Enter) {
                var oldLine = this.GetLineFromCharIndex(this.SelectionStart);
                string newText = null;
                try {
                    var parsed = FieldCollection.Parse(this.Text);
                    this._content = parsed;
                    newText = this.Content.ToString();
                } catch (FormatException) { }
                if (newText != null) {
                    this.Text = newText;
                    var newLineCharIndex = this.GetFirstCharIndexFromLine(oldLine);
                    this.SelectionStart = (newLineCharIndex == -1) ? this.Text.Length : newLineCharIndex;
                }
            }
        }

        protected override void OnTextChanged(EventArgs e) {
            this.IsOK = false;
            this.ErrorText = null;
            try {
                var parsed = FieldCollection.Parse(this.Text);
                this._content = parsed;
                this.IsOK = true;
            } catch (FormatException ex) {
                var line = ex.Data["Line"];
                if (line != null) {
                    this.ErrorText = ex.Message + "\nLine " + line.ToString();
                } else {
                    this.ErrorText = ex.Message;
                }
            }

            base.OnTextChanged(e);
        }

    }
}
