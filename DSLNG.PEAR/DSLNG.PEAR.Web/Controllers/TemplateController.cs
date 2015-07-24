﻿
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Requests.Template;
using DSLNG.PEAR.Web.ViewModels.Template;
using DSLNG.PEAR.Common.Extensions;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class TemplateController : BaseController
    {
        private readonly IArtifactService _artifactService;
        private readonly ITemplateService _templateService;

        public TemplateController(IArtifactService artifactService, ITemplateService templateService) {
            _artifactService = artifactService;
            _templateService = templateService;
        }

        public ActionResult ArtifactList(string term)
        {
            var artifacts = _artifactService.GetArtifactsToSelect(new GetArtifactsToSelectRequest { Term = term }).Artifacts;
            return Json(new { results = artifacts }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult View(int id)
        { 
            var template = _templateService.GetTemplate(new GetTemplateRequest{Id = id});
            var viewModel = template.MapTo<TemplateViewModel>();
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TemplateViewModel viewModel)
        {
            _templateService.CreateTemplate(viewModel.MapTo<CreateTemplateRequest>());
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var template = _templateService.GetTemplate(new GetTemplateRequest {Id = id});
            var viewModel = template.MapTo<TemplateViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Update(TemplateViewModel viewModel)
        {
            try
            {
                var request = viewModel.MapTo<UpdateTemplateRequest>();
                var response = _templateService.UpdateTemplate(request);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Preview(TemplateViewModel viewModel)
        {
            return View(viewModel);
        }

        //
        // GET: /Template/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Template/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #region Dev Express Grid
        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridTemplateIndex");
            if (viewModel == null)
                viewModel = CreateGridViewModel();
            return BindingCore(viewModel);
        }

        PartialViewResult BindingCore(GridViewModel gridViewModel)
        {
            gridViewModel.ProcessCustomBinding(
                GetDataRowCount,
                GetData
            );
            return PartialView("_IndexGridPartial", gridViewModel);
        }

        static GridViewModel CreateGridViewModel()
        {
            var viewModel = new GridViewModel();
            viewModel.KeyFieldName = "Id";
            viewModel.Columns.Add("Name");
            viewModel.Columns.Add("Remark");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridTemplateIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _templateService.GetTemplates(new GetTemplatesRequest { OnlyCount = true }).Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _templateService.GetTemplates(new GetTemplatesRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Artifacts;
        }
        #endregion
    }
}
