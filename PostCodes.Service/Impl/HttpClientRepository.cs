using System.Net.Http;
using System.Threading.Tasks;
using PostCodes.WebAPI.Services.Interfaces;

namespace PostCodes.WebAPI.Services
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClient client;
        public HttpClientRepository(IHttpClientFactory clientFactory)
        {
            
            client = clientFactory.CreateClient("PostCodesWebAPI");
        }
        public virtual async Task<string> Get(string url)
        {
            var response = await client.GetAsync(url);
            string stringResponse;
            if (response.IsSuccessStatusCode)
            {
                stringResponse = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return stringResponse;
        }
    }
}
