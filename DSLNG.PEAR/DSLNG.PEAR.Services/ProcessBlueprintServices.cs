using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Services.Responses.FileManager;
using DSLNG.PEAR.Data.Entities;
using System.Data.SqlClient;

namespace DSLNG.PEAR.Services
{
    public class ProcessBlueprintServices : BaseService, IProcessBlueprintServices
    {
        public ProcessBlueprintServices(IDataContext dataContext):base(dataContext)
        {
        }
        public GetFilesResponse GetAll()
        {
            var query = DataContext.ProcessBlueprints.AsQueryable();
            query.OrderBy(x => x.Id);
            return new GetFilesResponse
            {
                ProcessBlueprints = query.ToList().MapTo<GetFilesResponse.ProcessBlueprint>()
            };
        }

        public GetFileResponse GetFile(int Id)
        {
            throw new NotImplementedException();
        }

        protected IEnumerable<ProcessBlueprint> SortData(string search, IDictionary<string, SortOrder> sortingDictionary, out int TotalRecords)
        {
            var data = DataContext.ProcessBlueprints.AsQueryable();
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(x => x.Name.Contains(search));
            }

            foreach (var sortOrder in sortingDictionary)
            {
                switch (sortOrder.Key)
                {
                    case "Name":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Name)
                            : data.OrderByDescending(x => x.Name);
                        break;
                    case "LastWriteTime":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.LastWriteTime)
                            : data.OrderByDescending(x => x.LastWriteTime);
                        break;
                    case "IsFolder":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.IsFolder)
                            : data.OrderByDescending(x => x.IsFolder);
                        break;
                }
            }

            TotalRecords = data.Count();
            return data;
        }
    }
}
