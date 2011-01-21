using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace AtmSim
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length != 3)
                return;
            if (!File.Exists(args[0]))
                return;
            Config.Node cNode = Config.Node.fopen(args[0]);
            File.Delete(args[0]);
            int mPort = Int32.Parse(args[1]);
            int cPort = Int32.Parse(args[2]);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (cNode.Type == "Switch")
                Application.Run(new NodeForm(cNode, mPort));
            else if (cNode.Type == "Source")
                Application.Run(new SourceForm(cNode, mPort, cPort));
            else if (cNode.Type == "Sink")
                Application.Run(new SinkForm(cNode, mPort, cPort));
        }
    }
}
