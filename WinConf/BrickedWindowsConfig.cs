using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using System.Xml.Serialization;
using BrickedWindowsApp.Windows;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Reflection;

namespace BrickedWindowsApp.WinConf
{
    [XmlRoot(ElementName = "BrickedWindowsConfig")]
    public class BrickedWindowsConfig
    {
        public string AppName { get; set; }
        public BrickedWindowDescription WindowDescription { get; set; }

        public BrickedWindowsConfig(string appName)
        {
            AppName = appName;
            WindowDescription = new BrickedWindowDescription();
        }

        public BrickedWindowsConfig()
        {
            AppName = "ApplicationName";
            WindowDescription = new BrickedWindowDescription();
        }

        public void ReadConfig(string configFilePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BrickedWindowsConfig));
            if (File.Exists(configFilePath))
            {
                FileStream stream = File.OpenRead(configFilePath);
                var deserialization = (BrickedWindowsConfig)serializer.Deserialize(stream);
                if (deserialization != null)
                {
                    BrickedWindowsConfig config = (BrickedWindowsConfig)deserialization;
                    AppName = config.AppName;
                    WindowDescription = config.WindowDescription;
                }
            }
        }
    }


    public class BrickedWindowDescription
    {
        public string WindowTitle { get; set; }
        [XmlIgnore]
        public Type WindowType { get; set; }
        public string StrWindowType
        {
            get => WindowType.GetType().Name;
            set
            {
                string strWindowType = value;
                WindowType = GetWindow(strWindowType);
            }
        }

        public BrickedWindowDescription(Type brickedWindowType, string title)
        {
            WindowTitle = title;
            WindowType = brickedWindowType;
        }

        public BrickedWindowDescription()
        {
            WindowTitle = "WindowTitle";
            WindowType = typeof(BrickedSimpleWindow);
        }

        public Type GetWindow(string windowTypeName)
        {
            var definedtypes = Assembly.GetExecutingAssembly().DefinedTypes;
            var windowTypes = Assembly.GetAssembly(typeof(BrickedNavigationWindow)).GetTypes();
            List<string> typeNames = new List<string>();
            foreach (var type in windowTypes)
            {
                typeNames.Add(type.Name);
            }
            if (typeNames.Any(n => n == windowTypeName))
            {
                if (windowTypes.Any(t => t.Name == windowTypeName))
                {
                    return windowTypes.First(t => t.Name == windowTypeName);
                }
            }
            return typeof(BrickedWindowsApp.Windows.BrickedSimpleWindow);
        }
    }
}
