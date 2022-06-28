using System.Threading.Tasks;

namespace PostCodes.WebAPI.Services.Interfaces
{
    public interface IHttpClientRepository
    {
        Task<string> Get(string url);
    }
}
