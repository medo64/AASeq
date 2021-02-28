using System;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class EndpointForm : Form {
        public EndpointForm(Document document, Endpoint endpoint, Endpoint insertBefore = null, ProtocolPlugin defaultProtocol = null) {
            InitializeComponent();
            Font = SystemFonts.MessageBoxFont;
            foreach (var control in new Control[] { txtName, cmbProtocol, txtCaption, fccData }) {
                erp.SetIconAlignment(control, ErrorIconAlignment.MiddleLeft);
                erp.SetIconPadding(control, SystemInformation.BorderSize.Width);
            }
            erp.SetIconAlignment(fccData, ErrorIconAlignment.TopLeft);

            cmbProtocol.Items.Add("(none)");
            foreach (var protocol in Plugin.Protocols) {
                cmbProtocol.Items.Add(protocol);
            }

            Document = document;
            Endpoint = endpoint;
            InsertBefore = insertBefore;
            if (endpoint == null) {
                EndpointClone = new Endpoint(document.Endpoints.GetUniqueName("Endpoint"), defaultProtocol?.Name) { Caption = (defaultProtocol != null) ? "New " + defaultProtocol.DisplayName : "New endpoint" };
                Text = "New endpoint";
            } else {
                EndpointClone = endpoint.Clone();
                Text = "Endpoint properties";
            }
            txtCaption.Select();

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private readonly Document Document;
        private readonly Endpoint Endpoint;
        private readonly Endpoint EndpointClone;
        private readonly Endpoint InsertBefore;

        private Boolean IsLoaded = false;

        public Endpoint SelectedEndpoint { get; private set; }


        private void Form_Load(object sender, EventArgs e) {
            txtName.Text = EndpointClone.Name;
            txtCaption.Text = EndpointClone.Caption;
            fccData.Text = (Endpoint != null) ? Endpoint.Data.ToString() : "";

            if (EndpointClone.ProtocolName == null) {
                cmbProtocol.SelectedIndex = 0; //select null
            } else {
                foreach (var item in cmbProtocol.Items) {
                    if ((item is ProtocolPlugin protocol) && protocol.Equals(EndpointClone.ProtocolName)) {
                        cmbProtocol.SelectedItem = item;
                    }
                }
                if (cmbProtocol.SelectedItem == null) {
                    var nonexistantItem = "(" + EndpointClone.ProtocolName + "?)";
                    cmbProtocol.Items.Insert(1, nonexistantItem);
                    cmbProtocol.SelectedItem = nonexistantItem;
                }
            }

            IsLoaded = true;
            txt_TextChanged(null, null);
            if (Endpoint != null) { btnOK.Enabled = false; } //don't enable OK for existing items
        }


        private void txt_TextChanged(object sender, EventArgs e) {
            if (!IsLoaded) { return; }

            string nameError = null;
            string protocolError = null;
            string captionError = null;
            string dataError = null;

            var name = txtName.Text.Trim();
            var caption = txtCaption.Text.Trim();

            try { EndpointClone.Name = name; } catch (Exception ex) { nameError = ex.Message; }
            try {
                if (cmbProtocol.SelectedIndex == 0) { //null selected
                    EndpointClone.ProtocolName = null;
                } else if (cmbProtocol.SelectedItem is not ProtocolPlugin protocol) {
                    protocolError = "Protocol not found!";
                } else {
                    EndpointClone.ProtocolName = protocol.Name;
                }
            } catch (Exception ex) {
                protocolError = ex.Message;
            }
            try { EndpointClone.Caption = caption; } catch (Exception ex) { captionError = ex.Message; }

            var endpointByName = Document.Endpoints[name];
            if (Endpoint == null) { //it is a new item
                if (endpointByName != null) {
                    nameError = "Endpoint with the same name already exists.";
                }
            } else {
                if ((endpointByName != null) && !endpointByName.Equals(Endpoint)) {
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
            AcceptButton = null;
        }

        private void fccData_Leave(object sender, EventArgs e) {
            AcceptButton = btnOK;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            if (Endpoint == null) { //new
                if (InsertBefore == null) {
                    Document.Endpoints.Add(EndpointClone);
                } else {
                    Document.Endpoints.Insert(Document.Endpoints.IndexOf(InsertBefore), EndpointClone);
                }
                SelectedEndpoint = EndpointClone;
            } else {
                Endpoint.Name = EndpointClone.Name;
                Endpoint.ProtocolName = EndpointClone.ProtocolName;
                Endpoint.Caption = EndpointClone.Caption;
                SelectedEndpoint = Endpoint;
            }
            SelectedEndpoint.Data.Clear();
            foreach (var field in fccData.Content) {
                SelectedEndpoint.Data.Add(field.Clone());
            }
        }

    }
}
