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
    public partial class LogGUI : Form, Utils.ILogListener
    {
        //private Utils.Log log;
        private string elementName;

        public LogGUI(string name)
        {
            this.elementName = name;
            //this.log = Manager.GetLog(this.elementName);
            Manager.SubscribeLog(this.elementName, this);
            InitializeComponent();
            this.logBox.Text = Manager.GetLog(this.elementName);
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
            Manager.UnsubscribeLog(this.elementName, this);
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

    }
}
