using IntratimeClient.ViewModel;
using Xamarin.Forms;

namespace IntratimeClient.View
{
    public partial class App : Application
    {
        public static User User { get; set; }

        public App()
        {
            User = UserExtension.Get();

            var navPage = new NavigationPage(new LoginPage());

            MainPage = navPage;
            
            InitializeComponent();
        }
    }
}
