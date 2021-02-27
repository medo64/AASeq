using System;
using System.Windows.Forms;
using Clamito;

namespace Clamito.Gui {
    internal class FieldCollectionControl : TextBoxControl {

        public FieldCollectionControl() {
            AcceptsReturn = true;
            AcceptsTab = true;
            Multiline = true;
            ScrollBars = ScrollBars.Both;
            WordWrap = false;

            IsOK = true;
        }


        private FieldCollection _content;
        public FieldCollection Content {
            get {
                if (_content == null) { _content = new FieldCollection(); }
                return _content;
            }
            set {
                _content = value;
                Text = Content.ToString();
            }
        }


        public bool IsOK { get; private set; }
        public string ErrorText { get; private set; }


        protected override void OnKeyPress(KeyPressEventArgs e) {
            base.OnKeyPress(e);

            if (e.KeyChar == '\t') {
                SelectedText = "    ";
                e.Handled = true;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);

            if (e.KeyData == Keys.Enter) {
                var oldLine = GetLineFromCharIndex(SelectionStart);
                string newText = null;
                try {
                    var parsed = FieldCollection.Parse(Text);
                    _content = parsed;
                    newText = Content.ToString();
                } catch (FormatException) { }
                if (newText != null) {
                    Text = newText;
                    var newLineCharIndex = GetFirstCharIndexFromLine(oldLine);
                    SelectionStart = (newLineCharIndex == -1) ? Text.Length : newLineCharIndex;
                }
            }
        }

        protected override void OnTextChanged(EventArgs e) {
            IsOK = false;
            ErrorText = null;
            try {
                var parsed = FieldCollection.Parse(Text);
                _content = parsed;
                IsOK = true;
            } catch (FormatException ex) {
                var line = ex.Data["Line"];
                if (line != null) {
                    ErrorText = ex.Message + "\nLine " + line.ToString();
                } else {
                    ErrorText = ex.Message;
                }
            }

            base.OnTextChanged(e);
        }

    }
}
