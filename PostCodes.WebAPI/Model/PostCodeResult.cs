
using Newtonsoft.Json;

namespace PostCodes.WebAPI.Data.Model
{
    public class PostCodeResult
    {
        [JsonProperty("result")]
        public PostCodeDetails PostCodeDetails;
    }
    
    public class PostCodeDetails
    {
        private double _latitude;
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("latitude")]
        public double Latitude { 
            get { return _latitude; } 
            set
            {
                _latitude = value;
                if (_latitude < 52.229466)
                {
                    Area = "South";
                }
                else if (_latitude >= 52.229466 && _latitude < 53.27169)
                {
                    Area = "Midlands";
                }
                else if (_latitude >= 53.27169)
                {
                    Area = "North";
                }
            } 
        }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("codes")]
        public Code Codes { get; set; }


    }

    public class Code
    {
        [JsonProperty("admin_district")]
        public string AdminDistrict { get; set; }
        [JsonProperty("parliamentary_constituency")]
        public string ParliamentaryConstituency { get; set; }
    }
}
