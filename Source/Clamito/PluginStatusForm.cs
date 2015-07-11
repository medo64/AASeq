using System;
using System.Windows.Forms;

namespace Clamito.Gui {
    internal partial class PluginStatusForm : Form {
        public PluginStatusForm() {
            InitializeComponent();

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private void Form_Load(object sender, EventArgs e) {
            var protocolGroup = new ListViewGroup("Protocols");
            lsvPlugins.Groups.Add(protocolGroup);
            foreach (var plugin in Plugin.Protocols) {
                var lvi = new ListViewItem(plugin.DisplayName, protocolGroup);
                lvi.ToolTipText = plugin.Name + ": " + plugin.Description;
                lsvPlugins.Items.Add(lvi);
            }

            var commandGroup = new ListViewGroup("Commands");
            lsvPlugins.Groups.Add(commandGroup);
            foreach (var plugin in Plugin.Commands) {
                var lvi = new ListViewItem(plugin.DisplayName, commandGroup);
                lvi.ToolTipText = plugin.Name + ": " + plugin.Description;
                lsvPlugins.Items.Add(lvi);
            }

            Form_Resize(null, null);
        }

        private void Form_Resize(object sender, EventArgs e) {
            lsvPlugins.Columns[0].Width = lsvPlugins.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }

    }
}
