

namespace PostCodes.Common.Services.Interfaces
{
    public interface IPostCodesEnvironmentConfig
    {
        string GetPostCodesBaseURI();
        string GetAutoCompleteRoute();
        string GetPostCodeLookupRoute();
    }
}
