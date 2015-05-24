using System;
using System.Collections.Generic;
using iControlInterfaces;

namespace iControlPluginLogWriter {
    public class iControlPlugin : IiControlPlugin {
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

        private IiControlPluginHost pluginHost;
        public IiControlPluginHost Host {
            set {
                pluginHost = value;
            }
            get {
                return pluginHost;
            }
        }

        public bool Init() {
            string configFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "iControlPluginLogWriter.config");
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

        public void Handle(string[] commands, IiControlClient client) {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "commandlog.txt");
            string text = String.Format("[{0:s}] [{1,15}] >> {2}", System.DateTime.Now, client.IPAddress, String.Join(" ", commands)) + Environment.NewLine;

            System.IO.File.AppendAllText(path, text);
        }
    }
}
