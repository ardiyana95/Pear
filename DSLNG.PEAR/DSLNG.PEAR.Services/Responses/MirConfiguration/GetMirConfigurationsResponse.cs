using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.MirConfiguration
{
    public class GetMirConfigurationsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<MirDataTable> MirDataTables { get; set; }
        public bool IsActive { get; set; }

        public class MirDataTable
        {
            public int Id { get; set; }
            public IList<Kpi> Kpis { get; set; }
            public MirTableType Type { get; set; }

            public class Kpi
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }
    }
}
