using System.Net;
using System.Threading.Tasks;
using IntratimeClient.Services.ClientAPI.Request;
using IntratimeClient.Services.IntratimeClientAPI.Request.Command;
using IntratimeClient.Services.IntratimeClientAPI.Response.Command;
using Newtonsoft.Json;

namespace IntratimeClient.Services.IntratimeClientAPI
{
    public class LoginService
    {
        public async Task<(bool Success, string Token)> Login(string email, string password)
        {
            var loginRequest = new LoginCommandRequest(email, password);
            var loginCommandResult = await HttpHelper.CallToApi(loginRequest);
            var token = string.Empty;

            if (loginCommandResult.IsSuccessful
                && loginCommandResult.StatusCode == HttpStatusCode.Created)
            {
                var result = JsonConvert.DeserializeObject<LoginCommandResponse>(loginCommandResult.Content);

                token = result.USER_TOKEN;

                return (true, token);
            }

            return (false, token);
        }
    }
}
