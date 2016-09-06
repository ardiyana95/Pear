﻿using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Web.ViewModels.KpiTransformation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.KpiTransformation;
using DSLNG.PEAR.Web.Grid;

namespace DSLNG.PEAR.Web.Controllers
{
    public class KpiTransformationController : BaseController
    {
        private readonly IRoleGroupService _roleService;
        private readonly IKpiTransformationService _kpiTransformationService;

        public KpiTransformationController(IRoleGroupService roleService, IKpiTransformationService kpiTransformationService) {
            _roleService = roleService;
            _kpiTransformationService = kpiTransformationService;
        }
        // GET: KpiTransformation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid(GridParams gridParams)
        {
            var templates = _kpiTransformationService.Get(new GetKpiTransformationsRequest
            {
                Skip = gridParams.DisplayStart,
                Take = gridParams.DisplayLength,
                SortingDictionary = gridParams.SortingDictionary,
                Search = gridParams.Search
            });

            var data = new
            {
                sEcho = gridParams.Echo + 1,
                iTotalRecords = templates.KpiTransformations.Count,
                iTotalDisplayRecords = templates.TotalRecords,
                aaData = templates.KpiTransformations
            };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Create()
        {
            var viewModel = new KpiTransformationViewModel();
            viewModel.RoleGroupOptions = new MultiSelectList(_roleService.GetRoleGroups(new Services.Requests.RoleGroup.GetRoleGroupsRequest
            {
                Take = -1,
                SortingDictionary = new Dictionary<string, SortOrder> { { "Name", SortOrder.Ascending } }
            }).RoleGroups, "Id", "Name");

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(KpiTransformationViewModel viewModel) {
            return Save(viewModel);
        }

        public ActionResult Edit(int id) {
            var viewModel = _kpiTransformationService.Get(id).MapTo<KpiTransformationViewModel>();
            viewModel.RoleGroupOptions = new MultiSelectList(_roleService.GetRoleGroups(new Services.Requests.RoleGroup.GetRoleGroupsRequest
            {
                Take = -1,
                SortingDictionary = new Dictionary<string, SortOrder> { { "Name", SortOrder.Ascending } }
            }).RoleGroups, "Id", "Name", viewModel.RoleGroupIds);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(KpiTransformationViewModel viewModel)
        {
            return Save(viewModel);
        }

        private ActionResult Save(KpiTransformationViewModel viewModel) {
            var req = viewModel.MapTo<SaveKpiTransformationRequest>();
            var resp = _kpiTransformationService.Save(req);
            TempData["IsSuccess"] = resp.IsSuccess;
            TempData["Message"] = resp.Message;
            return RedirectToAction("Index");
        }
    }
}