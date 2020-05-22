using System.Windows.Input;
using IntratimeClient.View;
using Xamarin.Forms;

namespace IntratimeClient.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private readonly INavigation _navigation;
        
        private string _messageLabel;
        private string _email;
        private string _token;

        private bool _canExecuteCommands;

        public ICommand ListClockCommand { set; get; }
        public ICommand BulkClockCommand { set; get; }

        public bool CanExecuteCommands 
        { 
            get => _canExecuteCommands;
            set
            {
                _canExecuteCommands = value;
                ((Command)ListClockCommand).ChangeCanExecute();
                ((Command)BulkClockCommand).ChangeCanExecute();
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

        public string Token
        {
            get => _token;
            set => Set(ref _token, value);
        }

        public MenuViewModel(INavigation navigation)
        {
            _navigation = navigation;
            ListClockCommand = new Command(ListClocksAction, () => CanExecuteCommands);
            BulkClockCommand = new Command(BulkClocksAction, () => CanExecuteCommands);
        }

        private async void BulkClocksAction()
        {
            CanExecuteCommands = false;
            await _navigation.PushAsync(new BulkClockPage());
            CanExecuteCommands = true;
        }

        private async void ListClocksAction()
        {
            CanExecuteCommands = false;
            await _navigation.PushAsync(new ListClocksPage());
            CanExecuteCommands = true;
        }
    }
}