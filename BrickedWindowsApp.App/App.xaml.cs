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
using Microsoft.Identity.Client;

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
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, Tenant)
            .WithDefaultRedirectUri()
            .Build();
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
            services.AddSingleton<BrickedWindowsConfig>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            m_config = serviceProvider.GetService<BrickedWindowsConfig>();
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            directory += "\\BrickedWindows";
            string configFilePath = directory+"\\BrickedWindows.conf";
            Loader loader = new Loader();
            //loader.CreateFile(configFilePath);
            var newUserAuth = serviceProvider.GetService<UserAuth>();

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
        // Below are the clientId (Application Id) of your app registration and the tenant information.
        // You have to replace:
        // - the content of ClientID with the Application Id for your app registration
        // - the content of Tenant by the information about the accounts allowed to sign-in in your application:
        //   - For Work or School account in your org, use your tenant ID, or domain
        //   - for any Work or School accounts, use `organizations`
        //   - for any Work or School accounts, or Microsoft personal account, use `common`
        //   - for Microsoft Personal account, use consumers
        private static string ClientId = "Enter_the_Application_Id_here";

        private static string Tenant = "common";
        private static IPublicClientApplication _clientApp;
        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }
    }
}
