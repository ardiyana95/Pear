using DSLNG.PEAR.Data.Entities.Mir;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.MirDataTable;
using DSLNG.PEAR.Services.Responses.MirDataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services
{
    public class MirDataTableService : BaseService, IMirDataTableService
    {
        public MirDataTableService(IDataContext dataContext) : base(dataContext) { }


        //public DeleteKpiMirDataTableResponse DeleteKpi(DeleteKpiMirDataTableRequest request)
        //{
        //    var MirDataTable = new MirDataTable { Id = request.MirDataTableId };

        //}
    }
}
