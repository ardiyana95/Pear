﻿using DevExpress.Web.Mvc;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Highlight;
using DSLNG.PEAR.Web.ViewModels.Highlight;
using System;
using System.Web.Mvc;
using PeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.Select;
using System.Linq;
using DSLNG.PEAR.Services.Requests.NLS;
using DSLNG.PEAR.Services.Requests.VesselSchedule;
using DSLNG.PEAR.Services.Requests.Weather;
using DSLNG.PEAR.Services.Requests.HighlightOrder;

namespace DSLNG.PEAR.Web.Controllers
{
    public class HighlightController : BaseController
    {
        private IHighlightService _highlightService;
        private ISelectService _selectService;
        private INLSService _nlsService;
        private IVesselScheduleService _vesselScheduleService;
        private IWeatherService _waetherService;
        private IHighlightOrderService _highlightOrderService;

        public HighlightController(IHighlightService highlightService,
            ISelectService selectService,
            INLSService nlsService,
            IVesselScheduleService vesselScheduleService,
            IWeatherService weatherService,
            IHighlightOrderService highlightOrderService)
        {
            _highlightService = highlightService;
            _selectService = selectService;
            _nlsService = nlsService;
            _vesselScheduleService = vesselScheduleService;
            _waetherService = weatherService;
            _highlightOrderService = highlightOrderService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridArtifactIndex");
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
            viewModel.Columns.Add("PeriodeType");
            viewModel.Columns.Add("Type");
            viewModel.Columns.Add("Title");
            viewModel.Columns.Add("Date");
            viewModel.Columns.Add("IsActive");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridHighlightIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _highlightService.GetHighlights(new GetHighlightsRequest { OnlyCount = true }).Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _highlightService.GetHighlights(new GetHighlightsRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Highlights;
        }

        //
        // GET: /Highlight/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Highlight/Create
        public ActionResult Create()
        {
            var viewModel = new HighlightViewModel();
            foreach (var name in Enum.GetNames(typeof(PeriodeType)))
            {
                if (!name.Equals("Hourly") && !name.Equals("Weekly"))
                {
                    viewModel.PeriodeTypes.Add(new SelectListItem { Text = name, Value = name });
                }
            }
            viewModel.Types = _selectService.GetSelect(new GetSelectRequest { Name = "highlight-types" }).Options
                .Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            viewModel.AlertConditions = _selectService.GetSelect(new GetSelectRequest { Name = "alert-conditions" }).Options
                .Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return View(viewModel);
        }

        //
        // POST: /Highlight/Create
        [HttpPost]
        public ActionResult Create(HighlightViewModel viewModel)
        {
            var req = viewModel.MapTo<SaveHighlightRequest>();
            _highlightService.SaveHighlight(req);
            return RedirectToAction("Index");
        }

        //
        // GET: /Highlight/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = _highlightService.GetHighlight(new GetHighlightRequest { Id = id }).MapTo<HighlightViewModel>();
            foreach (var name in Enum.GetNames(typeof(PeriodeType)))
            {
                if (!name.Equals("Hourly") && !name.Equals("Weekly"))
                {
                    viewModel.PeriodeTypes.Add(new SelectListItem { Text = name, Value = name });
                }
            }
            viewModel.Types = _selectService.GetSelect(new GetSelectRequest { Name = "highlight-types" }).Options
                .Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            viewModel.AlertConditions = _selectService.GetSelect(new GetSelectRequest { Name = "alert-conditions" }).Options
               .Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return View(viewModel);
        }

        //
        // POST: /Highlight/Edit/5
        [HttpPost]
        public ActionResult Edit(HighlightViewModel viewModel)
        {
            var req = viewModel.MapTo<SaveHighlightRequest>();
            _highlightService.SaveHighlight(req);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _highlightService.DeleteHighlight(new DeleteRequest { Id = id });
            return RedirectToAction("Index");
        }

        public ActionResult DailyExecutionReport() {
            //var nlsList = _nlsService.GetNLSList(new GetNLSListRequest { TheActiveOnes = true });
            var vesselSchedules = _vesselScheduleService.GetVesselSchedules(new GetVesselSchedulesRequest{allActiveList=true});
            var viewModel = new DailyExecutionReportViewModel();
            viewModel.NLSList = vesselSchedules.VesselSchedules.MapTo<DailyExecutionReportViewModel.NLSViewModel>();
            viewModel.Weather = _waetherService.GetWeather(new GetWeatherRequest { Date = DateTime.Now.Date }).MapTo<DailyExecutionReportViewModel.WeatherViewModel>();
            viewModel.Highlights = _highlightService.GetHighlights(new GetHighlightsRequest { Except = new string[1] { "alert" },Date = DateTime.Now.Date, IsActive=true }).Highlights.MapTo<DailyExecutionReportViewModel.HighlightViewModel>();
            viewModel.Alert = _highlightService.GetHighlight(new GetHighlightRequest { Type = "alert", Date = DateTime.Now.Date }).MapTo<DailyExecutionReportViewModel.AlertViewModel>();
            var highlightOrders = _highlightOrderService.GetHighlights(new GetHighlightOrdersRequest());
            foreach (var highlight in highlightOrders.HighlightOrders) {
                var highlightVM = viewModel.Highlights.FirstOrDefault(x => x.Type == highlight.Value);
                if (highlightVM != null) {
                    highlightVM.Order = highlight.Order;
                }
            }
            viewModel.Highlights = viewModel.Highlights.OrderBy(x => x.Order).ToList();
            return View(viewModel);
        }
    }
}
