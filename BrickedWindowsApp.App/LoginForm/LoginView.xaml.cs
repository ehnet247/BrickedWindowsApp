using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BrickedWindowsApp.App.LoginForm
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView : Window
    {
        public LoginView()
        {
            this.InitializeComponent();
            DataContext = new LoginViewModel();
        }
        //https://learn.microsoft.com/en-us/windows/apps/develop/data-binding/data-binding-overview
        public LoginViewModel DataContext { get; set; }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
