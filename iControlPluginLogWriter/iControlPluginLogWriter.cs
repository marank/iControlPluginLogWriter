using System;
using System.Collections.Generic;
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
            string configFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "iControlPluginPowerManagement.config");
            if (System.IO.File.Exists(configFile)) {
                Dictionary<string, string> settings = pluginHost.DeserializeJSON(configFile);
                bool value;
                if (settings.ContainsKey("enabled") && Boolean.TryParse(settings["enabled"], out value) && value == false) {
                    pluginHost.Log("Plugin disabled", this);
                    return false;
                }
            }
            return true;
        }

        public void Handle(string[] commands, string ip) {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "commandlog.txt");
            string text = String.Format("[{0:s}] [{1,15}] >> {2}", System.DateTime.Now, ip, String.Join(" ", commands)) + Environment.NewLine;

            System.IO.File.AppendAllText(path, text);
        }
    }
}
