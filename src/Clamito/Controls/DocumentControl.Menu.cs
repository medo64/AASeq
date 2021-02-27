using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Clamito.Gui {
    partial class DocumentControl {

        private void mnxAdd_Opening(object sender, CancelEventArgs e) {
            mnxAddMessage.Enabled = (Document.Endpoints.Count >= 2);
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
            using (var frm = new EndpointForm(Document, null, GetNextEndpoint(Document, SelectedEndpoint))) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    SelectedEndpoint = frm.SelectedEndpoint;
                }
            }
        }

        private void mnxEndpointRemove_Click(object sender, EventArgs e) {
            var nextIndex = Document.Endpoints.IndexOf(SelectedEndpoint);
            Document.Endpoints.Remove(SelectedEndpoint);
            if (nextIndex >= Document.Endpoints.Count) { nextIndex--; }
            if (Document.Endpoints.Count > 0) {
                SelectedEndpoint = Document.Endpoints[nextIndex];
            } else {
                SelectedEndpoint = null;
            }
        }

        private void mnxEndpointProperties_Click(object sender, EventArgs e) {
            using (var frm = new EndpointForm(Document, SelectedEndpoint)) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    SelectedEndpoint = frm.SelectedEndpoint;
                }
            }
        }


        private void mnxInteractionAddContent_Click(object sender, EventArgs e) {
            using (var frm = new MessageForm(Document, null, GetNextInteraction(Document, SelectedInteraction))) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    SelectedInteraction = frm.SelectedInteraction;
                }
            }
        }

        private void mnxInteractionAddCommand_Click(object sender, EventArgs e) {
            using (var frm = new CommandForm(Document, null, GetNextInteraction(Document, SelectedInteraction))) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    SelectedInteraction = frm.SelectedInteraction;
                }
            }
        }

        private void mnxInteractionRemove_Click(object sender, EventArgs e) {
            var nextIndex = Document.Interactions.IndexOf(SelectedInteraction);
            Document.Interactions.Remove(SelectedInteraction);
            if (nextIndex >= Document.Interactions.Count) { nextIndex--; }
            if (Document.Interactions.Count > 0) {
                SelectedInteraction = Document.Interactions[nextIndex];
            } else {
                SelectedInteraction = null;
            }
        }

        private void mnxInteractionProperties_Click(object sender, EventArgs e) {
            var interaction = SelectedInteraction;
            if (interaction != null) {
                if (interaction.Kind == InteractionKind.Message) {
                    using (var frm = new MessageForm(Document, (Message)SelectedInteraction)) {
                        if (frm.ShowDialog(this) == DialogResult.OK) {
                            SelectedInteraction = frm.SelectedInteraction;
                        }
                    }
                } else if (interaction.Kind == InteractionKind.Command) {
                    using (var frm = new CommandForm(Document, (Command)SelectedInteraction)) {
                        if (frm.ShowDialog(this) == DialogResult.OK) {
                            SelectedInteraction = frm.SelectedInteraction;
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
