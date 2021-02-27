using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Clamito.Gui {
    partial class DocumentControl {

        private void mnxAdd_Opening(object sender, CancelEventArgs e) {
            mnxAddMessage.Enabled = (this.Document.Endpoints.Count >= 2);
            mnxAddCommand.Enabled = true;
        }

        private void mnxAddEndpoint_Click(object sender, EventArgs e) {
            mnxEndpointAdd_Click(null, null);
        }

        private void mnxAddContent_Click(object sender, EventArgs e) {
            mnxInteractionAddContent_Click(null, null);
        }

        private void mnxAddCommand_Click(object sender, EventArgs e) {
            mnxInteractionAddCommand_Click(null, null);
        }


        private void mnxEndpointAdd_Click(object sender, EventArgs e) {
            using (var frm = new EndpointForm(this.Document, null, GetNextEndpoint(this.Document, this.SelectedEndpoint))) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    this.SelectedEndpoint = frm.SelectedEndpoint;
                }
            }
        }

        private void mnxEndpointRemove_Click(object sender, EventArgs e) {
            var nextIndex = this.Document.Endpoints.IndexOf(this.SelectedEndpoint);
            this.Document.Endpoints.Remove(this.SelectedEndpoint);
            if (nextIndex >= this.Document.Endpoints.Count) { nextIndex--; }
            if (this.Document.Endpoints.Count > 0) {
                this.SelectedEndpoint = this.Document.Endpoints[nextIndex];
            } else {
                this.SelectedEndpoint = null;
            }
        }

        private void mnxEndpointProperties_Click(object sender, EventArgs e) {
            using (var frm = new EndpointForm(this.Document, this.SelectedEndpoint)) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    this.SelectedEndpoint = frm.SelectedEndpoint;
                }
            }
        }


        private void mnxInteractionAddContent_Click(object sender, EventArgs e) {
            using (var frm = new MessageForm(this.Document, null, GetNextInteraction(this.Document, this.SelectedInteraction))) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    this.SelectedInteraction = frm.SelectedInteraction;
                }
            }
        }

        private void mnxInteractionAddCommand_Click(object sender, EventArgs e) {
            using (var frm = new CommandForm(this.Document, null, GetNextInteraction(this.Document, this.SelectedInteraction))) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    this.SelectedInteraction = frm.SelectedInteraction;
                }
            }
        }

        private void mnxInteractionRemove_Click(object sender, EventArgs e) {
            var nextIndex = this.Document.Interactions.IndexOf(this.SelectedInteraction);
            this.Document.Interactions.Remove(this.SelectedInteraction);
            if (nextIndex >= this.Document.Interactions.Count) { nextIndex--; }
            if (this.Document.Interactions.Count > 0) {
                this.SelectedInteraction = this.Document.Interactions[nextIndex];
            } else {
                this.SelectedInteraction = null;
            }
        }

        private void mnxInteractionProperties_Click(object sender, EventArgs e) {
            var interaction = this.SelectedInteraction;
            if (interaction != null) {
                if (interaction.Kind == InteractionKind.Message) {
                    using (var frm = new MessageForm(this.Document, (Message)this.SelectedInteraction)) {
                        if (frm.ShowDialog(this) == DialogResult.OK) {
                            this.SelectedInteraction = frm.SelectedInteraction;
                        }
                    }
                } else if (interaction.Kind == InteractionKind.Command) {
                    using (var frm = new CommandForm(this.Document, (Command)this.SelectedInteraction)) {
                        if (frm.ShowDialog(this) == DialogResult.OK) {
                            this.SelectedInteraction = frm.SelectedInteraction;
                        }
                    }
                }
            }
        }


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
