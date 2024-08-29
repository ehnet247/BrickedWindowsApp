using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BrickedWindowsApp.App.LoginForm
{
    public class LoginViewModel : ObservableRecipient
    {
        // https://www.youtube.com/watch?v=FGqj4q09NtA
        // 11 min50
        private string username;
        private SecureString password;
        private string errorMessage;
        private bool isViewVisible = true;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public SecureString Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        public bool IsViewVisible
        {
            get => isViewVisible;
            set
            {
                isViewVisible = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, canExecute: CanLogin);
            RecoverPasswordCommand = new RelayCommand(RecoverPassword);
            ShowPasswordCommand = new RelayCommand(ShowPassword);
            RememberPasswordCommand = new RelayCommand(RememberPassword);
        }

        private bool CanLogin()
        {
            if (!string.IsNullOrWhiteSpace(Username) && Username.Length >= 3 &&
                Password.Length >= 3)
                return true;
            else
                return false;
        }

        //[RelayCommand(CanExecute = nameof(CanLogin))]
        private void Login()
        {
        }

        private void RecoverPassword()
        {
        }

        private void ShowPassword()
        {
        }

        private void RememberPassword()
        {
        }
    }
}
