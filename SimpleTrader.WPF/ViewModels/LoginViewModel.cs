using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            LoginCommand = new LoginCommand(authenticator, this, loginRenavigator);
            ErrorMessageViewModel = new MessageViewModel();
            ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
        }

        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }

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

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; set; }
        public string ErrorMessgae
        {
            set => ErrorMessageViewModel.Message = value;
        }

    }
}
