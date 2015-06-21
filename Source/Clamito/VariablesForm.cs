using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class VariablesForm : Form {
        public VariablesForm(Document document) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            erp.SetIconAlignment(fccContent, ErrorIconAlignment.TopLeft);
            erp.SetIconPadding(fccContent, SystemInformation.BorderSize.Width);

            this.document = document;

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private readonly Document document;

        private Boolean isLoaded = false;


        private void Form_Load(object sender, EventArgs e) {
            fccContent.Content = this.document.Variables;

            this.isLoaded = true;
        }


        private void fccContent_Enter(object sender, EventArgs e) {
            this.AcceptButton = null;
        }

        private void fccContent_Leave(object sender, EventArgs e) {
            this.AcceptButton = btnOK;
        }

        private void fccContent_TextChanged(object sender, EventArgs e) {
            if (!this.isLoaded) { return; }

            if (!fccContent.IsOK) {
                erp.SetError(fccContent, "Header fields are not supported in this context.");
            } else {
                erp.SetError(fccContent, fccContent.ErrorText);
            }

            btnOK.Enabled = fccContent.IsOK;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            this.document.Variables.Clear();
            foreach (var field in fccContent.Content) {
                this.document.Variables.Add(field.Clone());
            }
        }

    }
}
