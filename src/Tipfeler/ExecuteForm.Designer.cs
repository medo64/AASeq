namespace Tipfeler.Gui {
    partial class ExecuteForm {
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
            if (disposing) { this.Engine.Dispose(); }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mnu = new System.Windows.Forms.ToolStrip();
            this.mnuRun = new System.Windows.Forms.ToolStripButton();
            this.mnuStep = new System.Windows.Forms.ToolStripButton();
            this.mnuStop = new System.Windows.Forms.ToolStripButton();
            this.lsvStatus = new System.Windows.Forms.ListView();
            this.lsvStatus_colDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sta = new System.Windows.Forms.StatusStrip();
            this.staStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.staRunCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwInitializeEngine = new System.ComponentModel.BackgroundWorker();
            this.mnu.SuspendLayout();
            this.sta.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRun,
            this.mnuStep,
            this.mnuStop});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.mnu.Size = new System.Drawing.Size(542, 27);
            this.mnu.TabIndex = 0;
            // 
            // mnuRun
            // 
            this.mnuRun.Enabled = false;
            this.mnuRun.Image = global::Tipfeler.Gui.Properties.Resources.mnuRun_16;
            this.mnuRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRun.Name = "mnuRun";
            this.mnuRun.Size = new System.Drawing.Size(54, 24);
            this.mnuRun.Text = "Run";
            this.mnuRun.ToolTipText = "Run (F5)";
            this.mnuRun.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // mnuStep
            // 
            this.mnuStep.Enabled = false;
            this.mnuStep.Image = global::Tipfeler.Gui.Properties.Resources.mnuStep_16;
            this.mnuStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStep.Name = "mnuStep";
            this.mnuStep.Size = new System.Drawing.Size(59, 24);
            this.mnuStep.Text = "Step";
            this.mnuStep.ToolTipText = "Step (F8)";
            this.mnuStep.Click += new System.EventHandler(this.mnuStep_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Enabled = false;
            this.mnuStop.Image = global::Tipfeler.Gui.Properties.Resources.mnuStop_16;
            this.mnuStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Size = new System.Drawing.Size(60, 24);
            this.mnuStop.Text = "Stop";
            this.mnuStop.ToolTipText = "Stop (Shift+F5)";
            this.mnuStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // lsvStatus
            // 
            this.lsvStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvStatus_colDescription});
            this.lsvStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvStatus.FullRowSelect = true;
            this.lsvStatus.GridLines = true;
            this.lsvStatus.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvStatus.Location = new System.Drawing.Point(0, 27);
            this.lsvStatus.Name = "lsvStatus";
            this.lsvStatus.Size = new System.Drawing.Size(542, 301);
            this.lsvStatus.TabIndex = 1;
            this.lsvStatus.UseCompatibleStateImageBehavior = false;
            this.lsvStatus.View = System.Windows.Forms.View.Details;
            // 
            // lsvStatus_colDescription
            // 
            this.lsvStatus_colDescription.Text = "Description";
            // 
            // sta
            // 
            this.sta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staStatus,
            this.staRunCount});
            this.sta.Location = new System.Drawing.Point(0, 328);
            this.sta.Name = "sta";
            this.sta.ShowItemToolTips = true;
            this.sta.Size = new System.Drawing.Size(542, 25);
            this.sta.TabIndex = 2;
            // 
            // staStatus
            // 
            this.staStatus.Name = "staStatus";
            this.staStatus.Size = new System.Drawing.Size(510, 20);
            this.staStatus.Spring = true;
            this.staStatus.Text = "Initializing...";
            this.staStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.staStatus.ToolTipText = "Status";
            // 
            // staRunCount
            // 
            this.staRunCount.Name = "staRunCount";
            this.staRunCount.Size = new System.Drawing.Size(17, 20);
            this.staRunCount.Text = "0";
            this.staRunCount.ToolTipText = "Run count";
            // 
            // bwInitializeEngine
            // 
            this.bwInitializeEngine.WorkerSupportsCancellation = true;
            this.bwInitializeEngine.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwInitializeEngine_DoWork);
            this.bwInitializeEngine.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwInitializeEngine_RunWorkerCompleted);
            // 
            // ExecuteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 353);
            this.Controls.Add(this.lsvStatus);
            this.Controls.Add(this.sta);
            this.Controls.Add(this.mnu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 320);
            this.Name = "ExecuteForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Execute";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_FormClosed);
            this.Load += new System.EventHandler(this.Form_Load);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.sta.ResumeLayout(false);
            this.sta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mnu;
        private System.Windows.Forms.ToolStripButton mnuRun;
        private System.Windows.Forms.ToolStripButton mnuStep;
        private System.Windows.Forms.ToolStripButton mnuStop;
        private System.Windows.Forms.ListView lsvStatus;
        private System.Windows.Forms.ColumnHeader lsvStatus_colDescription;
        private System.Windows.Forms.StatusStrip sta;
        private System.Windows.Forms.ToolStripStatusLabel staStatus;
        private System.Windows.Forms.ToolStripStatusLabel staRunCount;
        private System.ComponentModel.BackgroundWorker bwInitializeEngine;
    }
}