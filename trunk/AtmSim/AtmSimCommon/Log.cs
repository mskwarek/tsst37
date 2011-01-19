using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    public class Log
    {
        private List<string> log;
        public List<string> Entries
        {
            get { return log; }
            set { log = value; }
        }

        private DateTime logStarted;
        private TimeSpan upTime
        {
            get { return DateTime.Now - logStarted; }
        }

        public Log()
        {
            this.log = new List<string>();
            this.logStarted = DateTime.Now;
        }

        public Log(string initString)
        {
            this.log = new List<string>();
            this.logStarted = DateTime.Now;
            this.log.Add("[" + upTime.TotalSeconds + "] " + initString);
        }

        public Log(Log origin)
        {
            this.log = new List<string>(origin.log);
            this.logStarted = DateTime.Now;
        }

        public Log(Log origin, int startIndex)
        {
            this.log = new List<string>(origin.log.Count);
            this.logStarted = DateTime.Now;
            for (int i = startIndex; i < origin.log.Count; i++)
            {
                this.log.Add(origin.log[i]);
            }
        }

        public void LogMsg(string msg)
        {
            this.log.Add("[" + upTime.TotalSeconds + "] " + msg);
        }

        public void Append(Log log)
        {
            foreach (string entry in log.log)
                this.log.Add(entry);
        }

        public override string ToString()
        {
            string logString = "";
            foreach (string msg in log)
            {
                logString += msg + Environment.NewLine;
            }
            return logString;
        }

    }
}
