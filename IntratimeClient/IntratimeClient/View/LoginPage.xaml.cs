using IntratimeClient.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntratimeClient.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel ViewModel => (LoginViewModel) BindingContext;
        
        public LoginPage()
        {
            var viewModel = new LoginViewModel(Navigation);
            
            viewModel.RememberCredentials = App.User.RememberCredentials;
            if (!string.IsNullOrEmpty(App.User.Email)) viewModel.Email = App.User.Email;
            if (!string.IsNullOrEmpty(App.User.Password)) viewModel.Password = App.User.Password;
            
            BindingContext = viewModel;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (string.IsNullOrEmpty(EmailEntry.Text)) EmailEntry.Focus();
            else if (string.IsNullOrEmpty(PasswordEntry.Text)) PasswordEntry.Focus();
            else LoginButton.Focus();

            ViewModel.MessageLabel = string.Empty;
            ViewModel.CanExecuteCommands = true;
        }
    }
}