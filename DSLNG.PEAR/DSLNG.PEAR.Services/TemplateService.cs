﻿
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Template;
using DSLNG.PEAR.Services.Responses.Template;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Entities;
using System.Data.Entity;
using System.Linq;

namespace DSLNG.PEAR.Services
{
    public class TemplateService : BaseService, ITemplateService
    {
        public TemplateService(IDataContext dataContext)
            : base(dataContext)
        {

        }

        public CreateTemplateResponse CreateTemplate(CreateTemplateRequest request)
        {
            var template = request.MapTo<DashboardTemplate>();
            var index = 0;
            foreach (var row in request.LayoutRows)
            {
                var layoutRow = new LayoutRow();
                var colIndex = 0;
                layoutRow.Index = index;
                foreach (var col in row.LayoutColumns)
                {
                    var LayoutColumn = new LayoutColumn();
                    LayoutColumn.Index = colIndex;
                    LayoutColumn.Width = col.Width;
                    if (col.ArtifactId != 0)
                    {
                        if (DataContext.Artifacts.Local.Where(x => x.Id == col.ArtifactId).FirstOrDefault() == null)
                        {
                            var artifact = new Artifact { Id = col.ArtifactId, GraphicType = "Unchanged", GraphicName = "Unchanged", HeaderTitle = "Unchanged" };
                            //DataContext.Entry(artifact).State = EntityState.Unchanged;
                            DataContext.Artifacts.Attach(artifact);
                            LayoutColumn.Artifact = artifact;
                        }
                        else
                        {
                            LayoutColumn.Artifact = DataContext.Artifacts.Local.Where(x => x.Id == col.ArtifactId).FirstOrDefault();
                        }
                    }
                    layoutRow.LayoutColumns.Add(LayoutColumn);
                    colIndex++;
                }
                template.LayoutRows.Add(layoutRow);
                index++;
            }
            DataContext.DashboardTemplates.Add(template);
            DataContext.SaveChanges();
            return new CreateTemplateResponse();
        }

        public GetTemplatesResponse GetTemplates(GetTemplatesRequest request)
        {

            if (request.OnlyCount)
            {
                return new GetTemplatesResponse { Count = DataContext.DashboardTemplates.Count() };
            }
            else
            {
                return new GetTemplatesResponse
                {
                    Artifacts = DataContext.DashboardTemplates.OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take)
                                    .ToList().MapTo<GetTemplatesResponse.TemplateResponse>()
                };
            }
        }

        public GetTemplateResponse GetTemplate(GetTemplateRequest request)
        {
            return DataContext.DashboardTemplates.Include(x => x.LayoutRows)
                .Include(x => x.LayoutRows.Select(y => y.LayoutColumns))
                .Include(x => x.LayoutRows.Select(y => y.LayoutColumns.Select(z => z.Artifact)))
                .FirstOrDefault(x => x.Id == request.Id).MapTo<GetTemplateResponse>();
        }

        public UpdateTemplateResponse UpdateTemplate(UpdateTemplateRequest request)
        {
            var template = DataContext.DashboardTemplates
                .Include(x => x.LayoutRows)
                .Include(x => x.LayoutRows.Select(y => y.LayoutColumns))
                .Single(x => x.Id == request.Id);
            template.Name = request.Name;
            template.RefershTime = request.RefershTime;
            template.Remark = request.Remark;
            template.IsActive = request.IsActive;
            foreach (var row in template.LayoutRows.ToList())
            {
                foreach (var column in row.LayoutColumns.ToList())
                {
                    DataContext.LayoutColumns.Remove(column);
                }
                DataContext.LayoutRows.Remove(row);
            }

            var index = 0;
            foreach (var row in request.LayoutRows)
            {
                var layoutRow = new LayoutRow();
                var colIndex = 0;
                layoutRow.Index = index;
                foreach (var col in row.LayoutColumns)
                {
                    var layoutColumn = new LayoutColumn();
                    layoutColumn.Index = colIndex;
                    layoutColumn.Width = col.Width;
                    if (col.ArtifactId != 0)
                    {
                        if (DataContext.Artifacts.Local.FirstOrDefault(x => x.Id == col.ArtifactId) == null)
                        {
                            var artifact = new Artifact { Id = col.ArtifactId, GraphicType = "Unchanged", GraphicName = "Unchanged", HeaderTitle = "Unchanged" };
                            //DataContext.Entry(artifact).State = EntityState.Unchanged;
                            DataContext.Artifacts.Attach(artifact);
                            layoutColumn.Artifact = artifact;
                        }
                        else
                        {
                            layoutColumn.Artifact = DataContext.Artifacts.Local.FirstOrDefault(x => x.Id == col.ArtifactId);
                        }
                    }
                    layoutRow.LayoutColumns.Add(layoutColumn);
                    colIndex++;
                }
                template.LayoutRows.Add(layoutRow);
                index++;
            }
            DataContext.DashboardTemplates.Attach(template);
            DataContext.Entry(template).State = EntityState.Modified;
            DataContext.SaveChanges();
            
            return new UpdateTemplateResponse();
        }
    }
}
