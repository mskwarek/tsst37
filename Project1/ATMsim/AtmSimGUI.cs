﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtmSim
{
    public partial class AtmSimGUI : Form
    {
        private string selectedName = "";

        public AtmSimGUI()
        {
            InitializeComponent();
        }

        private void netNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.InitTestNetwork();
            this.elementListBox.Items.Clear();
            foreach (string element in Manager.GetElements())
                elementListBox.Items.Add(element);
        }

        private void InitTestNetwork()
        {
            // Tymczasowo jedziemy z danymi na sztywno, kiedys znajdzie sie tutaj parsowanie z xml.
            Manager.Config config = new Manager.Config();
            Manager.Routing routing = new Manager.Routing();
            Utils.Log log = new Utils.Log("Log 1:");
            log.LogMsg("Galia est omnis divisa in partes tres");
            log.LogMsg("Quorum unam incolunt Belgae aliam Aquitani");
            log.LogMsg("Tertiam qui ipsorut Celtae nostra Gali apellantur");
            /*
            string log =     + Environment.NewLine +
                            "" + Environment.NewLine +
                            ;
             */
            config.Add("ID", "X");
            config.Add("type", "leet");
            routing.Add("B", "1");
            routing.Add("A", "-");
            Manager.AddElement("e1", config, routing, log);
            config = new Manager.Config();
            routing = new Manager.Routing();
            config.Add("ID", "Y");
            config.Add("type", "woot");
            routing.Add("A", "2");
            routing.Add("B", "-");
            log = new Utils.Log(log);
            log.ChangeInit("Log 2:");
            Manager.AddElement("e2", config, routing, log);
            // Koniec kodu tymczasowego
        }

        private void elementListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedName = (string)this.elementListBox.Items[this.elementListBox.SelectedIndex];
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            /*ConfigGUI configGUI = new ConfigGUI(
                Manager.GetConfig(this.selectedName),
                Manager.GetRouting(this.selectedName));*/
            ConfigGUI configGUI = new ConfigGUI(this.selectedName);
            configGUI.Show();
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogGUI logGUI = new LogGUI(this.selectedName);
            logGUI.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Manager.GetLog(this.selectedName).LogMsg("Hello!");
        }

    }
}