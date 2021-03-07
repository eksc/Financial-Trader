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
        public LoginViewModel(IAuthenticator authenticator, IRenavigator renavigator)
        {
            LoginCommand = new LoginCommand(authenticator, this, renavigator);
            ErrorMessageViewModel = new MessageViewModel();
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

        public MessageViewModel ErrorMessageViewModel { get; set; }
        public string ErrorMessgae
        {
            set => ErrorMessageViewModel.Message = value;
        }

    }
}
