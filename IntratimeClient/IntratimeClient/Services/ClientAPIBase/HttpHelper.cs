using RestSharp;
using System.Threading.Tasks;

namespace IntratimeClient.Services.ClientAPI.Request
{
    public static class HttpHelper
    {
        public static async Task<IRestResponse<T>> CallToApi<T>(IRequest<T> operation)
        {
            var client = new RestClient(operation.Path)
            {
                Timeout = -1
            };
            var request = new RestRequest(operation.Verb);

            if (operation.Headers != null)
            {
                foreach (var header in operation.Headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (operation.Parameters != null)
            {
                foreach (var parameter in operation.Parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            var response = await client.ExecuteAsync<T>(request);

            return response;
        }
    }
}