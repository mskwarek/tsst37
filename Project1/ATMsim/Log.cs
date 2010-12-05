using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATMsim.Utils
{
    public interface LogListener
    {
        void logUpdated();
    }

    public class Log
    {
        private List<string> log;
        private List<LogListener> listeners = new List<LogListener>();

        public Log()
        {
            this.log = new List<string>();
            this.log.Add("Nowy log!");
        }

        public Log(string initString)
        {
            this.log = new List<string>();
            this.log.Add(initString);
        }

        public Log(Log origin)
        {
            this.log = new List<string>(origin.log);
        }

        public void logMsg(string msg)
        {
            this.log.Add(msg);
            foreach (LogListener listener in this.listeners)
            {
                if (listener != null)
                    listener.logUpdated();
            }
        }

        public string getLog()
        {
            string logString = "";
            foreach (string msg in log)
            {
                logString += msg + Environment.NewLine;
            }
            return logString;
        }

        public void changeInit(string initString)
        {
            log[0] = initString;
        }

        public void subscribe(LogListener listener)
        {
            if (!listeners.Contains(listener))
                this.listeners.Add(listener);
        }

        public void unsubscribe(LogListener listener)
        {
            if (this.listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}
