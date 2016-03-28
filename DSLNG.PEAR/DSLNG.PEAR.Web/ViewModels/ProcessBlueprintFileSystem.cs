using DevExpress.Web;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Web.DependencyResolution;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels
{
    public static class ProcessBlueprintDataProvider
    {
        public static IProcessBlueprintServices service
        {
            get
            {
                return ObjectFactory.Container.GetInstance<IProcessBlueprintServices>();
            }
        }

        public static List<ProcessBlueprintFileItem> GetAll()
        {
            List<ProcessBlueprintFileItem> processBlueprints = (List<ProcessBlueprintFileItem>)HttpContext.Current.Session["ProcessBlueprint"];
            if (processBlueprints == null)
            {
                processBlueprints = service.GetAll().ProcessBlueprints.ToList().MapTo<ProcessBlueprintFileItem>();
                HttpContext.Current.Session["ProcessBlueprint"] = processBlueprints;
            }
            return processBlueprints;
        }
    }
    public class ProcessBlueprintFileItem
    {
        public int FileID { get; set; }

        public int? ParentID { get; set; }

        public string Name { get; set; }

        public bool IsFolder { get; set; }

        public byte[] Data { get; set; }

        public DateTime? LastWriteTime { get; set; }
    }

    public class ProcessBlueprintFileSystemProvider : FileSystemProviderBase
    {

        const int RootItemId = 1;
        string rootFolderDisplayName;
        Dictionary<int, ProcessBlueprintFileItem> folderChache;
        public ProcessBlueprintFileSystemProvider(string rootFolder)
            : base(rootFolder)
        {
            RefreshFolderCache();
        }

        public override string RootFolderDisplayName
        {
            get
            {
                return rootFolderDisplayName;
            }
        }

        public Dictionary<int, ProcessBlueprintFileItem> FolderChache { get { return folderChache; } }

        public override IEnumerable<FileManagerFile> GetFiles(FileManagerFolder folder)
        {
            ProcessBlueprintFileItem folderItem = FindFolderItem(folder);
            return from item in ProcessBlueprintDataProvider.GetAll()
                   where !item.IsFolder && item.ParentID == folderItem.FileID
                   select new FileManagerFile(this, folder, item.Name);
        }

        public override IEnumerable<FileManagerFolder> GetFolders(FileManagerFolder parentFolder)
        {
            ProcessBlueprintFileItem folderItem = FindFolderItem(parentFolder);
            return from item in FolderChache.Values
                   where item.IsFolder && folderItem.ParentID == item.FileID
                   select new FileManagerFolder(this, parentFolder, item.Name);
        }

        protected ProcessBlueprintFileItem FindFolderItem(FileManagerFolder folder)
        {
            return (from folderItem in FolderChache.Values
                    where folderItem.IsFolder && GetRelativeName(folderItem) == folder.RelativeName
                    select folderItem).FirstOrDefault();
        }

        protected ProcessBlueprintFileItem FindFileItem(FileManagerFile file)
        {
            ProcessBlueprintFileItem folderItem = FindFolderItem(file.Folder);
            if (folderItem == null) return null;
            return ProcessBlueprintDataProvider.GetAll().FindAll(x => (int)x.ParentID == folderItem.FileID && !x.IsFolder && x.Name == file.Name).FirstOrDefault();
        }
        public override bool Exists(FileManagerFile file)
        {
            return FindFileItem(file) != null;
        }

        public override bool Exists(FileManagerFolder folder)
        {
            return FindFolderItem(folder) != null;
        }

        public override Stream ReadFile(FileManagerFile file)
        {
            return new MemoryStream(FindFileItem(file).Data.ToArray());
        }

        public override DateTime GetLastWriteTime(FileManagerFile file)
        {
            var fileItem = FindFileItem(file);
            return fileItem.LastWriteTime.GetValueOrDefault(DateTime.Now);
        }

        public override long GetLength(FileManagerFile file)
        {
            var fileItem = FindFileItem(file);
            return fileItem.Data.Length;
        }

        public override void CreateFolder(FileManagerFolder parent, string name)
        {
            base.CreateFolder(parent, name);
        }

        public override void RenameFile(FileManagerFile file, string name)
        {
            base.RenameFile(file, name);
        }

        public override void RenameFolder(FileManagerFolder folder, string name)
        {
            base.RenameFolder(folder, name);
        }

        public override void MoveFile(FileManagerFile file, FileManagerFolder newParentFolder)
        {
            base.MoveFile(file, newParentFolder);
        }

        public override void MoveFolder(FileManagerFolder folder, FileManagerFolder newParentFolder)
        {
            base.MoveFolder(folder, newParentFolder);
        }

        public override void UploadFile(FileManagerFolder folder, string fileName, Stream content)
        {
            base.UploadFile(folder, fileName, content);
        }

        public override void DeleteFile(FileManagerFile file)
        {
            base.DeleteFile(file);
        }

        public override void DeleteFolder(FileManagerFolder folder)
        {
            base.DeleteFolder(folder);
        }

        protected string GetRelativeName(ProcessBlueprintFileItem folderItem)
        {
            if (folderItem.FileID == RootItemId) return string.Empty;
            if (folderItem.ParentID == RootItemId) return folderItem.Name;
            if (!FolderChache.ContainsKey((int)folderItem.ParentID)) return null;
            string name = GetRelativeName(FolderChache[(int)folderItem.ParentID]);
            return name == null ? null : Path.Combine(name, folderItem.Name);
        }
        protected void RefreshFolderCache()
        {
            this.folderChache = ProcessBlueprintDataProvider.GetAll().FindAll(x => x.IsFolder).ToDictionary(x => x.FileID);
            this.rootFolderDisplayName = (from folderItem in FolderChache.Values where folderItem.FileID == RootItemId select folderItem.Name).First();
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