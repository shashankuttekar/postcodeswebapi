using Xunit;
using Moq;
using PostCodes.WebAPI.Services.Interfaces;
using PostCodes.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using PostCodes.WebAPI.Data.Model;
using Microsoft.AspNetCore.Mvc;


namespace PostCodesWebAPITest
{
    public class PostCodesControllerTest
    {
        #region Property
        public Mock<IPostCodesService> postServiceMock = new Mock<IPostCodesService>();
        public Mock<ILogger<PostCodesController>> logMock = new Mock<ILogger<PostCodesController>>();
        #endregion
        [Fact]
        public async void GetLookUp()
        {
            PostCodesLookupResult result = new PostCodesLookupResult();
            result.Result = new System.Collections.Generic.List<string>
            {
                "PA10 2AB",
                "PA10 2AE",
                "PA10 2AD"
            };
            postServiceMock.Setup(p => p.SearchPostCodesAsync("AL")).ReturnsAsync(result);
            PostCodesController postCodesController = new PostCodesController(postServiceMock.Object, logMock.Object);
            var actionResult = await postCodesController.SearchPostCodes("AL") as OkObjectResult;
            var actualPostCodesResult = actionResult.Value as PostCodesLookupResult;
            Assert.Equal(actualPostCodesResult, result);

        }


        [Fact]
        public async void GetPostCodeDetails()
        {
            PostCodeResult result = new PostCodeResult
            {
                PostCodeDetails = new PostCodeDetails
                {
                    Latitude = 51.792326d,
                    Country = "England",
                    Region = "East of England",
                    Codes = new Code
                    {
                        AdminDistrict = "E07000067",
                        ParliamentaryConstituency = "E14001045"
                    }
                }
            };
            postServiceMock.Setup(p => p.GetPostCodeDetailAsync("CM8 1EF")).ReturnsAsync(result);
            PostCodesController postCodesController = new PostCodesController(postServiceMock.Object, logMock.Object);
            var actionResult = await postCodesController.GetPostCodes("CM8 1EF") as OkObjectResult;
            var actualPostCodesResult = actionResult.Value as PostCodeDetails;
            Assert.Equal(actualPostCodesResult, result.PostCodeDetails);

        }
    }
}
