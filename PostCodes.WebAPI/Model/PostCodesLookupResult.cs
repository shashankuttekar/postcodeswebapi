using System.Collections.Generic;
using Newtonsoft.Json;

namespace PostCodes.WebAPI.Data.Model
{
    public class PostCodesLookupResult
    {
        [JsonProperty("result")]
        public List<string> Result { get; set; }
    }
}
