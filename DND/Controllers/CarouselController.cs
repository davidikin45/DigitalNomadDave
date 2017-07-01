using AutoMapper;
using DND.Core.Constants;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using DND.Core.ViewModels;
using Solution.Base.Controllers;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Interfaces.Repository;
using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DND.Controllers
{
    public class CarouselController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly ICarouselItemService _carouselItemService;
        private readonly IFileSystemRepositoryFactory FileSystemRepository;

        public CarouselController(IBlogService blogService, ICarouselItemService carouselItemService, IFileSystemRepositoryFactory fileSystemRepository, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            FileSystemRepository = fileSystemRepository;
            _blogService = blogService;
            _carouselItemService = carouselItemService;
        }

        [OutputCache(Duration= 86400, VaryByParam = "None", VaryByCustom = "CacheExpiryKey")]
        [ChildActionOnly]
        public PartialViewResult Carousel()
        {
            string orderColumn = nameof(CarouselItemDTO.DateCreated);
            string orderType = OrderByType.Descending;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            IEnumerable<BlogPostDTO> posts = null;
            IList<CarouselItemDTO> carouselItemsFinal = new List<CarouselItemDTO>();
            IEnumerable<CarouselItemDTO> carouselItems = null;

            IList<DirectoryInfo> albums = new List<DirectoryInfo>();
            IList<CarouselItemDTO> albumCarouselItems = new List<CarouselItemDTO>();

            var result = Task.Run(async () =>
            {
                var postsTask = _blogService.BlogPostService.GetPostsForCarouselAsync(0, 3, cts.Token);
                var carouselItemsTask = _carouselItemService.GetAsync(cts.Token, c => c.Published, LamdaHelper.GetOrderBy<CarouselItemDTO>(orderColumn, orderType),null,null);

                await TaskHelper.WhenAllOrException(cts, postsTask, carouselItemsTask);

                posts = postsTask.Result;
                carouselItems = carouselItemsTask.Result;

                return 0;
            }).Result;

            var repository = FileSystemRepository.CreateFolderRepositoryReadOnly(cts.Token, Server.GetFolderPhysicalPathById(Folders.Gallery));
            foreach (CarouselItemDTO item in carouselItems)
            {
                if (!string.IsNullOrEmpty(item.Album))
                {
                    var album = repository.GetByPath(item.Album);
                    if (album != null)
                    {
                        albums.Add(album);
                        albumCarouselItems.Add(item);
                    }
                }
                else
                {
                    carouselItemsFinal.Add(item);
                }
            }

            var carouselViewModel = new CarouselViewModel
            {
                Posts = posts.ToList(),

                Albums = albums.ToList(),
                AlbumCarouselItems = albumCarouselItems.ToList(),

                CarouselItems = carouselItemsFinal.ToList(),
                ItemCount = posts.Count() + albums.Count() + carouselItemsFinal.Count()
            };

            return PartialView("_Carousel", carouselViewModel);
        }

    }
}