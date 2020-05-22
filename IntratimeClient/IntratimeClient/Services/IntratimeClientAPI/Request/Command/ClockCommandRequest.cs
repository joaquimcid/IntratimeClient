using System;
using System.Collections.Generic;
using IntratimeClient.Services.ClientAPI.Request;
using IntratimeClient.Services.IntratimeClientAPI.Dto;
using IntratimeClient.Services.IntratimeClientAPI.Response.Command;
using RestSharp;

namespace IntratimeClient.Services.IntratimeClientAPI.Request.Command
{
    public class ClockCommandRequest : IRequest<ClockCommandResponse>
    {
        public Method Verb { get; set; }
        public string Path { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public ClockCommandResponse Response { get; set; }

        public ClockCommandRequest(string token, UserAction userAction, DateTime dateTime)
        {
            Verb = Method.POST;
            Path = Constants.UrlClocking.FullUrl();

            Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Accept", "application/vnd.apiintratime.v1+json"),
                new KeyValuePair<string, string>("token", token),
            };

            var userActionInt = (int) userAction;
            var dateTimeParsed = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            
            Parameters = new Dictionary<string, object>
            {
                {"user_action", userActionInt.ToString() },
                {"user_timestamp", dateTimeParsed}
            };
        }
    }
}