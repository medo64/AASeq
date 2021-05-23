namespace Tipfeler.Gui {
    partial class PluginStatusForm {
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
            this.lsvPlugins = new System.Windows.Forms.ListView();
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lsvPlugins
            // 
            this.lsvPlugins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStatus});
            this.lsvPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvPlugins.FullRowSelect = true;
            this.lsvPlugins.GridLines = true;
            this.lsvPlugins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvPlugins.Location = new System.Drawing.Point(0, 0);
            this.lsvPlugins.Name = "lsvPlugins";
            this.lsvPlugins.ShowItemToolTips = true;
            this.lsvPlugins.Size = new System.Drawing.Size(462, 353);
            this.lsvPlugins.TabIndex = 1;
            this.lsvPlugins.UseCompatibleStateImageBehavior = false;
            this.lsvPlugins.View = System.Windows.Forms.View.Details;
            // 
            // colStatus
            // 
            this.colStatus.Text = "";
            // 
            // PluginStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 353);
            this.Controls.Add(this.lsvPlugins);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "PluginStatusForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loaded plugins";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView lsvPlugins;
        private System.Windows.Forms.ColumnHeader colStatus;
    }
}