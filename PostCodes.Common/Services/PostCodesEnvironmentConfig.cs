using PostCodes.Common.Services.Interfaces;
using PostCodes.Common.Model;


namespace PostCodes.Common.Services
{
    public class PostCodesEnvironmentConfig : IPostCodesEnvironmentConfig
    {
        private readonly PostCodeURIBase _routeConfig;
        public PostCodesEnvironmentConfig(PostCodeURIBase routeConfig)
        {
            _routeConfig = routeConfig;
        }
        public string GetAutoCompleteRoute() => _routeConfig.Routes.AutoComplete;

        public string GetPostCodeLookupRoute() => _routeConfig.Routes.PostCodeLookup;

        public string GetPostCodesBaseURI() => _routeConfig.BaseURI;
    }
}
