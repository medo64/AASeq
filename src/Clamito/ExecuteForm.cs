using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class ExecuteForm : Form {
        public ExecuteForm(Document document) {
            InitializeComponent();
            mnu.Renderer = Helper.ToolstripRenderer;
            Font = SystemFonts.MessageBoxFont;

            Document = document;

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
                    Close();
                    return true;

                default: return base.ProcessCmdKey(ref msg, keyData);
            }
        }


        private void Form_Load(object sender, EventArgs e) {
            Engine = new Engine(Document);
            Engine.Started += Engine_Started;
            Engine.StepStarted += Engine_StepStarted;
            Engine.StepCompleted += Engine_StepCompleted;
            Engine.Stopped += Engine_Stopped;
            bwInitializeEngine.RunWorkerAsync();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e) {
            Engine.Terminate();
        }


        private void Form_Resize(object sender, EventArgs e) {
            lsvStatus_colDescription.Width = lsvStatus.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

        #region Menu

        private void mnuStart_Click(object sender, EventArgs e) {
            staStatus.Text = "Starting...";
            sta.Refresh();
            Engine.Start();
        }

        private void mnuStep_Click(object sender, EventArgs e) {
            staStatus.Text = "Stepping...";
            sta.Refresh();
            Engine.Step();
        }

        private void mnuStop_Click(object sender, EventArgs e) {
            staStatus.Text = "Stopping...";
            sta.Refresh();
            Engine.Stop();
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
            staRunCount.Text = Engine.StepCount.ToString();
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
            Engine.Initialize();
        }

        private void bwInitializeEngine_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) {
                Close();
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
