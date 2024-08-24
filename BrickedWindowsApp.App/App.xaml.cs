using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using BrickedWindowsApp.Windows;
using Microsoft.Extensions.DependencyInjection;
using BrickedWindowsApp.WinConf;
using System.Diagnostics;
using System.Reflection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BrickedWindowsApp.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<BrickedNavigationWindow>();
            services.AddScoped<BrickedSimpleWindow>();
            services.AddScoped<BrickedWindowsConfig>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            m_config = serviceProvider.GetService<BrickedWindowsConfig>();
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            directory += "\\BrickedWindows";
            string configFilePath = directory+"\\BrickedWindows.conf";
            Loader loader = new Loader();
            //loader.CreateFile(configFilePath);
            
            
            m_config.ReadConfig(configFilePath);
            if (m_config.WindowDescription != null)
            {
                Type windowType = m_config.WindowDescription.WindowType;
                m_window = (Window)serviceProvider.GetService(windowType);

                m_window.Title = m_config.AppName;
                m_window.Activate();
            }
        }

        private Window m_window;
        private BrickedWindowsConfig m_config;
    }
}
