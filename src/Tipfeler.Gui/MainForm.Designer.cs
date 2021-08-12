namespace Tipfeler.Gui {
    partial class MainForm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnu = new System.Windows.Forms.ToolStrip();
            this.mnuNew = new System.Windows.Forms.ToolStripButton();
            this.mnuOpenDefault = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveDefault = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu0 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuApp = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuAppFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAppUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAppDonate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAppAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFlowLabel = new System.Windows.Forms.ToolStripLabel();
            this.mnuEndpointLabel = new System.Windows.Forms.ToolStripLabel();
            this.mnuEndpointAddDefault = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuEndpointAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEndpoint0 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEndpointProperties = new System.Windows.Forms.ToolStripButton();
            this.mnuEndpointRemove = new System.Windows.Forms.ToolStripButton();
            this.mnu2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuInteractionLabel = new System.Windows.Forms.ToolStripLabel();
            this.mnuInteractionAdd = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuInteractionAddMessage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInteractionAddCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInteractionProperties = new System.Windows.Forms.ToolStripButton();
            this.mnuInteractionRemove = new System.Windows.Forms.ToolStripButton();
            this.mnu4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExecute = new System.Windows.Forms.ToolStripButton();
            this.doc = new Tipfeler.Gui.DocumentControl();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAppPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNew,
            this.mnuOpenDefault,
            this.mnuSaveDefault,
            this.mnu0,
            this.mnuApp,
            this.mnuFlowLabel,
            this.mnuEndpointLabel,
            this.mnuEndpointAddDefault,
            this.mnuEndpointProperties,
            this.mnuEndpointRemove,
            this.mnu2,
            this.mnuInteractionLabel,
            this.mnuInteractionAdd,
            this.mnuInteractionProperties,
            this.mnuInteractionRemove,
            this.mnu4,
            this.mnuExecute});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Padding = new System.Windows.Forms.Padding(1, 1, 1, 0);
            this.mnu.Size = new System.Drawing.Size(982, 28);
            this.mnu.TabIndex = 0;
            // 
            // mnuNew
            // 
            this.mnuNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuNew.Image = global::Tipfeler.Gui.Properties.Resources.mnuNew_16;
            this.mnuNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuNew.Name = "mnuNew";
            this.mnuNew.Size = new System.Drawing.Size(24, 24);
            this.mnuNew.Text = "New";
            this.mnuNew.ToolTipText = "New (Ctrl+N)";
            this.mnuNew.Click += new System.EventHandler(this.mnuNew_Click);
            // 
            // mnuOpenDefault
            // 
            this.mnuOpenDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuOpenDefault.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpen});
            this.mnuOpenDefault.Image = global::Tipfeler.Gui.Properties.Resources.mnuOpen_16;
            this.mnuOpenDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOpenDefault.Name = "mnuOpenDefault";
            this.mnuOpenDefault.Size = new System.Drawing.Size(39, 24);
            this.mnuOpenDefault.Tag = "mnuOpen";
            this.mnuOpenDefault.Text = "Open";
            this.mnuOpenDefault.ToolTipText = "Open (Ctrl+O)";
            this.mnuOpenDefault.ButtonClick += new System.EventHandler(this.mnuOpen_Click);
            this.mnuOpenDefault.DropDownOpening += new System.EventHandler(this.mnuOpenDefault_DropDownOpening);
            // 
            // mnuOpen
            // 
            this.mnuOpen.Image = global::Tipfeler.Gui.Properties.Resources.mnuOpen_16;
            this.mnuOpen.Name = "mnuOpen";
            this.mnuOpen.ShortcutKeyDisplayString = "Ctrl+O";
            this.mnuOpen.Size = new System.Drawing.Size(173, 26);
            this.mnuOpen.Text = "Open";
            this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
            // 
            // mnuSaveDefault
            // 
            this.mnuSaveDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveDefault.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuSaveAs});
            this.mnuSaveDefault.Image = global::Tipfeler.Gui.Properties.Resources.mnuSave_16;
            this.mnuSaveDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveDefault.Name = "mnuSaveDefault";
            this.mnuSaveDefault.Size = new System.Drawing.Size(39, 24);
            this.mnuSaveDefault.Tag = "mnuSave";
            this.mnuSaveDefault.Text = "Save";
            this.mnuSaveDefault.ToolTipText = "Save (Ctrl+S)";
            this.mnuSaveDefault.ButtonClick += new System.EventHandler(this.mnuSaveDefault_ButtonClick);
            this.mnuSaveDefault.DropDownOpening += new System.EventHandler(this.mnuSaveDefault_DropDownOpening);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = global::Tipfeler.Gui.Properties.Resources.mnuSave_16;
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeyDisplayString = "Ctrl+S";
            this.mnuSave.Size = new System.Drawing.Size(165, 26);
            this.mnuSave.Text = "Save";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // mnuSaveAs
            // 
            this.mnuSaveAs.Name = "mnuSaveAs";
            this.mnuSaveAs.ShortcutKeyDisplayString = "";
            this.mnuSaveAs.Size = new System.Drawing.Size(165, 26);
            this.mnuSaveAs.Text = "Save as";
            this.mnuSaveAs.Click += new System.EventHandler(this.mnuSaveAs_Click);
            // 
            // mnu0
            // 
            this.mnu0.Name = "mnu0";
            this.mnu0.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuApp
            // 
            this.mnuApp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuApp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAppPlugins,
            this.toolStripMenuItem2,
            this.mnuAppFeedback,
            this.mnuAppUpdate,
            this.mnuAppDonate,
            this.toolStripMenuItem1,
            this.mnuAppAbout});
            this.mnuApp.Image = global::Tipfeler.Gui.Properties.Resources.mnuApp_16;
            this.mnuApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuApp.Name = "mnuApp";
            this.mnuApp.Size = new System.Drawing.Size(34, 24);
            this.mnuApp.Text = "Application";
            this.mnuApp.ToolTipText = "Application (F1)";
            // 
            // mnuAppFeedback
            // 
            this.mnuAppFeedback.Name = "mnuAppFeedback";
            this.mnuAppFeedback.Size = new System.Drawing.Size(197, 26);
            this.mnuAppFeedback.Text = "Send feedback";
            this.mnuAppFeedback.Click += new System.EventHandler(this.mnuAppFeedback_Click);
            // 
            // mnuAppUpdate
            // 
            this.mnuAppUpdate.Name = "mnuAppUpdate";
            this.mnuAppUpdate.Size = new System.Drawing.Size(197, 26);
            this.mnuAppUpdate.Text = "Check for update";
            this.mnuAppUpdate.Click += new System.EventHandler(this.mnuAppUpdate_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 6);
            // 
            // mnuAppAbout
            // 
            this.mnuAppAbout.Name = "mnuAppAbout";
            this.mnuAppAbout.Size = new System.Drawing.Size(197, 26);
            this.mnuAppAbout.Text = "About";
            this.mnuAppAbout.Click += new System.EventHandler(this.mnuAppAbout_Click);
            // 
            // mnuFlowLabel
            // 
            this.mnuFlowLabel.Name = "mnuFlowLabel";
            this.mnuFlowLabel.Size = new System.Drawing.Size(0, 24);
            // 
            // mnuEndpointLabel
            // 
            this.mnuEndpointLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.mnuEndpointLabel.Name = "mnuEndpointLabel";
            this.mnuEndpointLabel.Size = new System.Drawing.Size(69, 24);
            this.mnuEndpointLabel.Text = "Endpoint";
            // 
            // mnuEndpointAddDefault
            // 
            this.mnuEndpointAddDefault.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuEndpointAddDefault.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEndpointAdd,
            this.mnuEndpoint0});
            this.mnuEndpointAddDefault.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemAdd_16;
            this.mnuEndpointAddDefault.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEndpointAddDefault.Name = "mnuEndpointAddDefault";
            this.mnuEndpointAddDefault.Size = new System.Drawing.Size(39, 24);
            this.mnuEndpointAddDefault.Tag = "mnuItemAdd";
            this.mnuEndpointAddDefault.Text = "Add";
            this.mnuEndpointAddDefault.ToolTipText = "Add endpoint";
            this.mnuEndpointAddDefault.ButtonClick += new System.EventHandler(this.mnuEndpointAdd_Click);
            this.mnuEndpointAddDefault.DropDownOpening += new System.EventHandler(this.mnuEndpoint_DropDownOpening);
            // 
            // mnuEndpointAdd
            // 
            this.mnuEndpointAdd.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemAdd_16;
            this.mnuEndpointAdd.Name = "mnuEndpointAdd";
            this.mnuEndpointAdd.Size = new System.Drawing.Size(176, 26);
            this.mnuEndpointAdd.Tag = "mnuItemAdd";
            this.mnuEndpointAdd.Text = "Add endpoint";
            this.mnuEndpointAdd.Click += new System.EventHandler(this.mnuEndpointAdd_Click);
            // 
            // mnuEndpoint0
            // 
            this.mnuEndpoint0.Name = "mnuEndpoint0";
            this.mnuEndpoint0.Size = new System.Drawing.Size(173, 6);
            // 
            // mnuEndpointProperties
            // 
            this.mnuEndpointProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuEndpointProperties.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemEdit_16;
            this.mnuEndpointProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEndpointProperties.Name = "mnuEndpointProperties";
            this.mnuEndpointProperties.Size = new System.Drawing.Size(24, 24);
            this.mnuEndpointProperties.Tag = "mnuItemEdit";
            this.mnuEndpointProperties.Text = "Modify";
            this.mnuEndpointProperties.ToolTipText = "Modify endpoint";
            this.mnuEndpointProperties.Click += new System.EventHandler(this.mnuEndpointProperties_Click);
            // 
            // mnuEndpointRemove
            // 
            this.mnuEndpointRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuEndpointRemove.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemRemove_16;
            this.mnuEndpointRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEndpointRemove.Name = "mnuEndpointRemove";
            this.mnuEndpointRemove.Size = new System.Drawing.Size(24, 24);
            this.mnuEndpointRemove.Tag = "mnuItemRemove";
            this.mnuEndpointRemove.Text = "Remove";
            this.mnuEndpointRemove.ToolTipText = "Remove endpoint";
            this.mnuEndpointRemove.Click += new System.EventHandler(this.mnuEndpointRemove_Click);
            // 
            // mnu2
            // 
            this.mnu2.Name = "mnu2";
            this.mnu2.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuInteractionLabel
            // 
            this.mnuInteractionLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.mnuInteractionLabel.Name = "mnuInteractionLabel";
            this.mnuInteractionLabel.Size = new System.Drawing.Size(80, 24);
            this.mnuInteractionLabel.Text = "Interaction";
            // 
            // mnuInteractionAdd
            // 
            this.mnuInteractionAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuInteractionAdd.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInteractionAddMessage,
            this.mnuInteractionAddCommand});
            this.mnuInteractionAdd.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemAdd_16;
            this.mnuInteractionAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuInteractionAdd.Name = "mnuInteractionAdd";
            this.mnuInteractionAdd.Size = new System.Drawing.Size(39, 24);
            this.mnuInteractionAdd.Tag = "mnuItemAdd";
            this.mnuInteractionAdd.Text = "Add";
            this.mnuInteractionAdd.ToolTipText = "Add interaction";
            this.mnuInteractionAdd.ButtonClick += new System.EventHandler(this.mnuInteractionAdd_ButtonClick);
            // 
            // mnuInteractionAddMessage
            // 
            this.mnuInteractionAddMessage.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemAdd_16;
            this.mnuInteractionAddMessage.Name = "mnuInteractionAddMessage";
            this.mnuInteractionAddMessage.Size = new System.Drawing.Size(183, 26);
            this.mnuInteractionAddMessage.Tag = "mnuItemAdd";
            this.mnuInteractionAddMessage.Text = "Add message";
            this.mnuInteractionAddMessage.Click += new System.EventHandler(this.mnuInteractionAddContent_Click);
            // 
            // mnuInteractionAddCommand
            // 
            this.mnuInteractionAddCommand.Name = "mnuInteractionAddCommand";
            this.mnuInteractionAddCommand.Size = new System.Drawing.Size(183, 26);
            this.mnuInteractionAddCommand.Text = "Add command";
            this.mnuInteractionAddCommand.Click += new System.EventHandler(this.mnuInteractionAddCommand_Click);
            // 
            // mnuInteractionProperties
            // 
            this.mnuInteractionProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuInteractionProperties.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemEdit_16;
            this.mnuInteractionProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuInteractionProperties.Name = "mnuInteractionProperties";
            this.mnuInteractionProperties.Size = new System.Drawing.Size(24, 24);
            this.mnuInteractionProperties.Tag = "mnuItemEdit";
            this.mnuInteractionProperties.Text = "Modify";
            this.mnuInteractionProperties.ToolTipText = "Modify interaction";
            this.mnuInteractionProperties.Click += new System.EventHandler(this.mnuInteractionProperties_Click);
            // 
            // mnuInteractionRemove
            // 
            this.mnuInteractionRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuInteractionRemove.Image = global::Tipfeler.Gui.Properties.Resources.mnuItemRemove_16;
            this.mnuInteractionRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuInteractionRemove.Name = "mnuInteractionRemove";
            this.mnuInteractionRemove.Size = new System.Drawing.Size(24, 24);
            this.mnuInteractionRemove.Tag = "mnuItemRemove";
            this.mnuInteractionRemove.Text = "Remove";
            this.mnuInteractionRemove.ToolTipText = "Remove interaction";
            this.mnuInteractionRemove.Click += new System.EventHandler(this.mnuInteractionRemove_Click);
            // 
            // mnu4
            // 
            this.mnu4.Name = "mnu4";
            this.mnu4.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuExecute
            // 
            this.mnuExecute.Image = global::Tipfeler.Gui.Properties.Resources.mnuExecute_16;
            this.mnuExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExecute.Name = "mnuExecute";
            this.mnuExecute.Size = new System.Drawing.Size(84, 24);
            this.mnuExecute.Text = "Execute";
            this.mnuExecute.Click += new System.EventHandler(this.mnuExecute_Click);
            // 
            // doc
            // 
            this.doc.AutoScroll = true;
            this.doc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doc.Document = null;
            this.doc.LastSelection = null;
            this.doc.Location = new System.Drawing.Point(0, 28);
            this.doc.Name = "doc";
            this.doc.SelectedEndpoint = null;
            this.doc.SelectedInteraction = null;
            this.doc.Size = new System.Drawing.Size(982, 525);
            this.doc.TabIndex = 1;
            this.doc.Changed += new System.EventHandler(this.doc_Changed);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(194, 6);
            // 
            // mnuAppPlugins
            // 
            this.mnuAppPlugins.Name = "mnuAppPlugins";
            this.mnuAppPlugins.Size = new System.Drawing.Size(197, 26);
            this.mnuAppPlugins.Text = "Plugins";
            this.mnuAppPlugins.Click += new System.EventHandler(this.mnuAppPlugins_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.doc);
            this.Controls.Add(this.mnu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(599, 398);
            this.Name = "MainForm";
            this.Text = "Tipfeler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnu;
        private System.Windows.Forms.ToolStripButton mnuNew;
        private System.Windows.Forms.ToolStripSplitButton mnuOpenDefault;
        private System.Windows.Forms.ToolStripMenuItem mnuOpen;
        private System.Windows.Forms.ToolStripSplitButton mnuSaveDefault;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
        private System.Windows.Forms.ToolStripSeparator mnu0;
        private System.Windows.Forms.ToolStripDropDownButton mnuApp;
        private System.Windows.Forms.ToolStripMenuItem mnuAppFeedback;
        private System.Windows.Forms.ToolStripMenuItem mnuAppUpdate;
        private System.Windows.Forms.ToolStripMenuItem mnuAppDonate;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuAppAbout;
        private DocumentControl doc;
        private System.Windows.Forms.ToolStripLabel mnuFlowLabel;
        private System.Windows.Forms.ToolStripLabel mnuEndpointLabel;
        private System.Windows.Forms.ToolStripButton mnuEndpointRemove;
        private System.Windows.Forms.ToolStripButton mnuEndpointProperties;
        private System.Windows.Forms.ToolStripSeparator mnu2;
        private System.Windows.Forms.ToolStripLabel mnuInteractionLabel;
        private System.Windows.Forms.ToolStripButton mnuInteractionRemove;
        private System.Windows.Forms.ToolStripButton mnuInteractionProperties;
        private System.Windows.Forms.ToolStripSplitButton mnuInteractionAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuInteractionAddMessage;
        private System.Windows.Forms.ToolStripMenuItem mnuInteractionAddCommand;
        private System.Windows.Forms.ToolStripSplitButton mnuEndpointAddDefault;
        private System.Windows.Forms.ToolStripMenuItem mnuEndpointAdd;
        private System.Windows.Forms.ToolStripSeparator mnuEndpoint0;
        private System.Windows.Forms.ToolStripSeparator mnu4;
        private System.Windows.Forms.ToolStripButton mnuExecute;
        private System.Windows.Forms.ToolStripMenuItem mnuAppPlugins;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    }
}
