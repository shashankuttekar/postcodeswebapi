
namespace PostCodes.WebAPI.Data.Model
{
    public class PostCodesResult
    {
        public PostCodesDetails result { get; set; }
    }
    
    public class PostCodesDetails
    {
        private double _latitude;
        public string country { get; set; }
        public string region { get; set; }
        public double? latitude { 
            get { return _latitude; } 
            set
            {
                if (value != null) { 
                
                    _latitude = (double)value;
                    if (_latitude < 52.229466)
                    {
                        area = "South";
                    }
                    else if (_latitude >= 52.229466 && _latitude < 53.27169)
                    {
                        area = "Midlands";
                    }
                    else if (_latitude >= 53.27169)
                    {
                        area = "North";
                    }
                }
            } 
        }

        public string area { get; set; }
        public Code codes { get; set; }

    }

    public class Code
    {
        public string admin_district { get; set; }
        public string parliamentary_constituency { get; set; }
    }
}
