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
                return "marank <development@m-rank.de>";
            }
        }

        public string Version {
            get {
                return "0.0.1";
            }
        }

        private IiControlPluginHost _pluginHost;
        public IiControlPluginHost Host {
            set {
                _pluginHost = value;
            }
            get {
                return _pluginHost;
            }
        }

        private string _configpath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "iControlPluginLogWriter.config");
        private Dictionary<string, object> _settings;

        public bool Init() {
            if (System.IO.File.Exists(_configpath)) {
                _settings = Host.DeserializeJSON(_configpath);
                if (_settings.ContainsKey("enabled") && Convert.ToBoolean(_settings["enabled"]) == false) {
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
