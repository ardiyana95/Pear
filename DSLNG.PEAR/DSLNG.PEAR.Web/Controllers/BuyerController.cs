﻿using DevExpress.Web.Mvc;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Buyer;
using DSLNG.PEAR.Web.ViewModels.Buyer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Web.Controllers
{
    public class BuyerController : BaseController
    {
        private IBuyerService _buyerService { get; set; }
        public BuyerController(IBuyerService buyerService) {
            _buyerService = buyerService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridBuyerIndex");
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
            viewModel.Columns.Add("Address");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridBuyerIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _buyerService.GetBuyers(new GetBuyersRequest { OnlyCount = true }).Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _buyerService.GetBuyers(new GetBuyersRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Buyers;
        }


        //
        // GET: /Buyer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Buyer/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Buyer/Create
        [HttpPost]
        public ActionResult Create(BuyerViewModel viewModel)
        {
            var req = viewModel.MapTo<SaveBuyerRequest>();
            _buyerService.SaveBuyer(req);
            return RedirectToAction("Index");
           
        }

        //
        // GET: /Buyer/Edit/5
        public ActionResult Edit(int id)
        {
            var viewModel = _buyerService.GetBuyer(new GetBuyerRequest { Id = id }).MapTo<BuyerViewModel>();
            return View(viewModel);
        }

        //
        // POST: /Buyer/Edit/5
        [HttpPost]
        public ActionResult Edit(BuyerViewModel viewModel)
        {
            var req = viewModel.MapTo<SaveBuyerRequest>();
            _buyerService.SaveBuyer(req);
            return RedirectToAction("Index");
        }

        //
        // GET: /Buyer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Buyer/Delete/5
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
