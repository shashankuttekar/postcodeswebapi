using Xunit;
using Moq;
using PostCodes.WebAPI.Services.Interfaces;
using PostCodes.WebAPI.Controllers;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using PostCodes.DataTransferModel;

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
            PostCodesLookupDataTransferModel result = new PostCodesLookupDataTransferModel();
            result.result = new System.Collections.Generic.List<string>
            {
                "PA10 2AB",
                "PA10 2AE",
                "PA10 2AD"
            };
            postServiceMock.Setup(p => p.SearchPostCodesAsync("AL")).ReturnsAsync(result);
            PostCodesController postCodesController = new PostCodesController(postServiceMock.Object, logMock.Object);
            var actionResult = await postCodesController.SearchPostCodes("AL") as OkObjectResult;
            var actualPostCodesResult = actionResult.Value as PostCodesLookupDataTransferModel;
            Assert.Equal(actualPostCodesResult, result);

        }


        [Fact]
        public async void GetPostCodeDetails()
        {
            PostCodesDetailsDataTransferModel result = new PostCodesDetailsDataTransferModel
            {
                area = "Midlands",
                country = "England",
                region = "East of England",
                admin_district = "E07000067",
                parliamentary_constituency = "E14001045"
                
            };
            postServiceMock.Setup(p => p.GetPostCodeDetailAsync("CM8 1EF")).ReturnsAsync(result);
            PostCodesController postCodesController = new PostCodesController(postServiceMock.Object, logMock.Object);
            var actionResult = await postCodesController.GetPostCodes("CM8 1EF") as OkObjectResult;
            var actualPostCodesResult = actionResult.Value as PostCodesDetailsDataTransferModel;
            Assert.Equal(actualPostCodesResult, result);

        }

        [Fact]
        public async void PostCodeDetailsBadRequest()
        {
            
            //postServiceMock.Setup(p => p.GetPostCodeDetailAsync(""));
            PostCodesController postCodesController = new PostCodesController(postServiceMock.Object, logMock.Object);
            var response = await postCodesController.GetPostCodes("") as BadRequestObjectResult;

            Assert.Equal(400, response.StatusCode);
            

        }
    }
}
