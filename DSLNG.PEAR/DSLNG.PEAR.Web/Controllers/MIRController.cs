﻿using DevExpress.Web;
using DevExpress.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Files;
using DSLNG.PEAR.Services.Responses.Files;
using DSLNG.PEAR.Web.Attributes;
using DSLNG.PEAR.Web.Grid;
using DSLNG.PEAR.Web.ViewModels.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MIRController : BaseController
    {
        private readonly IFileRepositoryService _fileRepositoryService;
        private readonly IDropdownService _dropDownService;
        public MIRController(IFileRepositoryService fileRepostoryService, IDropdownService dropDownService)
        {
            _fileRepositoryService = fileRepostoryService;
            _dropDownService = dropDownService;
        }
        // GET: MIR
        public ActionResult Index()
        {
            ViewBag.Years = _dropDownService.GetYears().MapTo<SelectListItem>();
            ViewBag.Year = DateTime.Now.Year;
            return View();
        }

        public ActionResult Grid(GridParams gridParams, int? year)
        {
            year = year.HasValue ? year.Value : DateTime.Now.Year;
            var mirData = _fileRepositoryService.GetFiles(new Services.Requests.Files.GetFilesRequest
            {
                Year = year.Value,
                Take = gridParams.DisplayLength,
                Skip = gridParams.DisplayStart,
                SortingDictionary = gridParams.SortingDictionary,
                Search = gridParams.Search
            });
            List<FileRepositoryViewModel> DataResponse = mirData.FileRepositories.MapTo<FileRepositoryViewModel>();
            var data = new
            {
                sEcho = gridParams.Echo + 1,
                iTotalDisplayRecords = mirData.TotalRecords,
                iTotalRecords = mirData.FileRepositories.Count,
                aaData = DataResponse
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [AuthorizeUser(AccessLevel = "AllowCreate")]
        public ActionResult Create()
        {
            FileRepositoryCreateViewModel model = new FileRepositoryCreateViewModel
            {
                Year = DateTime.Now.Year,
                Month = DateTime.Now.Month,
                Years = _dropDownService.GetYears().MapTo<SelectListItem>(),
                Months = _dropDownService.GetMonths().MapTo<SelectListItem>()
            };
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FileRepositoryCreateViewModel model)
        {
            model.Years = _dropDownService.GetYears().MapTo<SelectListItem>();
            model.Months = _dropDownService.GetMonths().MapTo<SelectListItem>();

            if (ModelState.IsValid)
            {
                SaveFileRepositoryRequest saveModel = new SaveFileRepositoryRequest();
                saveModel = model.MapTo<SaveFileRepositoryRequest>();
                //this should be the reader
                //try to read data on buffer
                saveModel.Data = (byte[])Session[model.Filename];
                saveModel.LastWriteTime = DateTime.Now;
                saveModel.UserId = this.UserProfile().UserId;
                if (_fileRepositoryService.Save(saveModel).IsSuccess)
                {
                    return RedirectToAction("Index", "MIR");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            
        }

        public ActionResult UploadControlCallbackAction()
        {
            UploadControlExtension.GetUploadedFiles("mirUpload", MIRUploadControlSettings.ValidationSettings, MIRUploadControlSettings.FileUploadComplete);
            return null;
        }

        protected static byte[] ReadAllBytes(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];
            int readCount;
            using (MemoryStream ms = new MemoryStream())
            {
                while ((readCount = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, readCount);
                }
                return ms.ToArray();
            }
        }
    }
}