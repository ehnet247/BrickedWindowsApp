using BrickedWindowsApp.Windows;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BrickedWindowsApp.WinConf
{
    public class Loader
    {
        public string ConfigFileName { get; set; } = "BrickedWindows.conf";
        public string ConfigFilePath { get; set; }

        public Loader()
        {
            ConfigFileName = "BrickedWindows.conf";
            string folder = new FileInfo(Environment.ProcessPath).Directory.ToString();
            ConfigFilePath = $"{folder}\\{ConfigFileName}";
        }
        public Loader(string configFileName)
        {
            ConfigFileName = configFileName;
            string folder = new FileInfo(Environment.ProcessPath).Directory.ToString();
            ConfigFilePath = $"{folder}\\{ConfigFileName}";
        }

        public void CreateFile(string filePath)
        {
            ConfigFilePath = filePath;
            BrickedWindowsConfig brickedWindowsConfig = new BrickedWindowsConfig("AppName");
            brickedWindowsConfig.WindowDescription = new BrickedWindowDescription(typeof(BrickedNavigationWindow), "MainWindow");
            XmlSerializer serializer = new XmlSerializer(typeof(BrickedWindowsConfig));
            FileStream stream = File.OpenWrite(ConfigFilePath);
            serializer.Serialize(stream, brickedWindowsConfig);
            stream.Close();
        }
    }
}
