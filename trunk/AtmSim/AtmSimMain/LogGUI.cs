using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AtmSim
{
    public partial class LogGUI : Form, ILogListener
    {
        //private Utils.Log log;
        private int id;
        //private string elementName;
        private Manager manager;

        public LogGUI(Manager manager, int id)
        {
            this.id = id;
            //this.elementName = name;
            this.manager = manager;
            //this.log = Manager.GetLog(this.elementName);
            //manager.SubscribeLog(this.id, this);
            InitializeComponent();
            this.logBox.Text = manager.GetLog(this.id).ToString();
            this.logBox.Select(0, 0);
        }

        public void LogUpdated()
        {
            //this.logBox.Text = Manager.GetLog(this.elementName).GetString();
        }

        public void LogUpdated(string msg)
        {
            this.logBox.Text = this.logBox.Text + msg + Environment.NewLine;
        }

        public void LogGUI_Closing(Object sender, EventArgs e)
        {
            //manager.UnsubscribeLog(this.elementName, this);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveLogDialog.ShowDialog();
        }

        private void saveLogDialog_FileOk(object sender, CancelEventArgs e)
        {
            if (saveLogDialog.FileName != "")
            {
                System.IO.FileStream fs = (System.IO.FileStream)saveLogDialog.OpenFile();
                byte[] logbyte = new UTF8Encoding(true).GetBytes(logBox.Text);
                fs.Write(logbyte, 0, logbyte.Length);
                fs.Close();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.logBox.Text = manager.GetLog(this.id).ToString();
            this.logBox.Select(0, 0);
        }

    }
}
