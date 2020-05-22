using System;
using IntratimeClient.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntratimeClient.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListClocksPage : ContentPage
    {
        public ListClocksViewModel ViewModel => (ListClocksViewModel)BindingContext;
        
        public ListClocksPage()
        {
            BindingContext = new ListClocksViewModel() { Email = App.User.Email, Token = App.User.Token };

            SelectCurrentMonthDatePickers();

            InitializeComponent();
        }

        private void SelectCurrentMonthDatePickers()
        {
            DateTime dateNow = DateTime.Now;

            var firstDayOfMonth = new DateTime(dateNow.Year, dateNow.Month, 1);
            var lastDayOfMonth = new DateTime(dateNow.Year, dateNow.Month, DateTime.DaysInMonth(dateNow.Year, dateNow.Month));

            ViewModel.StartDate = firstDayOfMonth;
            ViewModel.EndDate = lastDayOfMonth;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.CanExecuteCommands = true;
            //await SearchResults(ViewModel.StartDate, ViewModel.EndDate);
        }
    }
}