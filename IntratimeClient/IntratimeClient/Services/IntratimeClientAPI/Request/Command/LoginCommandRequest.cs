using System.Collections.Generic;
using IntratimeClient.Services.ClientAPI.Request;
using IntratimeClient.Services.IntratimeClientAPI.Response.Command;
using RestSharp;

namespace IntratimeClient.Services.IntratimeClientAPI.Request.Command
{
    public class LoginCommandRequest : IRequest<LoginCommandResponse>
    {
        public LoginCommandRequest(string userName, string password)
        {
            Verb = Method.POST;
            Path = Constants.UrlLogin.FullUrl();
            Parameters = new Dictionary<string, object>
            {
                { "user", userName }, 
                { "pin", password }
            };

            Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Accept", "application/vnd.apiintratime.v1+json"),
                new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded; charset:utf8"),
                new KeyValuePair<string, string>("Content-Type", "application/x-www-form-urlencoded"),
            };
        }

        public Method Verb { get; set; }
        public string Path { get; set; }
        public List<KeyValuePair<string, string>> Headers { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
        public LoginCommandResponse Response { get; set; }

        //public string Login(string username, string password)
        //{
        //    var parameters = new Dictionary<string, object> {{"user", username}, {"pin", password}};

        //    var result = HttpHelper.CallToApi<LoginCommandResponse>(Method.POST, Constants.UrlLogin.FullUrl(), parameters);

        //    return result.USER_TOKEN;
        //}
    }
}
