using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtmSim
{
    // interfejs potrzebny do aktualizacji loga na biezaco
    public interface ILogListener
    {
        void LogUpdated();
        void LogUpdated(string msg);
    }

    public class Log
    {
        private List<string> log;
        public List<string> Entries
        {
            get { return log; }
            set { log = value; }
        }
        private List<ILogListener> listeners = new List<ILogListener>();
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
            foreach (ILogListener listener in this.listeners)
            {
                if (listener != null)
                {
                    listener.LogUpdated();
                    listener.LogUpdated(msg);
                }
            }
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

        public void Subscribe(ILogListener listener)
        {
            if (!listeners.Contains(listener))
                this.listeners.Add(listener);
        }

        public void Unsubscribe(ILogListener listener)
        {
            if (this.listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}
