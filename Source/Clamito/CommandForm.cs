using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class CommandForm : Form {
        public CommandForm(Document document, Command command, Interaction insertBefore = null) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, txtParameters, txtDescription }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }

            this.document = document;
            this.command = command;
            this.insertBefore = insertBefore;

            if (command == null) {
                this.Text = "New command";
            } else {
                this.Text = "Command properties";
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
            txtParameters.Text = (this.command != null) ? this.command.Parameters : "";
            txtDescription.Text = (this.command != null) ? this.command.Description : "";

            this.isLoaded = true;
            if (command == null) { txt_TextChanged(null, null); } //enable OK without change for new items
        }


        private void txt_TextChanged(object sender, EventArgs e) {
            if (!this.isLoaded) { return; }

            string name, parameters, description;
            btnOK.Enabled = GetOutputs(out name, out parameters, out description);
        }


        private void btnOK_Click(object sender, EventArgs e) {
            string name, parameters, description;
            GetOutputs(out name, out parameters, out description);
            if (this.command == null) { //new
                var interaction = new Command(name, parameters) { Description = description };
                if (insertBefore == null) {
                    this.document.Interactions.Add(interaction);
                } else {
                    this.document.Interactions.Insert(this.document.Interactions.IndexOf(this.insertBefore), interaction);
                }
                this.SelectedInteraction = interaction;
            } else {
                this.command.Name = name;
                this.command.Parameters = parameters;
                this.command.Description = description;
                this.SelectedInteraction = this.command;
            }
        }


        private bool GetOutputs(out string name, out string parameters, out string description) {
            var isValid = true;

            name = txtName.Text.Trim();
            if (Command.IsNameValid(name)) {
                erp.SetError(txtName, null);
            } else {
                erp.SetError(txtName, "Name is not valid.");
                isValid = false;
            }

            parameters = txtParameters.Text.Trim();
            description = txtDescription.Text.Trim();

            return isValid;
        }

    }
}
