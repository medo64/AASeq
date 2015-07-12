using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class EndpointForm : Form {
        public EndpointForm(Document document, Endpoint endpoint, Endpoint insertBefore = null, ProtocolPlugin defaultProtocol = null) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, cmbProtocol, txtCaption, fccData }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }
            erp.SetIconAlignment(fccData, ErrorIconAlignment.TopLeft);

            cmbProtocol.Items.Add("(none)");
            foreach (var protocol in Plugin.Protocols) {
                cmbProtocol.Items.Add(protocol);
            }

            this.document = document;
            this.endpoint = endpoint;
            this.insertBefore = insertBefore;
            if (endpoint == null) {
                this.endpointClone = new Endpoint(this.document.Endpoints.GetUniqueName("Endpoint"), (defaultProtocol != null) ? defaultProtocol.Name : null) { Caption = (defaultProtocol != null) ? "New " + defaultProtocol.DisplayName : "New endpoint" };
                this.Text = "New endpoint";
            } else {
                this.endpointClone = endpoint.Clone();
                this.Text = "Endpoint properties";
            }
            txtCaption.Select();

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
            txtCaption.Text = this.endpointClone.Caption;
            fccData.Text = (this.endpoint != null) ? endpoint.Data.ToString() : "";

            if (this.endpointClone.ProtocolName == null) {
                cmbProtocol.SelectedIndex = 0; //select null
            } else {
                foreach (var item in cmbProtocol.Items) {
                    var protocol = item as ProtocolPlugin;
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
            string protocolError = null;
            string captionError = null;
            string dataError = null;

            var name = txtName.Text.Trim();
            var protocol = cmbProtocol.SelectedItem as ProtocolPlugin;
            var caption = txtCaption.Text.Trim();

            try { this.endpointClone.Name = name; } catch (Exception ex) { nameError = ex.Message; }
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
            try { this.endpointClone.Caption = caption; } catch (Exception ex) { captionError = ex.Message; }

            var endpointByName = this.document.Endpoints[name];
            if (endpoint == null) { //it is a new item
                if (endpointByName != null) {
                    nameError = "Endpoint with the same name already exists.";
                }
            } else {
                if ((endpointByName != null) && !endpointByName.Equals(this.endpoint)) {
                    nameError = "Endpoint with the same name already exists.";
                }
            }

            if (!fccData.IsOK) {
                dataError = fccData.ErrorText;
            }

            erp.SetError(txtName, nameError);
            erp.SetError(cmbProtocol, protocolError);
            erp.SetError(txtCaption, captionError);
            erp.SetError(fccData, dataError);

            btnOK.Enabled = (nameError == null) && (protocolError == null) && (captionError == null) && (dataError == null);
        }


        private void fccData_Enter(object sender, EventArgs e) {
            this.AcceptButton = null;
        }

        private void fccData_Leave(object sender, EventArgs e) {
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
                this.endpoint.ProtocolName = this.endpointClone.ProtocolName;
                this.endpoint.Caption = this.endpointClone.Caption;
                this.SelectedEndpoint = this.endpoint;
            }
            this.SelectedEndpoint.Data.Clear();
            foreach (var field in fccData.Content) {
                this.SelectedEndpoint.Data.Add(field.Clone());
            }
        }

    }
}
