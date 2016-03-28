using DSLNG.PEAR.Data.Entities.Mir;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.MirConfiguration;
using DSLNG.PEAR.Services.Responses.MirConfiguration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class MirConfigurationService : BaseService, IMirConfigurationService
    {
        public MirConfigurationService(IDataContext dataContext) : base(dataContext) { }


        public GetsMirConfigurationsResponse Gets(GetMirConfigurationsRequest request)
        {
            int totalRecords;
            var data = SortData(request.Search, request.SortingDictionary, out totalRecords);
            if (request.Take != -1)
            {
                data = data.Skip(request.Skip).Take(request.Take);
            }
            return new GetsMirConfigurationsResponse
            {
                TotalRecords = totalRecords,
                MirConfigurations = data.ToList().MapTo<GetsMirConfigurationsResponse.MirConfiguration>()
            };

        }

        public IEnumerable<MirConfiguration> SortData(string search, IDictionary<string, SortOrder> sortingDictionary, out int TotalRecords)
        {
            var data = DataContext.MirConfigurations.Include(x => x.MirArtifacts).Include(x => x.MirDataTables).Include(x => x.MirHighlights).AsQueryable();
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(x => x.Title.Contains(search));
            }

            foreach (var sortOrder in sortingDictionary)
            {
                switch (sortOrder.Key)
                {
                    case "Title":
                        data = sortOrder.Value == SortOrder.Ascending
                            ? data.OrderBy(x => x.Title).ThenBy(x => x.IsActive)
                            : data.OrderByDescending(x => x.Title).ThenBy(x => x.IsActive);
                        break;
                }
            }


            TotalRecords = data.Count();
            return data;
        }


        public SaveMirConfigurationResponse Save(SaveMirConfigurationRequest request)
        {
            var mirConfiguration = request.MapTo<MirConfiguration>();
            if (request.Id == 0)
            {
                DataContext.MirConfigurations.Add(mirConfiguration);
            }
            else
            {
                mirConfiguration = DataContext.MirConfigurations
                    .Include(x => x.MirArtifacts)
                    .Include(x => x.MirDataTables)
                    .Include(x => x.MirHighlights)
                    .FirstOrDefault(x => x.Id == request.Id);

                request.MapPropertiesToInstance<MirConfiguration>(mirConfiguration);
            }
            DataContext.SaveChanges();

            return new SaveMirConfigurationResponse
            {
                IsSuccess = true,
                Message = "Mir Configuration has been save successfully"
            };
        }


        public GetMirConfigurationsResponse Get(int id)
        {
            return DataContext.MirConfigurations
                .Include(x => x.MirArtifacts)
                .Include(x => x.MirDataTables)
                .Include(x => x.MirHighlights)
                .Include(x => x.MirDataTables.Select(y => y.Kpis))
                .FirstOrDefault(x => x.Id == id).MapTo<GetMirConfigurationsResponse>();
        }
    }
}
