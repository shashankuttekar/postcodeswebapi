using System.Threading.Tasks;
using PostCodes.WebAPI.Services.Interfaces;
using PostCodes.Common.Services.Interfaces;
using Microsoft.Extensions.Logging;
using PostCodes.WebAPI.Data.Model;
using System.Text.Json;
using AutoMapper;
using PostCodes.DataTransferModel;

namespace PostCodes.WebAPI.Services
{
    /// <summary>
    /// This class implemented IPostCodesService. It provide functionality to do post codes lookup and get post codes details
    /// </summary>
    public class PostCodesService : IPostCodesService
    {

        private readonly IHttpClientRepository _httpClientRepository;
        private readonly IPostCodesEnvironmentConfig _postCodesEnvironmentConfig;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public PostCodesService( IHttpClientRepository httpClientRepository, IPostCodesEnvironmentConfig postCodesEnvironmentConfig, ILogger<PostCodesService> logger, IMapper mapper)
        {
            _httpClientRepository = httpClientRepository;
            _postCodesEnvironmentConfig = postCodesEnvironmentConfig;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// This function return details of postcodes
        /// </summary>
        /// <param name="postCode">Valid post code string</param>
        /// <returns></returns>
        public async Task<PostCodesDetailsDataTransferModel> GetPostCodeDetailAsync(string postCode)
        {
            var url = string.Format(_postCodesEnvironmentConfig.GetPostCodeLookupRoute(), postCode);
            _logger.LogInformation("Get PostCodes url: {0} " , url);
            string response = await _httpClientRepository.Get(url);
            _logger.LogInformation("PostCodes Details response => {0} ", response);
            PostCodesResult result = JsonSerializer.Deserialize<PostCodesResult>(response, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            PostCodesDetailsDataTransferModel postCodesDetailsDataTransferModel = _mapper.Map<PostCodesDetailsDataTransferModel>(result);
            return postCodesDetailsDataTransferModel;
        }

        /// <summary>
        /// This function provide post codes lookup for user provide string
        /// </summary>
        /// <param name="postCode">Any valid post code lookup string</param>
        /// <returns></returns>
        public async Task<PostCodesLookupDataTransferModel> SearchPostCodesAsync(string postCode)
        {
            var url = string.Format(_postCodesEnvironmentConfig.GetAutoCompleteRoute(), postCode);
            _logger.LogInformation("Get PostCodes Lookup url: {0} ", url);
            string response = await _httpClientRepository.Get(url);
            _logger.LogInformation("PostCodes Lookup response: {0} ", response);
            PostCodesLookupResult result = JsonSerializer.Deserialize<PostCodesLookupResult>(response, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            PostCodesLookupDataTransferModel postCodesLookupDataTransferModel = _mapper.Map<PostCodesLookupDataTransferModel>(result);
            return postCodesLookupDataTransferModel;
        }
    }
}
