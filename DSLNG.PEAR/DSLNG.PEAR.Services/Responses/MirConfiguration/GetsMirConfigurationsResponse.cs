using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.MirConfiguration
{
    public class GetsMirConfigurationsResponse : BaseResponse
    {
        public IList<MirConfiguration> MirConfigurations { get; set; }
        public int Count { get; set; }
        public int TotalRecords { get; set; }
        public class MirConfiguration
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public bool IsActive { get; set; }
        }
    }
}
