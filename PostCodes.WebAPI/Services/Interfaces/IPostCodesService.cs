using PostCodes.WebAPI.Data.Model;
using System.Threading.Tasks;

namespace PostCodes.WebAPI.Services.Interfaces
{
    public interface IPostCodesService
    {
        Task<PostCodesLookupResult> SearchPostCodesAsync(string postCode);
        Task<PostCodeResult> GetPostCodeDetailAsync(string postCode);
    }
}
