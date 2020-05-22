using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using IntratimeClient.Services.IntratimeClientAPI;
using Xamarin.Forms;

namespace IntratimeClient.ViewModel
{
    public class BulkClockViewModel : ViewModelBase
    {
        private DateTime? _startDate;
        private DateTime? _endDate;
        private List<ClocksGroupedByDayCell> _existingClocks;
        private string _messageLabel;
        private string _email;
        private string _token;

        private bool _skipIfClockAlreadyExists;
        private bool _skipWeekends;
        private bool _skipHolidays;

        private bool _canExecuteCommands;
        public ICommand BulkClockCommand { set; get; }

        public bool CanExecuteCommands
        {
            get => _canExecuteCommands;
            set
            {
                _canExecuteCommands = value;
                ((Command) BulkClockCommand).ChangeCanExecute();
            }
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

        public bool SkipWeekends
        {
            get => _skipWeekends;
            set => Set(ref _skipWeekends, value);
        }

        public bool SkipHolidays
        {
            get => _skipHolidays;
            set => Set(ref _skipHolidays, value);
        }

        public bool SkipIfClockAlreadyExists
        {
            get => _skipIfClockAlreadyExists;
            set => Set(ref _skipIfClockAlreadyExists, value);
        }

        public BulkClockViewModel()
        {
            BulkClockCommand = new Command(BulkClockAction, () => CanExecuteCommands);
        }

        private async void BulkClockAction()
        {
            CanExecuteCommands = false;

            if (!StartDate.HasValue)
            {
                MessageLabel = "Select Start date";
                return;
            }

            if (!EndDate.HasValue)
            {
                MessageLabel = "Select End date";
                return;
            }

            if (EndDate < StartDate)
            {
                MessageLabel = "End date must be bigger than Start date.";
                return;
            }

            var clockService = new ClockService();

            var clocksToCreate = await clockService.GenerateClockRequest(Token,
                StartDate.Value,
                EndDate.Value,
                SkipWeekends,
                SkipIfClockAlreadyExists);

            if (clocksToCreate.Success)
            {
                var current = 1;

                foreach (var clockValueTuple in clocksToCreate.ClockRecords)
                {
                    var foo = await clockService.CreateClock(Token, clockValueTuple.ClockType, clockValueTuple.Time);

                    MessageLabel = $"({current}/{clocksToCreate.ClockRecords.Count()})";
                    MessageLabel = $"{MessageLabel} {clockValueTuple.Time.ToString("yyyy-MM-dd HH:mm:ss")}";
                    MessageLabel = $"{MessageLabel} {clockValueTuple.ClockType.ToString()}";

                    if (foo)
                    {
                        MessageLabel = $"{MessageLabel} Succeed";
                    }
                    else
                    {
                        MessageLabel = $"{MessageLabel} Failed";
                        break;
                    }

                    current++;
                }
            }
            else
            {
                MessageLabel = "Error computing clock requests";
            }

            CanExecuteCommands = true;
        }
    }
}