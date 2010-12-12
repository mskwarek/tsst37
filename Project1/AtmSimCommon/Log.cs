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
        private List<ILogListener> listeners = new List<ILogListener>();

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

        public void LogMsg(string msg)
        {
            this.log.Add(msg);
            foreach (ILogListener listener in this.listeners)
            {
                if (listener != null)
                {
                    listener.LogUpdated();
                    listener.LogUpdated(msg);
                }
            }
        }

        public string GetString()
        {
            string logString = "";
            foreach (string msg in log)
            {
                logString += msg + Environment.NewLine;
            }
            return logString;
        }

        public void ChangeInit(string initString)
        {
            log[0] = initString;
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
