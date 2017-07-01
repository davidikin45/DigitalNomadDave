using Solution.Base.Implementation.Model;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Solution.Base.Extensions;
using Solution.Base.Interfaces.Model;
using System.Linq;
using System.IO;
using System.Web;
using Solution.Base.Helpers;

namespace Solution.Base.ModelMetadata
{
    public class FolderDropdownAttribute : DropdownAttribute
    {
        public FolderDropdownAttribute(string folderId, Boolean nullable = false)
            : this(folderId, nameof(DirectoryInfo.Name), nameof(DirectoryInfo.LastWriteTime), Solution.Base.ModelMetadata.OrderByType.Descending, nullable)
        {

        }

        public FolderDropdownAttribute(string folderId, string valueProperty, string orderByProperty, string orderByType, Boolean nullable = false)
            : base(folderId, "", valueProperty, orderByProperty, orderByType, nullable)
        {
        }
    }

    public class FileDropdownAttribute : DropdownAttribute
    {
        public FileDropdownAttribute(string folderId, Boolean nullable = false)
            : this(folderId, nameof(DirectoryInfo.Name), nameof(DirectoryInfo.LastWriteTime), Solution.Base.ModelMetadata.OrderByType.Descending, nullable)
        {

        }

        public FileDropdownAttribute(string folderId, string valueProperty, string orderByProperty, string orderByType, Boolean nullable = false)
            : base("", folderId, valueProperty, orderByProperty, orderByType, nullable)
        {
        }
    }

    public class DropdownAttribute : Attribute, IMetadataAware
    {
        public Type ModelType { get; set; }
        public string KeyProperty { get; set; }
        public string ValueProperty { get; set; }
        public string OrderByProperty { get; set; }
        public string OrderByType { get; set; }

        public string FolderFolderId { get; set; }
        public string PhysicalFolderPath { get; set; }

        public string FileFolderId { get; set; }
        public string PhysicalFilePath { get; set; }

        public Boolean Nullable { get; set; }

        public DropdownAttribute(string folderFolderId, string fileFolderId, string valueProperty, string orderByProperty, string orderByType, Boolean nullable = false)
        {
            FolderFolderId = folderFolderId;
            FileFolderId = fileFolderId;

            ValueProperty = valueProperty;
            OrderByProperty = orderByProperty;
            OrderByType = orderByType;

            Nullable = nullable;
        }

        //typeof
        //nameof
        public DropdownAttribute(Type modelType, string valueProperty)
            :this(modelType, "Id", valueProperty, "Id", Solution.Base.ModelMetadata.OrderByType.Descending)
        {

        }

        public DropdownAttribute(Type modelType, string valueProperty, string orderByProperty, string orderByType)
           : this(modelType, "Id", valueProperty, orderByProperty, orderByType)
        {

        }

        //typeof
        //nameof
        public DropdownAttribute(Type modelType, string keyProperty, string valueProperty, string orderByProperty, string orderByType, Boolean nullable = false)
        {
            if (!modelType.GetInterfaces().Contains(typeof(IBaseEntity)))
            {
                throw new ApplicationException("modelType must implement IBaseEntity");
            }

            ModelType = modelType;
            KeyProperty = keyProperty;
            ValueProperty = valueProperty;

            OrderByProperty = orderByProperty;
            OrderByType = orderByType;
            Nullable = nullable;
        }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.DataTypeName = "ModelDropdown";

            metadata.AdditionalValues["DropdownModelType"] = ModelType;
            metadata.AdditionalValues["DropdownKeyProperty"] = KeyProperty;
            metadata.AdditionalValues["DropdownValueProperty"] = ValueProperty;

            metadata.AdditionalValues["DropdownOrderByProperty"] = OrderByProperty;
            metadata.AdditionalValues["DropdownOrderByType"] = OrderByType;

            if(!string.IsNullOrEmpty(FolderFolderId))
            {
                PhysicalFolderPath = HttpContext.Current.Server.GetFolderPhysicalPathById(FolderFolderId);
            }

            metadata.AdditionalValues["DropdownPhysicalFolderPath"] = PhysicalFolderPath;

            if (!string.IsNullOrEmpty(FileFolderId))
            {
                PhysicalFilePath = HttpContext.Current.Server.GetFolderPhysicalPathById(FileFolderId);
            }

            metadata.AdditionalValues["DropdownPhysicalFilePath"] = PhysicalFilePath;
            metadata.AdditionalValues["DropdownNullable"] = Nullable;
        }
    }

    public static class OrderByType
    {
        public const string Descending = "desc";
        public const string Ascending = "asc";
    }

}