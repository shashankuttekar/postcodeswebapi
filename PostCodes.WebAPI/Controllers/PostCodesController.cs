using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostCodes.WebAPI.Services.Interfaces;

namespace PostCodes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PostCodesController : ControllerBase
    {
        private readonly IPostCodesService _postCodesService;
        private readonly ILogger _logger;

        public PostCodesController(IPostCodesService postCodesService, ILogger<PostCodesController> logger)
        {
            _postCodesService = postCodesService;
            _logger = logger;
        }

        /// <summary>
        /// This api is used to get post codes on lookup post code input value
        /// </summary>
        /// <param name="postCode">User input post code</param>
        /// <returns></returns>
        [HttpGet("{searchParam}/autocomplete")]
        public async Task<IActionResult> SearchPostCodes(string searchParam)
        {
            _logger.LogInformation("PostCodes search parameter: {0}", searchParam);
            var data = await _postCodesService.SearchPostCodesAsync(searchParam);
            return Ok(data);
        }

        /// <summary>
        /// This api is used to get input postcode details
        /// </summary>
        /// <param name="postCode">Valid post code</param>
        /// <returns></returns>
        [HttpGet("{postCode}")]
        public async Task<IActionResult> GetPostCodes(string postCode)
        {
            if (string.IsNullOrWhiteSpace(postCode))
            {
                return BadRequest("Please provide post code");
            }
            _logger.LogInformation("PostCodes get detail called for post code: {0}", postCode);
            var data = await _postCodesService.GetPostCodeDetailAsync(postCode);
            return Ok(data);
        }
    }
}
