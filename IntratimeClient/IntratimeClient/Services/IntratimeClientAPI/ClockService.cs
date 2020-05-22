using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IntratimeClient.Services.ClientAPI.Request;
using IntratimeClient.Services.IntratimeClientAPI.Dto;
using IntratimeClient.Services.IntratimeClientAPI.Request.Command;
using IntratimeClient.Services.IntratimeClientAPI.Request.Query;
using IntratimeClient.Services.IntratimeClientAPI.Response.Query;
using Newtonsoft.Json;
using RestSharp;

namespace IntratimeClient.Services.IntratimeClientAPI
{
    public class ClockService
    {
        public async Task<(bool IsSuccessful, List<ClockQueryResponse> Result)> GetClocksList(string token, DateTime dateFrom, DateTime dateTo)
        {
            (bool IsSuccessful, List<ClockQueryResponse> Result) result = (false, new List<ClockQueryResponse>());

            var clocks = new ClockListQueryRequests(token, dateFrom, dateTo);
            var clocksQuery = await HttpHelper.CallToApi(clocks);

            if (clocksQuery.IsSuccessful && clocksQuery.StatusCode == HttpStatusCode.OK)
            {
                result.Result = new List<ClockQueryResponse>(); 

                var resultArray = JsonConvert.DeserializeObject<ClockQueryResponse[]>(clocksQuery.Content);
                result.Result = resultArray.ToList();
                result.IsSuccessful = true;
            }

            return result;
        }

        public async Task<bool> CreateClock(string token, UserAction userAction, DateTime day)
        {
            if (day == default) return false;
            if (token == null) return false;

            //await Task.Delay(2000);
            //return true;

            var clockRequest = new ClockCommandRequest(token, userAction, day);
            var clockRequestResult = await HttpHelper.CallToApi(clockRequest);

            var result = IsCallSucced(clockRequestResult);

            return result;
        }

        public async Task<(bool Success, IEnumerable<(UserAction ClockType, DateTime Time)> ClockRecords)> GenerateClockRequest(
            string token,
            DateTime startDateTime = default,
            DateTime stopDateTime = default,
            bool? skipWeekend = null,
            bool? skipClockIfExists = null)
        {
            var result = (Success: false, ClockRecords: new List<(UserAction ClockType, DateTime Time)>());

            if (token == null) return result;
            if (startDateTime != default
                && stopDateTime != default
                && stopDateTime.Date < startDateTime.Date) return result;

            var workingDaysInMonth = GetDateRange(startDateTime, stopDateTime, skipWeekend);
            
            var clocksOnSystem = new List<ClockQueryResponse>();

            if (skipClockIfExists.HasValue && skipClockIfExists.Value)
            {
                var (clockIsSuccessful, clockResult) = await GetClocksList(token, startDateTime, stopDateTime);
                if (!clockIsSuccessful)
                {
                    return result;
                }

                clocksOnSystem = clockResult;
            }
            
            foreach (var day in workingDaysInMonth)
            {
                if (startDateTime != default && day.Date < startDateTime) continue;
                if (stopDateTime != default && day.Date > stopDateTime) continue;

                foreach (var clockGivenDayGenerateTime in GivenDayGenerateTimes(day))
                {
                    if (!clocksOnSystem.Any(c => 
                        c.InoutDate.Equals(clockGivenDayGenerateTime.Time) 
                        && c.InoutType == (long)clockGivenDayGenerateTime.ClockType))
                    {
                        result.ClockRecords.Add(clockGivenDayGenerateTime);
                    }
                }
            }

            result.Success = true;

            return result;
        }

        private IEnumerable<(UserAction ClockType, DateTime Time)> GivenDayGenerateTimes(DateTime day)
        {
            var result = new List<(UserAction ClockType, DateTime Time)>
            {
                (UserAction.Entrada, GenerateRandomMinutesSeconds(day, 9)),
                (UserAction.Pause, GenerateRandomMinutesSeconds(day, 13)),
                (UserAction.Volver, GenerateRandomMinutesSeconds(day, 14)),
                (UserAction.Salida, GenerateRandomMinutesSeconds(day, 18))
            };

            return result;
        }

        private IEnumerable<DateTime> GetDateRange(DateTime fromDate, DateTime toDate, bool? skipWeekend)
        {
            var resultList = new List<DateTime>();

            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                if (skipWeekend.HasValue
                    && skipWeekend.Value
                    && (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday))
                {
                    continue;
                }

                resultList.Add(date);
            }

            return resultList;
        }

        private static DateTime GenerateRandomMinutesSeconds(DateTime day, int hour)
        {
            var rnd = new Random();
            int minutes = rnd.Next(0, 11); // creates a number between 0 and 10
            int seconds = rnd.Next(0, 60); // creates a number between 0 and 59

            var time = new DateTime(day.Year, day.Month, day.Day, hour, minutes, seconds);
            return time;
        }

        private bool IsCallSucced<T>(IRestResponse<T> response)
        {
            return response.IsSuccessful && response.StatusCode == HttpStatusCode.Created;
        }
    }
}
