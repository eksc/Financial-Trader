using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IAuthenticator authenticator)
        {
            LoginCommand = new LoginCommand(authenticator, this);
        }

        public ICommand LoginCommand { get; }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        
    }
}
