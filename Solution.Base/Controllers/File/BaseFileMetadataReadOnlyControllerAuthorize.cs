using Solution.Base.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solution.Base.Implementation.Extensions;
using Solution.Base.Interfaces.Validation;
using Solution.Base.Implementation.Validation;
using AutoMapper;
using System.Net;
using Solution.Base.Interfaces.Logging;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Http.Results;
using Solution.Base.Alerts;
using Solution.Base.Interfaces.Services;
using Solution.Base.Interfaces.Model;
using System.Web.Http.Description;
using Solution.Base.Helpers;
using Solution.Base.Implementation.DTOs;
using System.Web.Mvc;
using Solution.Base.Email;
using Solution.Base.Interfaces.Repository;
using System.IO;
using Solution.Base.Model;
using Solution.Base.ModelMetadata;

namespace Solution.Base.Controllers
{

    //Edit returns a view of the resource being edited, the Update updates the resource it self

    //C - Create - POST
    //R - Read - GET
    //U - Update - PUT
    //D - Delete - DELETE

    //If there is an attribute applied(via[HttpGet], [HttpPost], [HttpPut], [AcceptVerbs], etc), the action will accept the specified HTTP method(s).
    //If the name of the controller action starts the words "Get", "Post", "Put", "Delete", "Patch", "Options", or "Head", use the corresponding HTTP method.
    //Otherwise, the action supports the POST method.
    [System.Web.Mvc.Authorize(Roles = "admin")]
    public abstract class BaseFileMetadataReadOnlyControllerAuthorize : BaseController
    {   
        public IFileSystemRepositoryFactory FileSystemRepositoryFactory { get; private set; }
        public Boolean Admin { get; set; }
        public Boolean IncludeSubDirectories { get; set; }
        public String PhysicalPath { get; set; }

        public BaseFileMetadataReadOnlyControllerAuthorize(string physicalPath, Boolean includeSubDirectories, Boolean admin, IFileSystemRepositoryFactory fileSystemRepositoryFactory, IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
        : base(mapper,logFactory, emailService)
        {
            PhysicalPath = physicalPath;
            IncludeSubDirectories = includeSubDirectories;
            Admin = admin;
            FileSystemRepositoryFactory = fileSystemRepositoryFactory;
        }

        // GET: Default
        [Route("")]
        public virtual async Task<ActionResult> Index(int page = 1, int pageSize = 10, string orderColumn = nameof(FileInfo.LastWriteTime), string orderType = OrderByType.Descending, string search = "")
        {

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);
                  
            try
            {
                var repository = FileSystemRepositoryFactory.CreateFileRepositoryReadOnly(cts.Token, PhysicalPath, IncludeSubDirectories);
                var dataTask = repository.SearchAsync(search, null, LamdaHelper.GetOrderByFunc<FileInfo>(orderColumn, orderType), (page - 1) * pageSize, pageSize);
                var totalTask = repository.GetSearchCountAsync(search, null);

                await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                var data = dataTask.Result;
                var total = totalTask.Result;

                var rows = data.ToList().Select(Mapper.Map<FileInfo, FileMetadataDTO>).ToList();

                foreach (FileMetadataDTO dto in rows)
                {
                    dto.Id = dto.Id.Replace(PhysicalPath, "");
                }

                var response = new WebApiPagedResponseDTO<FileMetadataDTO>
                {
                    Page = page,
                    PageSize = pageSize,
                    Records = total,
                    Rows = rows,
                    OrderColumn = orderColumn,
                    OrderType = orderType,
                    Search = search
                };

                ViewBag.Search = search;
                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.OrderColumn = orderColumn;
                ViewBag.OrderType = orderType;

                ViewBag.DisableCreate = true;
                ViewBag.DisableSorting = true;
                ViewBag.DisableDelete = false;

                ViewBag.PageTitle = Title;
                ViewBag.Admin = Admin;
                return View("List", response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

        [ChildActionOnly]
        public virtual ActionResult ViewAllChild(int page = 1, int pageSize = 10, string orderColumn = nameof(FileInfo.LastWriteTime), string orderType = OrderByType.Descending)
        { 
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);
        
            try
            {
                IEnumerable<FileInfo> data = null;
                int total = 0;

                var result = Task.Run(async () => {
                    var repository = FileSystemRepositoryFactory.CreateFileRepositoryReadOnly(cts.Token, PhysicalPath, IncludeSubDirectories);
                    var dataTask = repository.GetAllAsync(LamdaHelper.GetOrderByFunc<FileInfo>(orderColumn, orderType), (page - 1) * pageSize, pageSize);
                    var totalTask = repository.GetCountAsync(null);

                    await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                    data = dataTask.Result;
                    total = totalTask.Result;

                    return true;
                }).Result;

                var rows = data.ToList().Select(Mapper.Map<FileInfo, FileMetadataDTO>).ToList();

                foreach(FileMetadataDTO dto in rows)
                {
                    dto.Id = dto.Id.Replace(PhysicalPath, "");
                }

                var response = new WebApiPagedResponseDTO<FileMetadataDTO>
                {
                    Page = page,
                    PageSize = pageSize,
                    Records = total,
                    Rows = rows,
                    OrderColumn = orderColumn,
                    OrderType = orderType
                };

                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.OrderColumn = orderColumn;
                ViewBag.OrderType = orderType;

                ViewBag.DisableCreate = true;
                ViewBag.DisableSorting = true;
                ViewBag.DisableDelete = false;

                return PartialView("_List", rows);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

        // GET: Default/Details/5
        [Route("details/{*id}")]
        public virtual async Task<ActionResult> Details(string id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            FileInfo data = null;
            try
            {
                var repository = FileSystemRepositoryFactory.CreateFileRepositoryReadOnly(cts.Token, PhysicalPath, IncludeSubDirectories);

                data = await repository.GetByPathAsync(id.Replace("/","\\"));

                if (data == null)
                    return HandleReadException();
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }

            ViewBag.PageTitle = Title;
            ViewBag.Admin = Admin;

            var dto = Mapper.Map<FileMetadataDTO>(data);
            dto.Id = dto.Id.Replace(PhysicalPath, "");

            return View("Details", dto);
        }

        [ChildActionOnly]
        public virtual ActionResult DetailsChild(string id)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            FileInfo data = null;
            try
            {
                var result = Task.Run(async () => {
                    var repository = FileSystemRepositoryFactory.CreateFileRepositoryReadOnly(cts.Token, PhysicalPath, IncludeSubDirectories);

                    data = await repository.GetByPathAsync(id.Replace("/", "\\"));
                    return true;
                }).Result;
                if (data == null)
                    return HandleReadException();
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }

            var dto = Mapper.Map<FileMetadataDTO>(data);
            dto.Id = dto.Id.Replace(PhysicalPath, "");

            return PartialView("_Details", dto);
        }

    }
}

