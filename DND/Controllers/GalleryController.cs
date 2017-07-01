using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using DND.Core.ViewModels;
using DND.Core.Model;
using Solution.Base.Interfaces.Persistance;

using DND.Core.Interfaces.Services;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Repository;
using System.IO;
using Solution.Base.Implementation.DTOs;
using System.Threading.Tasks;
using System;
using Solution.Base.Extensions;
using Solution.Base.ModelMetadata;
using DND.Core.Constants;
using System.Web.UI;

namespace DND.Controllers
{
    [RoutePrefix("gallery")]
    public class GalleryController : BaseController
	{
        private readonly IBlogService _blogService;
        private readonly IFileSystemRepositoryFactory _fileSystemRepositoryFactory;


        public GalleryController(IBlogService blogService, IMapper mapper, ILogFactory logFactory, IFileSystemRepositoryFactory fileSystemRepositoryFactory)
             : base(mapper, logFactory)
        {
            _blogService = blogService;
            _fileSystemRepositoryFactory = fileSystemRepositoryFactory;
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("")]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 20, string orderColumn = nameof(DirectoryInfo.LastWriteTime), string orderType = OrderByType.Descending)
		{
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);
           
            try
            {
                var repository = _fileSystemRepositoryFactory.CreateFolderRepository(cts.Token, Server.GetFolderPhysicalPathById(Folders.Gallery));
                var dataTask = repository.GetAllAsync(LamdaHelper.GetOrderByFunc<DirectoryInfo>(orderColumn, orderType), (page - 1) * pageSize, pageSize);
                var totalTask = repository.GetCountAsync(null);

                await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                var data = dataTask.Result;
                var total = totalTask.Result;

                var response = new WebApiPagedResponseDTO<DirectoryInfo>
                {
                    Page = page,
                    PageSize = pageSize,
                    Records = total,
                    Rows = data.ToList(),
                    OrderColumn = orderColumn,
                    OrderType = orderType
                };

                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.OrderColumn = orderColumn;
                ViewBag.OrderType = orderType;

                return View(response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        // GET: Default/Details/5
        [Route("{name}")]
        public virtual async Task<ActionResult> Gallery(string name, int page = 1, int pageSize = 40, string orderColumn = nameof(FileInfo.LastWriteTime), string orderType = OrderByType.Descending)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());
            try
            {
                if (name == "instagram")
                    return View("Instagram");

                name = name.ToLower().Replace("-", " ");
                string physicalPath = Server.GetFolderPhysicalPathById(Folders.Gallery) + name;

                if (!System.IO.Directory.Exists(physicalPath))
                    return HandleReadException();

                var repository = _fileSystemRepositoryFactory.CreateFileRepository(cts.Token, physicalPath, true, "*.*", ".jpg", ".jpeg", ".mp4",".txt");
                var dataTask = repository.GetAllAsync(LamdaHelper.GetOrderByFunc<FileInfo>(orderColumn, orderType), (page - 1) * pageSize, pageSize);
                var totalTask = repository.GetCountAsync(null);

                await TaskHelper.WhenAllOrException(cts, dataTask, totalTask);

                var data = dataTask.Result;
                var total = totalTask.Result;

                var response = new WebApiPagedResponseDTO<FileInfo>
                {
                    Page = page,
                    PageSize = pageSize,
                    Records = total,
                    Rows = data.ToList(),
                    OrderColumn = orderColumn,
                    OrderType = orderType
                };

                ViewBag.Album = new DirectoryInfo(Server.GetFolderPhysicalPathById(Folders.Gallery) + name);

                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.OrderColumn = orderColumn;
                ViewBag.OrderType = orderType;

                return View(response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }

        }

    }
}