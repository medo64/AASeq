using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Clamito.Gui {

    internal partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            mnu.Renderer = Helper.ToolstripRenderer;
            Helper.ScaleToolstrip(mnu);
            doc.Font = SystemFonts.MessageBoxFont;
            this.Font = SystemFonts.MessageBoxFont;

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);

            mnuNew_Click(null, null);
        }


        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Alt | Keys.Menu:
                case Keys.F10:
                    if (mnu.ContainsFocus) {
                        doc.Select();
                    } else {
                        mnu.Select();
                        mnu.Items[0].Select();
                    }
                    return true;

                case Keys.Alt | Keys.N:
                    mnuNew.Select();
                    return true;

                case Keys.Control | Keys.N:
                    mnuNew.PerformClick();
                    return true;

                case Keys.Alt | Keys.O:
                    mnuOpenDefault.Select();
                    mnuOpenDefault.ShowDropDown();
                    mnuOpen.Select();
                    return true;

                case Keys.Control | Keys.O:
                    mnuOpenDefault.PerformButtonClick();
                    return true;

                case Keys.Alt | Keys.S:
                    mnuSaveDefault.Select();
                    mnuSaveDefault.ShowDropDown();
                    if (mnuSave.Enabled) {
                        mnuSave.Select();
                    } else {
                        mnuSaveAs.Select();
                    }
                    return true;

                case Keys.Control | Keys.S:
                    mnuSaveDefault.PerformButtonClick();
                    return true;

                case Keys.Alt | Keys.E:
                    if (mnuEndpointAddDefault.Enabled) {
                        mnuEndpointAddDefault.ShowDropDown();
                        mnuEndpointAddDefault.Select();
                    }
                    return true;

                case Keys.Alt | Keys.I:
                    if (mnuInteractionAdd.Enabled) {
                        mnuInteractionAdd.ShowDropDown();
                        mnuInteractionAddMessage.Select();
                    }
                    return true;

                case Keys.F1:
                    mnuApp.ShowDropDown();
                    return true;

                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private Document Document = new Document();
        private string DocumentFileName = null;
        private readonly Medo.Configuration.RecentFiles Recent = new Medo.Configuration.RecentFiles();


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!ProceedWithNewDocument()) {
                e.Cancel = true;
            }
        }


        private void doc_Changed(object sender, EventArgs e) {
            mnuEndpointRemove.Enabled = (doc.SelectedEndpoint != null);
            mnuEndpointProperties.Enabled = (doc.SelectedEndpoint != null);

            mnuInteractionAdd.Enabled = true;
            mnuInteractionAddMessage.Enabled = (this.Document.Endpoints.Count >= 2);
            mnuInteractionAddCommand.Enabled = true;
            mnuInteractionRemove.Enabled = (doc.SelectedInteraction != null);
            mnuInteractionProperties.Enabled = (doc.SelectedInteraction != null);
        }


        #region Menu

        private void mnuNew_Click(object sender, EventArgs e) {
            if (ProceedWithNewDocument()) {
                this.Document = new Document(new Endpoint[] { new Endpoint("Me") }, null);
                this.Document.Changed += delegate (object sender2, EventArgs e2) { this.RefreshTitle(); };
                this.RefreshDocument();
            }
        }

        private void mnuOpenDefault_DropDownOpening(object sender, EventArgs e) {
            for (int i = mnuOpenDefault.DropDownItems.Count - 1; i >= 1; i--) { //remove old ones
                mnuOpenDefault.DropDownItems.RemoveAt(i);
            }

            if (this.Recent.Count > 0) {
                mnuOpenDefault.DropDownItems.Add(new ToolStripSeparator());
                foreach (var file in this.Recent.Items) {
                    if (File.Exists(file.FileName)) {
                        mnuOpenDefault.DropDownItems.Add(file.Title, null, delegate (object sender2, EventArgs e2) {
                            if (ProceedWithNewDocument()) {
                                OpenFile(file.FileName);
                            }
                        });
                    }
                }
            }
        }

        private void mnuOpen_Click(object sender, EventArgs e) {
            if (ProceedWithNewDocument()) {
                using (var frm = new OpenFileDialog() { Filter = "Clamito documents (*.clamito)|*.clamito|All files (*.*)|*.*" }) {
                    if (frm.ShowDialog(this) == DialogResult.OK) {
                        OpenFile(frm.FileName);
                    }
                }
            }
        }

        private bool ProceedWithNewDocument() {
            if (this.Document.IsChanged == false) { return true; }

            switch (Medo.MessageBox.ShowQuestion(this, "Current document has not been saved. Do you wish to save it now?", MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1)) {
                case DialogResult.Yes:
                    mnuSaveDefault_ButtonClick(null, null);
                    return (this.Document.IsChanged == false);

                case DialogResult.No: return true;
            }
            return false;
        }

        private void OpenFile(string fileName) {
            try {
                this.Document = Document.Load(File.OpenRead(fileName));
                this.DocumentFileName = fileName;
                this.Document.Changed += delegate (object sender2, EventArgs e2) { this.RefreshTitle(); };
                RefreshDocument();
                this.Recent.Push(fileName);
            } catch (Exception ex) {
                Medo.MessageBox.ShowError(this, "Cannot open " + Path.GetFileName(fileName) + "!\n\n" + ex.Message);
            }
        }

        private void mnuSaveDefault_ButtonClick(object sender, EventArgs e) {
            if (this.DocumentFileName != null) {
                mnuSave_Click(null, null);
            } else {
                mnuSaveAs_Click(null, null);
            }
        }

        private void mnuSaveDefault_DropDownOpening(object sender, EventArgs e) {
            mnuSave.Enabled = (this.DocumentFileName != null);
        }

        private void mnuSave_Click(object sender, EventArgs e) {
            if (this.DocumentFileName == null) { return; }

            using (var stream = File.OpenWrite(this.DocumentFileName)) {
                stream.SetLength(0);
                this.Document.Save(stream);
            }
            this.RefreshTitle();
            this.Recent.Push(this.DocumentFileName); //to have it move to front in case of multiple concurent instances
        }

        private void mnuSaveAs_Click(object sender, EventArgs e) {
            using (var frm = new SaveFileDialog() { AddExtension = true, DefaultExt = "clamito", Filter = "Clamito documents (*.clamito)|*.clamito|All files (*.*)|*.*" }) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    try {
                        using (var stream = File.OpenWrite(frm.FileName)) {
                            stream.SetLength(0);
                            this.Document.Save(stream);
                            this.DocumentFileName = frm.FileName;
                        }
                        this.RefreshTitle();
                        this.Recent.Push(frm.FileName);
                    } catch (Exception ex) {
                        Medo.MessageBox.ShowError(this, "Cannot save " + Path.GetFileName(frm.FileName) + "!\n\n" + ex.Message);
                    }
                }
            }
        }


        private void mnuEndpoint_DropDownOpening(object sender, EventArgs e) {
            for (int i = mnuEndpointAddDefault.DropDownItems.Count - 1; i >= 0; i--) {
                if (string.IsNullOrEmpty(mnuEndpointAddDefault.DropDownItems[i].Name)) {
                    mnuEndpointAddDefault.DropDownItems.RemoveAt(i);
                }
            }

            foreach (var protocol in Plugin.Protocols) {
                var item = new ToolStripMenuItem("Add " + protocol.DisplayName + " endpoint", null, mnuEndpointAdd_Click) { Tag = protocol };
                mnuEndpointAddDefault.DropDownItems.Add(item);
            }
        }

        private void mnuEndpointAdd_Click(object sender, EventArgs e) {
            var item = sender as ToolStripItem;
            using (var frm = new EndpointForm(this.Document, null, GetNextEndpoint(doc.Document, doc.SelectedEndpoint), item.Tag as ProtocolPlugin)) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    doc.SelectedEndpoint = frm.SelectedEndpoint;
                }
            }
        }

        private void mnuEndpointRemove_Click(object sender, EventArgs e) {
            if (doc.SelectedEndpoint != null) {
                var nextIndex = this.Document.Endpoints.IndexOf(doc.SelectedEndpoint);
                this.Document.Endpoints.Remove(doc.SelectedEndpoint);
                if (nextIndex >= this.Document.Endpoints.Count) { nextIndex--; }
                if (this.Document.Endpoints.Count > 0) {
                    doc.SelectedEndpoint = this.Document.Endpoints[nextIndex];
                } else {
                    doc.SelectedEndpoint = null;
                }
            }
        }

        private void mnuEndpointProperties_Click(object sender, EventArgs e) {
            if (doc.SelectedEndpoint != null) {
                using (var frm = new EndpointForm(this.Document, doc.SelectedEndpoint)) {
                    if (frm.ShowDialog(this) == DialogResult.OK) {
                        doc.SelectedEndpoint = frm.SelectedEndpoint;
                    }
                }
            }
        }



        private void mnuInteractionAdd_ButtonClick(object sender, EventArgs e) {
            if (mnuInteractionAddMessage.Enabled) {
                mnuInteractionAddContent_Click(null, null);
            } else {
                mnuInteractionAdd.ShowDropDown();
            }
        }

        private void mnuInteractionAddContent_Click(object sender, EventArgs e) {
            if (this.Document.Endpoints.Count >= 2) {
                using (var frm = new MessageForm(this.Document, null, GetNextInteraction(doc.Document, doc.SelectedInteraction))) {
                    if (frm.ShowDialog(this) == DialogResult.OK) {
                        doc.SelectedInteraction = frm.SelectedInteraction;
                    }
                }
            }
        }

        private void mnuInteractionAddCommand_Click(object sender, EventArgs e) {
            if (this.Document.Endpoints.Count >= 1) {
                using (var frm = new CommandForm(this.Document, null, GetNextInteraction(doc.Document, doc.SelectedInteraction))) {
                    if (frm.ShowDialog(this) == DialogResult.OK) {
                        doc.SelectedInteraction = frm.SelectedInteraction;
                    }
                }
            }
        }

        private void mnuInteractionRemove_Click(object sender, EventArgs e) {
            if (doc.SelectedInteraction != null) {
                var nextIndex = doc.Document.Interactions.IndexOf(doc.SelectedInteraction);
                doc.Document.Interactions.Remove(doc.SelectedInteraction);
                if (nextIndex >= doc.Document.Interactions.Count) { nextIndex--; }
                if (doc.Document.Interactions.Count > 0) {
                    doc.SelectedInteraction = doc.Document.Interactions[nextIndex];
                } else {
                    doc.SelectedInteraction = null;
                }
            }
        }

        private void mnuInteractionProperties_Click(object sender, EventArgs e) {
            if (doc.SelectedInteraction != null) {
                if (doc.SelectedInteraction.IsMessage) {
                    using (var frm = new MessageForm(this.Document, (Message)doc.SelectedInteraction)) {
                        if (frm.ShowDialog(this) == DialogResult.OK) {
                            doc.SelectedInteraction = frm.SelectedInteraction;
                        }
                    }
                }
            }
        }


        private void mnuExecute_Click(object sender, EventArgs e) {
            using (var frm = new ExecuteForm(this.Document)) {
                frm.ShowDialog(this);
            }
        }


        private void mnuAppFeedback_Click(object sender, EventArgs e) {
            Medo.Diagnostics.ErrorReport.ShowDialog(this, null, new Uri("http://jmedved.com/feedback/"));
        }

        private void mnuAppUpdate_Click(object sender, EventArgs e) {
            Medo.Services.Upgrade.ShowDialog(this, new Uri("http://jmedved.com/upgrade/"));
        }

        private void mnuAppDonate_Click(object sender, EventArgs e) {
            Process.Start("http://jmedved.com/donate/");
        }

        private void mnuAppAbout_Click(object sender, EventArgs e) {
            Medo.Windows.Forms.AboutBox.ShowDialog(this, new Uri("http://www.jmedved.com/clamito/"));
        }

        #endregion


        #region Refresh

        private void RefreshDocument() {
            doc.Document = this.Document;
            RefreshTitle();
        }

        private void RefreshTitle() {
            if (this.DocumentFileName != null) {
                this.Text = Path.GetFileNameWithoutExtension(this.DocumentFileName) + (this.Document.IsChanged ? "*" : "") + " - Clamito";
            } else {
                this.Text = "Clamito" + (this.Document.IsChanged ? "*" : "");
            }
        }

        #endregion


        #region Helper

        private static Interaction GetNextInteraction(Document document, Interaction currentInteraction) {
            if (currentInteraction != null) {
                var nextIndex = document.Interactions.IndexOf(currentInteraction) + 1;
                if (nextIndex < document.Interactions.Count) { return document.Interactions[nextIndex]; }
            }
            return null;
        }

        private static Endpoint GetNextEndpoint(Document document, Endpoint currentEndpoint) {
            if (currentEndpoint != null) {
                var nextIndex = document.Endpoints.IndexOf(currentEndpoint) + 1;
                if (nextIndex < document.Endpoints.Count) { return document.Endpoints[nextIndex]; }
            }
            return null;
        }

        #endregion

    }
}
