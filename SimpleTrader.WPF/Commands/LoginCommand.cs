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
        }


        public override async Task ExecuteAsync(object parameter)
        {
            _loginViewModel.ErrorMessgae = string.Empty;

            try
            {
                await _authenticator.Login(_loginViewModel.Username, parameter.ToString());
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
    }
}
