﻿using DevExpress.Web.Mvc;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Buyer;
using DSLNG.PEAR.Services.Requests.Vessel;
using DSLNG.PEAR.Services.Requests.VesselSchedule;
using DSLNG.PEAR.Web.ViewModels;
using DSLNG.PEAR.Web.ViewModels.VesselSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class VesselScheduleController : Controller
    {
        private readonly IVesselScheduleService _vesselScheduleService;
        private readonly IVesselService _vesselService;
        private readonly IBuyerService _buyerService;
        public VesselScheduleController(IVesselScheduleService vesselScheduleService,
            IVesselService vesselService,
            IBuyerService buyerService) {
            _vesselScheduleService = vesselScheduleService;
            _vesselService = vesselService;
            _buyerService = buyerService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridVesselScheduleIndex");
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
            viewModel.Columns.Add("Vessel");
            viewModel.Columns.Add("ETA");
            viewModel.Columns.Add("ETD");
            viewModel.Columns.Add("Buyer");
            viewModel.Columns.Add("Location");
            viewModel.Columns.Add("SalesType");
            viewModel.Columns.Add("Type");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridVesselScheduleIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _vesselScheduleService.GetVesselSchedules(new GetVesselSchedulesRequest { OnlyCount = true }).Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _vesselScheduleService.GetVesselSchedules(new GetVesselSchedulesRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).VesselSchedules;
        }

        public ActionResult VesselList(string term)
        {
            var vessels = _vesselService.GetVessels(new GetVesselsRequest { Skip=0,Take=20, Term=term }).Vessels;
            return Json(new { results = vessels }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BuyerList(string term) {
            var buyers = _buyerService.GetBuyers(new GetBuyersRequest { Skip = 0, Take = 20, Term = term }).Buyers;
            return Json(new { results = buyers }, JsonRequestBehavior.AllowGet);
        }


        //
        // GET: /VesselSchedule/Create
        public ActionResult Create()
        {
            var viewModel = new VesselScheduleViewModel();
            return View(viewModel);
        }

        //
        // POST: /VesselSchedule/Create
        [HttpPost]
        public ActionResult Create(VesselScheduleViewModel viewModel)
        {
            var req = viewModel.MapTo<SaveVesselScheduleRequest>();
            _vesselScheduleService.SaveVesselSchedule(req);
            return RedirectToAction("Index");
        }

        //
        // GET: /VesselSchedule/Edit/5
        public ActionResult Edit(int id)
        {
            var vesselSchedule = _vesselScheduleService.GetVesselSchedule(new GetVesselScheduleRequest { Id = id });
            return View(vesselSchedule.MapTo<VesselScheduleViewModel>());
        }

        //
        // POST: /VesselSchedule/Edit/5
        [HttpPost]
        public ActionResult Edit(VesselScheduleViewModel viewModel)
        {
            var req = viewModel.MapTo<SaveVesselScheduleRequest>();
            _vesselScheduleService.SaveVesselSchedule(req);
            return RedirectToAction("Index");
        }

        //
        // GET: /VesselSchedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /VesselSchedule/Delete/5
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
    }
}
