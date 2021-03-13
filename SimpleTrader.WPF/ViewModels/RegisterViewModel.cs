using SimpleTrader.WPF.Commands;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterViewModel(IRenavigator loginRenavigator, IAuthenticator authenticator,
            IRenavigator registerRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();

            RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
            ViewLoginCommand = new RenavigateCommand(loginRenavigator);
        }

        public ICommand RegisterCommand { get; }
        public ICommand ViewLoginCommand { get; }
        public MessageViewModel ErrorMessageViewModel { get; set; }
        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }


        private string _email;

        public string Email
        {
            get { return _email; }
            set 
            { 
                _email = value;
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                OnPropertyChanged(nameof(CanRegister));
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
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
                OnPropertyChanged(nameof(CanRegister));
            }
        }

        public bool CanRegister => !string.IsNullOrEmpty(Email) &&
                                   !string.IsNullOrEmpty(Username) &&
                                   !string.IsNullOrEmpty(Password) &&
                                   !string.IsNullOrEmpty(ConfirmPassword);

        public override void Dispose()
        {
            ErrorMessageViewModel.Dispose();
            
            base.Dispose();
        }

    }
}
