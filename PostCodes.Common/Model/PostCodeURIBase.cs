
namespace PostCodes.Common.Model
{
    public class PostCodeURIBase
    {
        public string BaseURI { get; set; }
        public PostCodeURIRoutes Routes { get; set; }

    }

    public class PostCodeURIRoutes
    {
        public string AutoComplete { get; set; }
        public string PostCodeLookup { get; set; }

    }
}
