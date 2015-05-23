using System;
using iControlPluginInterface;

namespace iControlPluginLogWriter {
    class iControlPlugin : IiControlPlugin {
        public string Name {
            get {
                return "LogWriter";
            }
        }

        public string Author {
            get {
                return "Matthias Rank";
            }
        }

        private iControlPluginInterface.IiControlPluginHost pluginHost;
        public iControlPluginInterface.IiControlPluginHost Host {
            set {
                pluginHost = value;
            }
            get {
                return pluginHost;
            }
        }

        public bool Init() {
            return true;
        }

        public void Handle(string[] commands, string ip) {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\commandlog.txt";
            string text = String.Format("[{0:s}] [{1,15}] >> {2}", System.DateTime.Now, ip, String.Join(" ", commands)) + Environment.NewLine;

            System.IO.File.AppendAllText(path, text);
        }
    }
}
