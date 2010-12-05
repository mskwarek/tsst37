using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ATMsim
{
    public partial class AtmSimGUI : Form
    {
        private AtmSimData data = new AtmSimData();
        private string selectedName = "";

        public AtmSimGUI()
        {
            InitializeComponent();
        }

        private void netNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.initiateTestNetwork();
            this.elementListBox.Items.Clear();
            foreach (string element in this.data.getElements())
                elementListBox.Items.Add(element);
        }

        private void initiateTestNetwork()
        {
            // Tymczasowo jedziemy z danymi na sztywno, kiedys znajdzie sie tutaj parsowanie z xml.
            Dictionary<string, string> config = new Dictionary<string, string>();
            Dictionary<string, string> routing = new Dictionary<string, string>();
            Utils.Log log = new Utils.Log("Log 1:");
            log.logMsg("Galia est omnis divisa in partes tres");
            log.logMsg("Quorum unam incolunt Belgae aliam Aquitani");
            log.logMsg("Tertiam qui ipsorut Celtae nostra Gali apellantur");
            /*
            string log =     + Environment.NewLine +
                            "" + Environment.NewLine +
                            ;
             */
            config.Add("ID", "X");
            config.Add("type", "none");
            routing.Add("B", "1");
            routing.Add("A", "-");
            this.data.addNetworkElement("e1", config, routing, log);
            config = new Dictionary<string, string>(config);
            routing = new Dictionary<string, string>();
            routing.Add("A", "2");
            routing.Add("B", "-");
            log = new Utils.Log(log);
            log.changeInit("Log 2:");
            this.data.addNetworkElement("e2", config, routing, log);
            // Koniec kodu tymczasowego
        }

        private void elementListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedName = (string)this.elementListBox.Items[this.elementListBox.SelectedIndex];
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            ConfigGUI configGUI = new ConfigGUI(
                this.data.getConfig(this.selectedName),
                this.data.getRouting(this.selectedName));
            configGUI.Show();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogGUI logGUI = new LogGUI(this.data.getLog(this.selectedName));
            logGUI.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.data.getLog(this.selectedName).logMsg("Hello!");
        }

    }
}
