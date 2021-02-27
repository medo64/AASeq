using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class CommandForm : Form {
        public CommandForm(Document document, Command command, Interaction insertBefore = null) {
            InitializeComponent();
            Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, txtCaption, fccData }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }
            erp.SetIconAlignment(fccData, ErrorIconAlignment.TopLeft);

            Document = document;
            Command = command;
            InsertBefore = insertBefore;

            if (command == null) {
                Text = "New command";
            } else {
                Text = "Command properties";
            }

            foreach (var cmd in Plugin.Commands) {
                txtName.AutoCompleteCustomSource.Add(cmd.Name);
            }

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private readonly Document Document;
        private readonly Command Command;
        private readonly Interaction InsertBefore;

        private Boolean IsLoaded = false;

        public Interaction SelectedInteraction { get; private set; }


        private void Form_Load(object sender, EventArgs e) {
            txtName.Text = (Command != null) ? Command.Name : "";
            txtCaption.Text = (Command != null) ? Command.Caption : "";
            fccData.Text = (Command != null) ? Command.Data.ToString() : "";

            IsLoaded = true;
            if (Command == null) { txt_TextChanged(null, null); } //enable OK without change for new items
        }


        private void txt_TextChanged(object sender, EventArgs e) {
            if (!IsLoaded) { return; }
            btnOK.Enabled = GetOutputs(out _, out _, out _);
        }


        private void txtName_Leave(object sender, EventArgs e) {
            var cmd = Plugin.Commands[txtName.Text];
            if (cmd != null) {
                if (!txtName.Text.Equals(cmd.Name)) { txtName.Text = cmd.Name; }
                if (fccData.TextLength == 0) {
                    fccData.Text = cmd.GetDefaultData().ToString();
                }
            }
        }


        private void fccData_Enter(object sender, EventArgs e) {
            AcceptButton = null;
        }

        private void fccData_Leave(object sender, EventArgs e) {
            AcceptButton = btnOK;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            GetOutputs(out var name, out var caption, out var data);
            if (Command == null) { //new
                var interaction = new Command(name) { Caption = caption };
                if (InsertBefore == null) {
                    Document.Interactions.Add(interaction);
                } else {
                    Document.Interactions.Insert(Document.Interactions.IndexOf(InsertBefore), interaction);
                }
                SelectedInteraction = interaction;
            } else {
                Command.Name = name;
                Command.Caption = caption;
                SelectedInteraction = Command;
            }
            SelectedInteraction.ReplaceData(data);
        }


        private bool GetOutputs(out string name, out string caption, out FieldCollection content) {
            var isValid = true;

            name = txtName.Text.Trim();
            var cmd = Plugin.Commands[name];
            if (cmd != null) {
                erp.SetError(txtName, null);
            } else {
                erp.SetError(txtName, "Command name is not valid.");
                isValid = false;
            }

            caption = txtCaption.Text.Trim();

            content = fccData.Content;
            if (fccData.IsOK) {
                if (cmd != null) {
                    erp.SetError(fccData, null);
                    foreach (var failure in cmd.ValidateData(content)) {
                        erp.SetError(fccData, failure.Text);
                        isValid = false;
                        break;
                    }
                } else {
                    erp.SetError(fccData, null);
                }
            } else {
                erp.SetError(fccData, fccData.ErrorText);
                isValid = false;
            }

            return isValid;
        }

    }
}
