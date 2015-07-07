using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class MessageForm : Form {
        public MessageForm(Document document, Message message, Interaction insertBefore = null, Endpoint defaultSource = null, Endpoint defaultDestination = null) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, cmbSource, cmbDestination, txtDescription, fccData }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }
            erp.SetIconAlignment(fccData, ErrorIconAlignment.TopLeft);

            foreach (var endpoint in document.Endpoints) {
                cmbSource.Items.Add(endpoint);
                cmbDestination.Items.Add(endpoint);
            }

            this.document = document;
            this.message = message;
            this.insertBefore = insertBefore;
            this.defaultSource = defaultSource;
            this.defaultDestination = defaultDestination;

            if (message == null) {
                this.Text = "New message";
            } else {
                this.Text = "Message properties";
            }

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private readonly Document document;
        private readonly Message message;
        private readonly Interaction insertBefore;
        private readonly Endpoint defaultSource, defaultDestination;

        private Boolean isLoaded = false;

        public Message SelectedInteraction { get; private set; }


        private void Form_Load(object sender, EventArgs e) {
            txtName.Text = (this.message != null) ? this.message.Name : "";
            cmbSource.SelectedItem = (this.message != null) ? this.message.Source : this.defaultSource;
            cmbDestination.SelectedItem = (this.message != null) ? this.message.Destination : this.defaultDestination;
            txtDescription.Text = (this.message != null) ? this.message.Description : "";
            fccData.Text = (this.message != null) ? this.message.Data.ToString() : "";

            this.isLoaded = true;
            if (message == null) { txt_TextChanged(null, null); } //enable OK without change for new items
        }


        private void txt_TextChanged(object sender, EventArgs e) {
            if (!this.isLoaded) { return; }

            string name, description;
            Endpoint source, destination;
            FieldCollection content;
            btnOK.Enabled = GetOutputs(out name, out source, out destination, out content, out description);
        }


        private void txtContent_Enter(object sender, EventArgs e) {
            this.AcceptButton = null;
        }

        private void txtContent_Leave(object sender, EventArgs e) {
            this.AcceptButton = btnOK;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            string name, description;
            Endpoint source, destination;
            FieldCollection content;
            GetOutputs(out name, out source, out destination, out content, out description);
            if (this.message == null) { //new
                var interaction = new Message(name, source, destination) { Description = description };
                if (insertBefore == null) {
                    this.document.Interactions.Add(interaction);
                } else {
                    this.document.Interactions.Insert(this.document.Interactions.IndexOf(this.insertBefore), interaction);
                }
                this.SelectedInteraction = interaction;
            } else {
                this.message.Name = name;
                this.message.ReplaceEndpoints(source, destination);
                this.message.Description = description;
                this.SelectedInteraction = this.message;
            }
            this.SelectedInteraction.ReplaceData(content);
        }


        private bool GetOutputs(out string name, out Endpoint source, out Endpoint destination, out FieldCollection content, out string description) {
            var isValid = true;

            name = txtName.Text.Trim();
            if (Message.IsNameValid(name)) {
                erp.SetError(txtName, null);
            } else {
                erp.SetError(txtName, "Name is not valid.");
                isValid = false;
            }

            source = cmbSource.SelectedItem as Endpoint;
            if (source != null) {
                erp.SetError(cmbSource, null);
            } else {
                erp.SetError(cmbSource, "Message must have a source.");
                isValid = false;
            }

            destination = cmbDestination.SelectedItem as Endpoint;
            if (destination != null) {
                erp.SetError(cmbDestination, null);
            } else {
                erp.SetError(cmbDestination, "Message must have a destination.");
                isValid = false;
            }

            if ((source != null) && (destination != null)) {
                if (source.Equals(destination)) {
                    erp.SetError(cmbSource, "Message must have different source and destination.");
                    erp.SetError(cmbDestination, "Message must have different source and destination.");
                    isValid = false;
                }
            }

            description = txtDescription.Text.Trim();

            content = fccData.Content;
            if (fccData.IsOK) {
                erp.SetError(fccData, null);
            } else {
                erp.SetError(fccData, fccData.ErrorText);
                isValid = false;
            }

            return isValid;
        }

    }
}
