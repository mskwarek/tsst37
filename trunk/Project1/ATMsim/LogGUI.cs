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
    public partial class LogGUI : Form, Utils.LogListener
    {
        private Utils.Log log;

        public LogGUI(Utils.Log log)
        {
            this.log = log;
            log.subscribe(this);
            InitializeComponent();
            this.logBox.Text = log.getLog();
            this.logBox.Select(0, 0);
        }

        public void logUpdated()
        {
            this.logBox.Text = log.getLog();
        }

        public void LogGUI_Closing(Object sender, EventArgs e)
        {
            this.log.unsubscribe(this);
        }

    }
}
