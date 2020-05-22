using System.Windows.Input;
using IntratimeClient.Services.IntratimeClientAPI;
using IntratimeClient.View;
using Xamarin.Forms;

namespace IntratimeClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private INavigation _navigation;

        private string _email;
        private string _password;
        private string _token;
        private string _messageLabel;
        private bool _rememberCredentials;

        private bool _canExecuteLogin;
        
        public ICommand LoginCommand { get; set; }

        public bool CanExecuteCommands
        {
            get => _canExecuteLogin;
            set
            {
                _canExecuteLogin = value;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        
        public string MessageLabel
        {
            get => _messageLabel;
            set => Set(ref _messageLabel, value);
        }

        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string Token
        {
            get => _token;
            set => Set(ref _token, value);
        }

        public bool RememberCredentials
        {
            get => _rememberCredentials;
            set => Set(ref _rememberCredentials, value);
        }


        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            LoginCommand = new Command(LoginAction, () => CanExecuteCommands);
            MessageLabel = string.Empty;
        }

        private async void LoginAction()
        {
            CanExecuteCommands = false;
            MessageLabel = "Login ...";

            var loginModel = new LoginService();
            var isValid = await loginModel.Login(Email, Password);

            if (isValid.Success)
            {
                MessageLabel = "Login success";
                Token = isValid.Token;

                var user = new User() {Email = Email, Password = Password, Token = Token, RememberCredentials = RememberCredentials};
                
                await _navigation.PushAsync(new MenuPage(user));
            }
            else
            {
                MessageLabel = "Login failed";
                CanExecuteCommands = true;
            }
        }
    }
}
