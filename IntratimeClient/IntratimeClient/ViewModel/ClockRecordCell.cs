using System;
using IntratimeClient.Services.IntratimeClientAPI.Dto;

namespace IntratimeClient.ViewModel
{
    public class ClockRecordCell
    {
        public ClockRecordCell()
        {
        }

        public ClockRecordCell(UserAction userAction, DateTime date, int hour, int minutes, int seconds, RequestStatus status)
        {
            UserAction = userAction;
            Time = new DateTime(date.Year, date.Month, date.Day, hour, minutes, seconds);
            Status = status;
        }

        public RequestStatus Status { get; set; }
        public DateTime Time { get; set; }
        public UserAction UserAction { get; set; }
    }
}
