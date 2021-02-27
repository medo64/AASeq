namespace Clamito.Gui {
    partial class DocumentControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.mnxAdd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnxAddEndpoint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxAddMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxAddCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxEndpoint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnxEndpointAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxEndpointRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxEndpoint0 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxEndpointProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxInteraction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnxInteractionAddMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxInteractionAddCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxInteractionRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxInteraction0 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxInteractionProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxAdd.SuspendLayout();
            this.mnxEndpoint.SuspendLayout();
            this.mnxInteraction.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnxAdd
            // 
            this.mnxAdd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnxAddEndpoint,
            this.mnxAddMessage,
            this.mnxAddCommand});
            this.mnxAdd.Name = "mnxAdd";
            this.mnxAdd.Size = new System.Drawing.Size(178, 76);
            this.mnxAdd.Opening += new System.ComponentModel.CancelEventHandler(this.mnxAdd_Opening);
            // 
            // mnxAddEndpoint
            // 
            this.mnxAddEndpoint.Name = "mnxAddEndpoint";
            this.mnxAddEndpoint.Size = new System.Drawing.Size(177, 24);
            this.mnxAddEndpoint.Text = "Add endpoint";
            this.mnxAddEndpoint.Click += new System.EventHandler(this.mnxAddEndpoint_Click);
            // 
            // mnxAddMessage
            // 
            this.mnxAddMessage.Name = "mnxAddMessage";
            this.mnxAddMessage.Size = new System.Drawing.Size(177, 24);
            this.mnxAddMessage.Text = "Add message";
            this.mnxAddMessage.Click += new System.EventHandler(this.mnxAddContent_Click);
            // 
            // mnxAddCommand
            // 
            this.mnxAddCommand.Name = "mnxAddCommand";
            this.mnxAddCommand.Size = new System.Drawing.Size(177, 24);
            this.mnxAddCommand.Text = "Add command";
            this.mnxAddCommand.Click += new System.EventHandler(this.mnxAddCommand_Click);
            // 
            // mnxEndpoint
            // 
            this.mnxEndpoint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnxEndpointAdd,
            this.mnxEndpointProperties,
            this.mnxEndpoint0,
            this.mnxEndpointRemove});
            this.mnxEndpoint.Name = "mnxEndpoint";
            this.mnxEndpoint.Size = new System.Drawing.Size(197, 82);
            // 
            // mnxEndpointAdd
            // 
            this.mnxEndpointAdd.Image = global::Clamito.Gui.Properties.Resources.mnuItemAdd_16;
            this.mnxEndpointAdd.Name = "mnxEndpointAdd";
            this.mnxEndpointAdd.Size = new System.Drawing.Size(196, 24);
            this.mnxEndpointAdd.Tag = "mnuItemAdd";
            this.mnxEndpointAdd.Text = "Add endpoint";
            this.mnxEndpointAdd.Click += new System.EventHandler(this.mnxEndpointAdd_Click);
            // 
            // mnxEndpointRemove
            // 
            this.mnxEndpointRemove.Image = global::Clamito.Gui.Properties.Resources.mnuItemRemove_16;
            this.mnxEndpointRemove.Name = "mnxEndpointRemove";
            this.mnxEndpointRemove.Size = new System.Drawing.Size(196, 24);
            this.mnxEndpointRemove.Tag = "mnuItemRemove";
            this.mnxEndpointRemove.Text = "Remove endpoint";
            this.mnxEndpointRemove.Click += new System.EventHandler(this.mnxEndpointRemove_Click);
            // 
            // mnxEndpoint0
            // 
            this.mnxEndpoint0.Name = "mnxEndpoint0";
            this.mnxEndpoint0.Size = new System.Drawing.Size(193, 6);
            // 
            // mnxEndpointProperties
            // 
            this.mnxEndpointProperties.Image = global::Clamito.Gui.Properties.Resources.mnuItemEdit_16;
            this.mnxEndpointProperties.Name = "mnxEndpointProperties";
            this.mnxEndpointProperties.Size = new System.Drawing.Size(196, 24);
            this.mnxEndpointProperties.Tag = "mnuItemEdit";
            this.mnxEndpointProperties.Text = "Modify endpoint";
            this.mnxEndpointProperties.Click += new System.EventHandler(this.mnxEndpointProperties_Click);
            // 
            // mnxInteraction
            // 
            this.mnxInteraction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnxInteractionAddMessage,
            this.mnxInteractionAddCommand,
            this.mnxInteractionProperties,
            this.mnxInteraction0,
            this.mnxInteractionRemove});
            this.mnxInteraction.Name = "mnxEndpoint";
            this.mnxInteraction.Size = new System.Drawing.Size(201, 82);
            // 
            // mnxInteractionAddMessage
            // 
            this.mnxInteractionAddMessage.Image = global::Clamito.Gui.Properties.Resources.mnuItemAdd_16;
            this.mnxInteractionAddMessage.Name = "mnxInteractionAddMessage";
            this.mnxInteractionAddMessage.Size = new System.Drawing.Size(200, 24);
            this.mnxInteractionAddMessage.Tag = "mnuItemAdd";
            this.mnxInteractionAddMessage.Text = "Add message";
            this.mnxInteractionAddMessage.Click += new System.EventHandler(this.mnxInteractionAddContent_Click);
            // 
            // mnxInteractionAddCommand
            // 
            this.mnxInteractionAddCommand.Name = "mnxInteractionAddCommand";
            this.mnxInteractionAddCommand.Size = new System.Drawing.Size(200, 24);
            this.mnxInteractionAddCommand.Text = "Add command";
            this.mnxInteractionAddCommand.Click += new System.EventHandler(this.mnxInteractionAddCommand_Click);
            // 
            // mnxInteractionRemove
            // 
            this.mnxInteractionRemove.Image = global::Clamito.Gui.Properties.Resources.mnuItemRemove_16;
            this.mnxInteractionRemove.Name = "mnxInteractionRemove";
            this.mnxInteractionRemove.Size = new System.Drawing.Size(207, 24);
            this.mnxInteractionRemove.Tag = "mnuItemRemove";
            this.mnxInteractionRemove.Text = "Remove interaction";
            this.mnxInteractionRemove.Click += new System.EventHandler(this.mnxInteractionRemove_Click);
            // 
            // mnxInteraction0
            // 
            this.mnxInteraction0.Name = "mnxInteraction0";
            this.mnxInteraction0.Size = new System.Drawing.Size(197, 6);
            // 
            // mnxInteractionProperties
            // 
            this.mnxInteractionProperties.Image = global::Clamito.Gui.Properties.Resources.mnuItemEdit_16;
            this.mnxInteractionProperties.Name = "mnxInteractionProperties";
            this.mnxInteractionProperties.Size = new System.Drawing.Size(200, 24);
            this.mnxInteractionProperties.Tag = "mnuItemEdit";
            this.mnxInteractionProperties.Text = "Modify interaction";
            this.mnxInteractionProperties.Click += new System.EventHandler(this.mnxInteractionProperties_Click);
            this.mnxAdd.ResumeLayout(false);
            this.mnxEndpoint.ResumeLayout(false);
            this.mnxInteraction.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip mnxAdd;
        private System.Windows.Forms.ContextMenuStrip mnxEndpoint;
        private System.Windows.Forms.ToolStripMenuItem mnxAddEndpoint;
        private System.Windows.Forms.ToolStripMenuItem mnxAddMessage;
        private System.Windows.Forms.ToolStripMenuItem mnxEndpointAdd;
        private System.Windows.Forms.ToolStripMenuItem mnxEndpointRemove;
        private System.Windows.Forms.ToolStripSeparator mnxEndpoint0;
        private System.Windows.Forms.ToolStripMenuItem mnxEndpointProperties;
        private System.Windows.Forms.ContextMenuStrip mnxInteraction;
        private System.Windows.Forms.ToolStripMenuItem mnxInteractionAddMessage;
        private System.Windows.Forms.ToolStripMenuItem mnxInteractionRemove;
        private System.Windows.Forms.ToolStripSeparator mnxInteraction0;
        private System.Windows.Forms.ToolStripMenuItem mnxInteractionProperties;
        private System.Windows.Forms.ToolStripMenuItem mnxInteractionAddCommand;
        private System.Windows.Forms.ToolStripMenuItem mnxAddCommand;
    }
}
