using IntratimeClient.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntratimeClient.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuViewModel ViewModel => (MenuViewModel)BindingContext;

        public MenuPage(User user)
        {
            App.User = user;
            App.User.Save();
            
            var viewModel = new MenuViewModel(Navigation)
            {
                Token = user.Token,
                Email = user.Email,
                CanExecuteCommands = true
            };

            BindingContext = viewModel;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.CanExecuteCommands = true;
        }
    }
}