using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace IniFiles
{
    public class ConfigurationManager
    {
        private Dictionary<string,Dictionary<string, string>> dico;
        private DateTime lastUpdate = DateTime.MinValue;
        private string iniPath;

        private static string slash1 = Path.DirectorySeparatorChar.ToString();
        private static string slash2 = slash1 + slash1;

        public ConfigurationManager(string iniPath)
        {
            this.iniPath = iniPath;
            refresh(true);
        }

        public string getValue(string section, string key)
        {
            refresh();
            if (dico.ContainsKey(section) && dico[section].ContainsKey(key))
                return dico[section][key].Replace(slash2,slash1);
            return string.Empty;
        }

        public void setValue(string section, string key,string value)
        {
            IniWriter.WriteKey(iniPath, section, key, value.Replace(slash1,slash2));
            lastUpdate = DateTime.MinValue;
        }

        private void refresh()
        {
            refresh(false);
        }

        private void refresh(bool force)
        {
            FileInfo fi = new FileInfo(iniPath);
            if (force || fi.LastWriteTime > lastUpdate)
            {
                Dictionary<string, Dictionary<string, string>> tmpDico = new Dictionary<string, Dictionary<string, string>>();
                foreach(string category in IniReader.GetCategories(iniPath))
                {
                    tmpDico.Add(category, new Dictionary<string, string>());
                    foreach(string key in IniReader.GetKeys(iniPath, category))
                    {
                        tmpDico[category].Add(key, IniReader.GetValue(iniPath, category, key, ""));
                    }
                }
                dico = tmpDico;
                lastUpdate = DateTime.Now;
            }
        }
    }
}
