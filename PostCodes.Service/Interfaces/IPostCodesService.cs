using PostCodes.DataTransferModel;
using PostCodes.WebAPI.Data.Model;
using System.Threading.Tasks;

namespace PostCodes.WebAPI.Services.Interfaces
{
    public interface IPostCodesService
    {
        Task<PostCodesLookupDataTransferModel> SearchPostCodesAsync(string postCode);
        Task<PostCodesDetailsDataTransferModel> GetPostCodeDetailAsync(string postCode);
    }
}
