using System.Threading.Tasks;
using PostCodes.WebAPI.Services.Interfaces;
using PostCodes.Common.Services.Interfaces;
using Microsoft.Extensions.Logging;
using PostCodes.WebAPI.Data.Model;
using Newtonsoft.Json;

namespace PostCodes.WebAPI.Services
{
    public class PostCodesService : IPostCodesService
    {

        private readonly IHttpClientRepository _httpClientRepository;
        private readonly IPostCodesEnvironmentConfig _postCodesEnvironmentConfig;
        private readonly ILogger _logger;

        public PostCodesService( IHttpClientRepository httpClientRepository, IPostCodesEnvironmentConfig postCodesEnvironmentConfig, ILogger<PostCodesService> logger)
        {
            _httpClientRepository = httpClientRepository;
            _postCodesEnvironmentConfig = postCodesEnvironmentConfig;
            _logger = logger;
        }

        public async Task<PostCodeResult> GetPostCodeDetailAsync(string postCode)
        {
            var url = string.Format(_postCodesEnvironmentConfig.GetPostCodeLookupRoute(), postCode);
            _logger.LogInformation("Get PostCodes url: {0} " , url);
            string response = await _httpClientRepository.Get(url);
            _logger.LogInformation("PostCodes Details response => {0} ", response);
            PostCodeResult result = JsonConvert.DeserializeObject<PostCodeResult>(response);
            return result;
        }

        public async Task<PostCodesLookupResult> SearchPostCodesAsync(string postCode)
        {
            var url = string.Format(_postCodesEnvironmentConfig.GetAutoCompleteRoute(), postCode);
            _logger.LogInformation("Get PostCodes Lookup url: {0} ", url);
            string response = await _httpClientRepository.Get(url);
            _logger.LogInformation("PostCodes Lookup response: {0} ", response);
            PostCodesLookupResult result = JsonConvert.DeserializeObject<PostCodesLookupResult>(response);
            return result;
        }
    }
}
