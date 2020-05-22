using System.Collections.Generic;
using RestSharp;

namespace IntratimeClient.Services.ClientAPI.Request
{
    public interface IRequest<T>
    {
        Method Verb { get; set; }
        string Path { get; set; }
        List<KeyValuePair<string, string>> Headers { get; set; }
        Dictionary<string, object> Parameters { get; set; }
        T Response { get; set; }
    }
}