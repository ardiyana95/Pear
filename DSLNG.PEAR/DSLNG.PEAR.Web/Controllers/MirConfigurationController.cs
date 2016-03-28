using DevExpress.Web.Mvc;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.MirConfiguration;
using DSLNG.PEAR.Services.Responses.MirConfiguration;
using DSLNG.PEAR.Web.Grid;
using DSLNG.PEAR.Web.ViewModels.MirConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MirConfigurationController : BaseController
    {
        public IMirConfigurationService _mirConfigurationService;
        public IDropdownService _dropdownService;
        public MirConfigurationController(IMirConfigurationService mirConfigurationService, IDropdownService dropdownService)
        {
            _mirConfigurationService = mirConfigurationService;
            _dropdownService = dropdownService;
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridMirConfigurationIndex");
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

        private static GridViewModel CreateGridViewModel()
        {
            var viewModel = new GridViewModel();
            viewModel.KeyFieldName = "Id";
            viewModel.Columns.Add("Name");
            viewModel.Columns.Add("Desc");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
            return viewModel;

        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {
            e.DataRowCount = _mirConfigurationService.Gets(new GetMirConfigurationsRequest { OnlyCount = true }).Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _mirConfigurationService.Gets(new GetMirConfigurationsRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).MirConfigurations;
        }


        public ActionResult Grid(GridParams gridParams)
        {
            var mirConfiguration = _mirConfigurationService.Gets(new GetMirConfigurationsRequest
                {
                    Skip = gridParams.DisplayStart,
                    Take = gridParams.DisplayLength,
                    SortingDictionary = gridParams.SortingDictionary,
                    Search = gridParams.Search
                });

            IList<GetsMirConfigurationsResponse.MirConfiguration> mirConfigurationResponse = mirConfiguration.MirConfigurations;
            var data = new
            {
                sEcho = gridParams.Echo + 1,
                iTotalDisplayRecords = mirConfiguration.TotalRecords,
                iTotalRecords = mirConfiguration.MirConfigurations.Count,
                aaData = mirConfigurationResponse
            };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            var viewModel = new SaveMirConfigurationViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(SaveMirConfigurationViewModel viewModel)
        {
            var request = viewModel.MapTo<SaveMirConfigurationRequest>();
            var response = _mirConfigurationService.Save(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View("Create", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var viewModel = _mirConfigurationService.Get(id).MapTo<SaveMirConfigurationViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(SaveMirConfigurationViewModel viewModel)
        {
            var request = viewModel.MapTo<SaveMirConfigurationRequest>();
            var response = _mirConfigurationService.Save(request);
            TempData["IsSuccess"] = response.IsSuccess;
            TempData["Message"] = response.Message;
            if (response.IsSuccess)
            {
                return RedirectToAction("Index");
            }
            return View("Create", viewModel);
        }

        public ActionResult Configure(int id)
        {
            var viewModel = _mirConfigurationService.Get(id).MapTo<ConfigureMirConfigurationViewModel>();
            var mirConfiguration = new ConfigureMirConfigurationViewModel.MirDataTable();
            mirConfiguration.KpiList = _dropdownService.GetKpis().MapTo<SelectListItem>();

            return View(viewModel);
        }
	}
}