

namespace DSLNG.PEAR.Services.Requests.MirConfiguration
{
    public class GetMirConfigurationsRequest : GridBaseRequest
    {
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool OnlyCount { get; set; }
    }
}
