using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class MessageForm : Form {
        public MessageForm(Document document, Message message, Interaction insertBefore = null, Endpoint defaultSource = null, Endpoint defaultDestination = null) {
            InitializeComponent();
            Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, cmbSource, cmbDestination, txtCaption, fccData }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }
            erp.SetIconAlignment(fccData, ErrorIconAlignment.TopLeft);

            foreach (var endpoint in document.Endpoints) {
                cmbSource.Items.Add(endpoint);
                cmbDestination.Items.Add(endpoint);
            }

            Document = document;
            Message = message;
            InsertBefore = insertBefore;
            DefaultSource = defaultSource;
            DefaultDestination = defaultDestination;

            if (message == null) {
                Text = "New message";
            } else {
                Text = "Message properties";
            }

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private readonly Document Document;
        private readonly Message Message;
        private readonly Interaction InsertBefore;
        private readonly Endpoint DefaultSource, DefaultDestination;

        private Boolean IsLoaded = false;

        public Message SelectedInteraction { get; private set; }


        private void Form_Load(object sender, EventArgs e) {
            txtName.Text = (Message != null) ? Message.Name : "";
            cmbSource.SelectedItem = (Message != null) ? Message.Source : DefaultSource;
            cmbDestination.SelectedItem = (Message != null) ? Message.Destination : DefaultDestination;
            txtCaption.Text = (Message != null) ? Message.Caption : "";
            fccData.Text = (Message != null) ? Message.Data.ToString() : "";

            IsLoaded = true;
            if (Message == null) { txt_TextChanged(null, null); } //enable OK without change for new items
        }


        private void txt_TextChanged(object sender, EventArgs e) {
            if (!IsLoaded) { return; }

            btnOK.Enabled = GetOutputs(out _, out _, out _, out _, out _);
        }


        private void fccData_Enter(object sender, EventArgs e) {
            AcceptButton = null;
        }

        private void fccData_Leave(object sender, EventArgs e) {
            AcceptButton = btnOK;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            GetOutputs(out var name, out var source, out var destination, out var caption, out var data);
            if (Message == null) { //new
                var interaction = new Message(name, source, destination) { Caption = caption };
                if (InsertBefore == null) {
                    Document.Interactions.Add(interaction);
                } else {
                    Document.Interactions.Insert(Document.Interactions.IndexOf(InsertBefore), interaction);
                }
                SelectedInteraction = interaction;
            } else {
                Message.Name = name;
                Message.ReplaceEndpoints(source, destination);
                Message.Caption = caption;
                SelectedInteraction = Message;
            }
            SelectedInteraction.ReplaceData(data);
        }


        private bool GetOutputs(out string name, out Endpoint source, out Endpoint destination, out string caption, out FieldCollection content) {
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

            caption = txtCaption.Text.Trim();

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
