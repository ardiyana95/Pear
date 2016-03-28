using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels;

namespace DSLNG.PEAR.Web.Controllers
{
    public class ProcessBlueprintController : BaseController
    {
        //
        // GET: /ProcessBlueprint/
        public ActionResult Index()
        {
            return View(ProcessBlueprintControllerFileManagerSettings.ProcessBlueprintFileSystemProvider);
        }

        [ValidateInput(false)]
        public ActionResult FileManagerPartial()
        {
            return PartialView("_FileManagerPartial", ProcessBlueprintControllerFileManagerSettings.ProcessBlueprintFileSystemProvider);
        }

        [ValidateInput(false)]
        public ActionResult DataBindingPartial()
        {
            return PartialView("DataBindingPartial", ProcessBlueprintControllerFileManagerSettings.ProcessBlueprintFileSystemProvider);
        }
        public FileStreamResult FileManagerPartialDownload()
        {
            return FileManagerExtension.DownloadFiles("FileManager", ProcessBlueprintControllerFileManagerSettings.ProcessBlueprintFileSystemProvider);
        }
	}
    public class ProcessBlueprintControllerFileManagerSettings
    {
        static ProcessBlueprintFileSystemProvider processBlueprintFileSystemProvider;
        public static readonly object FileManagerFolder = "~/Content/FileManager";
        public static readonly object RootFolder = "~/Content/FileManager";
        public static readonly object ImagesRootFolder = "~/Content/images";
        public static readonly string[] AllowedFileExtensions = new string[] {
            ".jpg", ".jpeg", ".gif", ".rtf", ".txt", ".avi", ".png", ".mp3", ".xml", ".doc", ".pdf"
        };

        public static ProcessBlueprintFileSystemProvider ProcessBlueprintFileSystemProvider
        {
            get
            {
                if (processBlueprintFileSystemProvider == null)
                    processBlueprintFileSystemProvider = new ProcessBlueprintFileSystemProvider(string.Empty);
                return processBlueprintFileSystemProvider;
            }
        }
        static DevExpress.Web.FileManagerSettingsDataSource _dataSourceSettings;
        public static DevExpress.Web.FileManagerSettingsDataSource DataSourceSettings
        {
            get
            {
                if (_dataSourceSettings == null)
                {
                    _dataSourceSettings = new DevExpress.Web.FileManagerSettingsDataSource();
                    _dataSourceSettings.KeyFieldName = "FileID";
                    _dataSourceSettings.ParentKeyFieldName = "ParentID";
                    _dataSourceSettings.IsFolderFieldName = "IsFolder";
                    _dataSourceSettings.NameFieldName = "Name";
                    _dataSourceSettings.FileBinaryContentFieldName = "Data";
                    _dataSourceSettings.LastWriteTimeFieldName = "LastWriteTime";
                }
                return _dataSourceSettings;
            }
        }
        public static DevExpress.Web.Mvc.MVCxDataSourceFileSystemProvider Model
        {
            get
            {
                object dataModel = new object[0]; // Insert here your data model object
                return new DevExpress.Web.Mvc.MVCxDataSourceFileSystemProvider(dataModel, DataSourceSettings);
            }
        }

        public static DevExpress.Web.Mvc.FileManagerSettings CreateFileManagerDownloadSettings()
        {
            var settings = new DevExpress.Web.Mvc.FileManagerSettings();

            settings.SettingsEditing.AllowDownload = true;

            settings.Name = "FileManager";
            return settings;
        }
    }

}