using SimpleTrader.Domain.Exceptions;
using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigations;
using SimpleTrader.WPF.ViewModels;
using SimpleTrader.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _loginViewModel;
        private readonly IRenavigator _renavigator;

        public LoginCommand(IAuthenticator authenticator, LoginViewModel loginViewModel, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _loginViewModel = loginViewModel;
            _renavigator = renavigator;

            _loginViewModel.PropertyChanged += _loginViewModel_PropertyChanged;
        }

        private void _loginViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.CanLogin))
            {
                OnCanExecuteChanged();
            }
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _loginViewModel.ErrorMessgae = string.Empty;

            try
            {
                await _authenticator.Login(_loginViewModel.Username, _loginViewModel.Password);
                _renavigator.Renavigator();
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessgae = "User does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessgae = "Incorrect password.";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessgae = "Login failed.";
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _loginViewModel.CanLogin && base.CanExecute(parameter);
        }
    }
}
