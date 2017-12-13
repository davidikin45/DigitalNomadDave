using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Solution.Base.Controllers;
using DND.Core.ViewModels;
using DND.Core.Models;
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
using Solution.Base.Filters;
using System.Threading;

namespace DND.Controllers
{
    [RoutePrefix("videos")]
    public class VideoController : BaseController
	{
        private readonly IFileSystemRepositoryFactory _fileSystemRepositoryFactory;


        public VideoController(IMapper mapper, ILogFactory logFactory, IFileSystemRepositoryFactory fileSystemRepositoryFactory)
             : base(mapper, logFactory)
        {
            _fileSystemRepositoryFactory = fileSystemRepositoryFactory;
        }

        [NoAjaxRequest]
        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("")]
        public virtual async Task<ActionResult> Index(int page = 1, int pageSize = 10, string orderColumn = nameof(FileInfo.LastWriteTime), string orderType = OrderByType.Descending)
        {
            try
            {

                string physicalPath = Server.GetFolderPhysicalPathById(Folders.Videos);

                if (!System.IO.Directory.Exists(physicalPath))
                    return HandleReadException();

                var response = await GetVideosViewModel(physicalPath, page, pageSize, orderColumn, orderType);

                ViewBag.Album = new DirectoryInfo(Server.GetFolderPhysicalPathById(Folders.Videos));

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

        [AjaxRequest]
        [ActionName("Index")]
        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("")]
        public virtual async Task<ActionResult> IndexList(int page = 1, int pageSize = 10, string orderColumn = nameof(FileInfo.LastWriteTime), string orderType = OrderByType.Descending)
        {
            try
            {              
                string physicalPath = Server.GetFolderPhysicalPathById(Folders.Videos);

                if (!System.IO.Directory.Exists(physicalPath))
                    return HandleReadException();

                var response = await GetVideosViewModel(physicalPath, page, pageSize, orderColumn, orderType);

                return PartialView("_GalleryList",response);
            }
            catch (Exception ex)
            {
                return HandleReadException();
            }
        }

        private async Task<WebApiPagedResponseDTO<FileInfo>> GetVideosViewModel(string physicalPath, int page = 1, int pageSize = 40, string orderColumn = nameof(FileInfo.LastWriteTime), string orderType = OrderByType.Descending)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(ClientDisconnectedToken());

            var repository = _fileSystemRepositoryFactory.CreateFileRepository(cts.Token, physicalPath, true, "*.*", ".mp4", ".txt");
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

            return response;
        }

    }
}