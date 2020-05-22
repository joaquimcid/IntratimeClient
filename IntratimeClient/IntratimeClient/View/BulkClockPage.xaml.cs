using System;
using System.Collections.Generic;
using IntratimeClient.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IntratimeClient.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BulkClockPage : ContentPage
    {
        public BulkClockViewModel ViewModel => (BulkClockViewModel) BindingContext;

        public BulkClockPage()
        {
            BindingContext = new BulkClockViewModel()
            {
                Token = App.User.Token,
                Email = App.User.Email,
                SkipIfClockAlreadyExists = true,
                SkipWeekends = true,
                SkipHolidays = false,
                ExistingClocks = new List<ClocksGroupedByDayCell>(),
            };

            SelectCurrentMonthDatePickers();
            InitializeComponent();
        }

        private void SelectCurrentMonthDatePickers()
        {
            DateTime dateNow = DateTime.Now;

            var firstDayOfMonth = new DateTime(dateNow.Year, dateNow.Month, 1);
            //var lastDayOfMonth = new DateTime(dateNow.Year, dateNow.Month, DateTime.DaysInMonth(dateNow.Year, dateNow.Month));
            var lastDayOfMonth = dateNow;

            ViewModel.StartDate = firstDayOfMonth;
            ViewModel.EndDate = lastDayOfMonth;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.CanExecuteCommands = true;
        }
    }
}