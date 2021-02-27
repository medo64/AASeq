using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class ExecuteForm : Form {
        public ExecuteForm(Document document) {
            InitializeComponent();
            mnu.Renderer = Helper.ToolstripRenderer;
            this.Font = SystemFonts.MessageBoxFont;

            this.Document = document;

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }

        private readonly Document Document;


        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Alt | Keys.Menu:
                case Keys.F10:
                    if (mnu.ContainsFocus) {
                        //doc.Select();
                    } else {
                        mnu.Select();
                        mnu.Items[0].Select();
                    }
                    return true;


                case Keys.F5:
                    if (mnuRun.Enabled) { mnuRun.PerformClick(); }
                    return true;

                case Keys.F8:
                    if (mnuStep.Enabled) { mnuStep.PerformClick(); }
                    return true;

                case Keys.Shift | Keys.F5:
                    if (mnuStop.Enabled) { mnuStop.PerformClick(); }
                    return true;


                case Keys.Escape:
                    this.Close();
                    return true;

                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private void Form_Load(object sender, EventArgs e) {
            this.Engine = new Engine(this.Document);
            this.Engine.Started += Engine_Started;
            this.Engine.StepStarted += Engine_StepStarted;
            this.Engine.StepCompleted += Engine_StepCompleted;
            this.Engine.Stopped += Engine_Stopped;
            bwInitializeEngine.RunWorkerAsync();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e) {
            this.Engine.Terminate();
        }


        private void Form_Resize(object sender, EventArgs e) {
            lsvStatus_colDescription.Width = lsvStatus.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        #region Menu

        private void mnuStart_Click(object sender, EventArgs e) {
            staStatus.Text = "Starting...";
            sta.Refresh();
            this.Engine.Start();
        }

        private void mnuStep_Click(object sender, EventArgs e) {
            staStatus.Text = "Stepping...";
            sta.Refresh();
            this.Engine.Step();
        }

        private void mnuStop_Click(object sender, EventArgs e) {
            staStatus.Text = "Stopping...";
            sta.Refresh();
            this.Engine.Stop();
        }

        #endregion


        #region Engine

        private Engine Engine;


        void Engine_Started(object sender, EventArgs e) {
            staStatus.Text = "Started.";
        }

        void Engine_StepStarted(object sender, EventArgs e) {
            staStatus.Text = "Running...";
        }

        void Engine_StepCompleted(object sender, StepCompletedEventArgs e) {
            staRunCount.Text = this.Engine.StepCount.ToString();
            if (e.IsSuccess) {
                staStatus.Text = "Done.";
            } else {
                staStatus.Text = "Error!";
            }
        }

        void Engine_Stopped(object sender, EventArgs e) {
            staStatus.Text = "Stopped.";
        }



        private void bwInitializeEngine_DoWork(object sender, DoWorkEventArgs e) {
            this.Engine.Initialize();
        }

        private void bwInitializeEngine_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                this.Close();
            } else {
                mnuRun.Enabled = true;
                mnuStep.Enabled = true;
                mnuStop.Enabled = true;
                staStatus.Text = "Initialized.";
            }
        }

        #endregion

    }
}
