using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using IntratimeClient.Services.IntratimeClientAPI;
using IntratimeClient.Services.IntratimeClientAPI.Response.Query;
using IntratimeX.Mappers;
using Xamarin.Forms;

namespace IntratimeClient.ViewModel
{
    public class ListClocksViewModel : ViewModelBase
    {
        private DateTime? _startDate;
        private DateTime? _endDate;
        private List<ClocksGroupedByDayCell> _existingClocks; 
        private string _messageLabel;
        private string _email;
        private string _token;

        private bool _canExecuteCommands;

        public ICommand SearchClocksCommand { set; get; }

        public bool CanExecuteCommands
        {
            get => _canExecuteCommands;
            set
            {
                _canExecuteCommands = value;
                ((Command)SearchClocksCommand).ChangeCanExecute();
            }
        }

        public List<ClocksGroupedByDayCell> ExistingClocks
        {
            get => _existingClocks;
            set => Set(ref _existingClocks, value);
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

        public DateTime? StartDate
        {
            get => _startDate;
            set => Set(ref _startDate, value);
        }

        public DateTime? EndDate
        {
            get => _endDate;
            set => Set(ref _endDate, value);
        }

        public ListClocksViewModel()
        {
            SearchClocksCommand = new Command(SearchAction, () => CanExecuteCommands);
        }

        async void SearchAction()
        {
            CanExecuteCommands = false;

            var starting = StartDate ?? new DateTime();
            var ending = EndDate ?? new DateTime();
            var clockService = new ClockService();

            bool clockIsSuccessful;
            List<ClockQueryResponse> clockResult;
            (clockIsSuccessful, clockResult) = await clockService.GetClocksList(Token, starting, ending);
            if (clockIsSuccessful)
            {
                MessageLabel = string.Empty;
                var clockMapper = new ClockRowMapper();
                var groupedClocksMapper = new GroupedClockRecordCellMapper(clockMapper);

                var listOfClockRecordCells = groupedClocksMapper.Map(clockResult).ToList();
                ExistingClocks = listOfClockRecordCells;
            }
            else
            {
                MessageLabel = "Could not retrieve from Intratime API the list of recorded clocks.";
            }

            CanExecuteCommands = true;
        }

    }
}