using System;
using System.Collections.Generic;
using IntratimeClient.Services.ClientAPI.Request;
using IntratimeClient.Services.IntratimeClientAPI.Response.Query;
using RestSharp;

namespace IntratimeClient.Services.IntratimeClientAPI.Request.Query
{
    public class ClockListQueryRequests : IRequest<List<ClockQueryResponse>>
    {
        public Method Verb { get; set; }
        public string Path { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public List<ClockQueryResponse> Response { get; set; }
        public ClockListQueryRequests(string token, DateTime? dateFrom, DateTime? dateTo)
        {
            Verb = Method.GET;
            Path = Constants.UrlGetClocking.FullUrl();
            //var client = new RestClient("https://newapi.intratime.es/api/user/clockings?from=2017-10-18 14:09:18&type=0,1,2,3");

            var filteredPath = string.Empty;
            if (dateFrom.HasValue && dateFrom.Value != DateTime.MinValue)
            {
                filteredPath = $"?from={dateFrom.Value.ToString("yyyy-MM-dd")} 00:00:00";
            }

            if (dateTo.HasValue && dateTo.Value != DateTime.MinValue)
            {
                if (string.IsNullOrEmpty(filteredPath))
                {
                    filteredPath = $"{filteredPath}?";
                }
                else
                {
                    filteredPath = $"{filteredPath}&";
                }
                filteredPath = $"{filteredPath}to={dateTo.Value.ToString("yyyy-MM-dd")} 23:59:59";

            }

            Path = $"{Path}{filteredPath}";
            
            Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Accept", "application/vnd.apiintratime.v1+json"),
                new KeyValuePair<string, string>("token", token),
            };
        }
    }
}