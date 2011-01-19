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
    public partial class LogGUI : Form
    {
        private Log log;
        private int id;
        private Manager manager;

        public LogGUI(Manager manager, int id)
        {
            this.id = id;
            this.manager = manager;
            InitializeComponent();
            this.Text = "Log " + manager.Get(id, "Name");
            this.log = manager.GetLog(this.id, 0);
            this.logBox.Text = this.log.ToString();
            this.logBox.Select(0, 0);
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
            this.log.Append(manager.GetLog(this.id, this.log.Entries.Count));
            this.logBox.Text = this.log.ToString();
            this.logBox.Select(0, 0);
        }

    }
}
