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
            Manager.GetLog(this.elementName).Subscribe(this);
            InitializeComponent();
            this.logBox.Text = Manager.GetLog(this.elementName).GetString();
            this.logBox.Select(0, 0);
        }

        public void LogUpdated()
        {
            this.logBox.Text = Manager.GetLog(this.elementName).GetString();
        }

        public void LogGUI_Closing(Object sender, EventArgs e)
        {
            Manager.GetLog(this.elementName).Unsubscribe(this);
        }

    }
}
