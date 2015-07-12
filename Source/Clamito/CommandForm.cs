using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class CommandForm : Form {
        public CommandForm(Document document, Command command, Interaction insertBefore = null) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, txtDescription, fccData }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }
            erp.SetIconAlignment(fccData, ErrorIconAlignment.TopLeft);

            this.document = document;
            this.command = command;
            this.insertBefore = insertBefore;

            if (command == null) {
                this.Text = "New command";
            } else {
                this.Text = "Command properties";
            }

            foreach (var cmd in Plugin.Commands) {
                txtName.AutoCompleteCustomSource.Add(cmd.Name);
            }

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private readonly Document document;
        private readonly Command command;
        private readonly Interaction insertBefore;

        private Boolean isLoaded = false;

        public Interaction SelectedInteraction { get; private set; }


        private void Form_Load(object sender, EventArgs e) {
            txtName.Text = (this.command != null) ? this.command.Name : "";
            txtDescription.Text = (this.command != null) ? this.command.Description : "";
            fccData.Text = (this.command != null) ? this.command.Data.ToString() : "";

            this.isLoaded = true;
            if (command == null) { txt_TextChanged(null, null); } //enable OK without change for new items
        }


        private void txt_TextChanged(object sender, EventArgs e) {
            if (!this.isLoaded) { return; }

            string name, description;
            FieldCollection data;
            btnOK.Enabled = GetOutputs(out name, out description, out data);
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
            this.AcceptButton = null;
        }

        private void fccData_Leave(object sender, EventArgs e) {
            this.AcceptButton = btnOK;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            string name, description;
            FieldCollection data;
            GetOutputs(out name, out description, out data);
            if (this.command == null) { //new
                var interaction = new Command(name) { Description = description };
                if (insertBefore == null) {
                    this.document.Interactions.Add(interaction);
                } else {
                    this.document.Interactions.Insert(this.document.Interactions.IndexOf(this.insertBefore), interaction);
                }
                this.SelectedInteraction = interaction;
            } else {
                this.command.Name = name;
                this.command.Description = description;
                this.SelectedInteraction = this.command;
            }
            this.SelectedInteraction.ReplaceData(data);
        }


        private bool GetOutputs(out string name, out string description, out FieldCollection content) {
            var isValid = true;

            name = txtName.Text.Trim();
            var cmd = Plugin.Commands[name];
            if (cmd != null) {
                erp.SetError(txtName, null);
            } else {
                erp.SetError(txtName, "Command name is not valid.");
                isValid = false;
            }

            description = txtDescription.Text.Trim();

            content = fccData.Content;
            if (fccData.IsOK) {
                if (cmd != null) {
                    var validationResults = cmd.ValidateData(content);
                    if (validationResults.HasWarnings || validationResults.HasErrors) {
                        erp.SetError(fccData, validationResults[0].Text);
                        isValid = false;
                    } else {
                        erp.SetError(fccData, null);
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
