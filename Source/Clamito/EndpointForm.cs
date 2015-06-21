using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class EndpointForm : Form {
        public EndpointForm(Document document, Endpoint endpoint, Endpoint insertBefore = null, ProtocolResolver defaultProtocol = null) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, txtDisplayName, cmbProtocol, txtDescription, fccContent }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }
            erp.SetIconAlignment(fccContent, ErrorIconAlignment.TopLeft);

            cmbProtocol.Items.Add("(none)");
            foreach (var protocol in Engine.Protocols) {
                cmbProtocol.Items.Add(protocol);
            }

            this.document = document;
            this.endpoint = endpoint;
            this.insertBefore = insertBefore;
            if (endpoint == null) {
                this.endpointClone = new Endpoint(this.document.Endpoints.GetUniqueName("Endpoint"), (defaultProtocol != null) ? defaultProtocol.Name : null) { DisplayName = "New endpoint" };
                this.Text = "New endpoint";
            } else {
                this.endpointClone = endpoint.Clone();
                this.Text = "Endpoint properties";
            }
            txtDisplayName.Select();

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private readonly Document document;
        private readonly Endpoint endpoint;
        private readonly Endpoint endpointClone;
        private readonly Endpoint insertBefore;

        private Boolean isLoaded = false;

        public Endpoint SelectedEndpoint { get; private set; }


        private void Form_Load(object sender, EventArgs e) {
            txtName.Text = this.endpointClone.Name;
            txtDisplayName.Text = this.endpointClone.DisplayName;
            txtDescription.Text = this.endpointClone.Description;
            fccContent.Text = (this.endpoint != null) ? endpoint.Properties.ToString() : "";

            if (this.endpointClone.ProtocolName == null) {
                cmbProtocol.SelectedIndex = 0; //select null
            } else {
                foreach (var item in cmbProtocol.Items) {
                    var protocol = item as ProtocolResolver;
                    if ((protocol != null) && protocol.Equals(this.endpointClone.ProtocolName)) {
                        cmbProtocol.SelectedItem = item;
                    }
                }
                if (cmbProtocol.SelectedItem == null) {
                    var nonexistantItem = "(" + this.endpointClone.ProtocolName + "?)";
                    cmbProtocol.Items.Insert(1, nonexistantItem);
                    cmbProtocol.SelectedItem = nonexistantItem;
                }
            }

            this.isLoaded = true;
            txt_TextChanged(null, null);
            if (endpoint != null) { btnOK.Enabled = false; } //don't enable OK for existing items
        }


        private void txt_TextChanged(object sender, EventArgs e) {
            if (!this.isLoaded) { return; }

            string nameError = null;
            string displayNameError = null;
            string protocolError = null;
            string descriptionError = null;
            string contentError = null;

            var name = txtName.Text.Trim();
            var displayName = txtDisplayName.Text.Trim();
            var protocol = cmbProtocol.SelectedItem as ProtocolResolver;
            var description = txtDescription.Text.Trim();

            try { this.endpointClone.Name = name; } catch (Exception ex) { nameError = ex.Message; }
            try { this.endpointClone.DisplayName = displayName; } catch (Exception ex) { displayNameError = ex.Message; }
            try {
                if (cmbProtocol.SelectedIndex == 0) { //null selected
                    this.endpointClone.ProtocolName = null;
                } else if (protocol == null) {
                    protocolError = "Protocol not found!";
                } else {
                    this.endpointClone.ProtocolName = protocol.Name;
                }
            } catch (Exception ex) {
                protocolError = ex.Message;
            }
            try { this.endpointClone.Description = description; } catch (Exception ex) { descriptionError = ex.Message; }

            var endpointByName = this.document.Endpoints[name];
            if (endpoint == null) { //it is a new item
                if (endpointByName != null) {
                    nameError = "Endpoint with same name already exists.";
                }
            } else {
                if ((endpointByName != null) && !endpointByName.Equals(this.endpoint)) {
                    nameError = "Endpoint with same name already exists.";
                }
            }

            if (!fccContent.IsOK) {
                contentError = fccContent.ErrorText;
            }

            erp.SetError(txtName, nameError);
            erp.SetError(txtDisplayName, displayNameError);
            erp.SetError(cmbProtocol, protocolError);
            erp.SetError(txtDescription, descriptionError);
            erp.SetError(fccContent, contentError);

            btnOK.Enabled = (nameError == null) && (displayNameError == null) && (protocolError == null) && (descriptionError == null) && (contentError == null);
        }


        private void txtContent_Enter(object sender, EventArgs e) {
            this.AcceptButton = null;
        }

        private void txtContent_Leave(object sender, EventArgs e) {
            this.AcceptButton = btnOK;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            if (this.endpoint == null) { //new
                if (insertBefore == null) {
                    this.document.Endpoints.Add(this.endpointClone);
                } else {
                    this.document.Endpoints.Insert(this.document.Endpoints.IndexOf(this.insertBefore), this.endpointClone);
                }
                this.SelectedEndpoint = this.endpointClone;
            } else {
                this.endpoint.Name = this.endpointClone.Name;
                this.endpoint.DisplayName = this.endpointClone.DisplayName;
                this.endpoint.ProtocolName = this.endpointClone.ProtocolName;
                this.endpoint.Description = this.endpointClone.Description;
                this.SelectedEndpoint = this.endpoint;
            }
            this.SelectedEndpoint.Properties.Clear();
            foreach (var field in fccContent.Content) {
                this.SelectedEndpoint.Properties.Add(field.Clone());
            }
        }

    }
}
