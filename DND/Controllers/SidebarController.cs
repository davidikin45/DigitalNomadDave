using AutoMapper;
using DND.Core.Constants;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using DND.Core.ViewModels;
using Solution.Base.Controllers;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Interfaces.Repository;
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
    public class SidebarController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IFileSystemRepositoryFactory FileSystemRepository;

        public SidebarController(IBlogService blogService, IFileSystemRepositoryFactory fileSystemRepository, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            FileSystemRepository = fileSystemRepository;
            _blogService = blogService;
        }

        [OutputCache(Duration = 86400, VaryByParam = "None", VaryByCustom ="CacheExpiryKey")]
        [ChildActionOnly]
        public PartialViewResult Sidebar()
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            IList<CategoryDTO> categories = null;
            IEnumerable<TagDTO> tags = null;
            IEnumerable<BlogPostDTO> posts = null;
            IEnumerable<FileInfo> photos = null;

            var result = Task.Run(async () =>
            {

                var repository = FileSystemRepository.CreateFileRepository(cts.Token, Server.GetFolderPhysicalPathById(Folders.Gallery), true, "*.*", ".jpg", ".jpeg");
              
                var categoriesTask = _blogService.CategoryService.GetAllAsync(cts.Token);
                var tagsTask = _blogService.TagService.GetAllAsync(cts.Token);
                var postsTask = _blogService.BlogPostService.GetPostsAsync(0, 10, cts.Token);
                var photosTask = repository.GetAllAsync(d => d.OrderByDescending(f => f.LastWriteTime), 0, 6);

                await TaskHelper.WhenAllOrException(cts, tagsTask, categoriesTask);

                List<Task<int>> countTasks = new List<Task<int>>();

                //foreach (TagDTO dto in tagsTask.Result)
                //{
                //    tagCountTasks.Add(_blogService.BlogPostService.GetTotalPostsForTagAsync(dto.UrlSlug, cts.Token).ContinueWith(t => dto.Count = t.Result));
                //}
                categories = categoriesTask.Result.ToList();
                foreach (CategoryDTO dto in categories)
                {
                    countTasks.Add(_blogService.BlogPostService.GetTotalPostsForCategoryAsync(dto.UrlSlug, cts.Token));
                }

                await TaskHelper.WhenAllOrException(cts, categoriesTask, postsTask, photosTask);
                await TaskHelper.WhenAllOrException(cts, countTasks.ToArray());

                int i = 0;
                foreach (CategoryDTO dto in categories)
                {
                    dto.Count = countTasks[i].Result;
                    i++;
                }

                tags = tagsTask.Result;
                posts = postsTask.Result;
                photos = photosTask.Result;

                return 0;
            }).Result;

            var widgetViewModel = new BlogWidgetViewModel
            {
                Categories = categories,
                Tags = tags.ToList(),
                LatestPosts = posts.ToList(),
                LatestPhotos = photos.ToList()
            };

            return PartialView("_Sidebar", widgetViewModel);
        }
    }
}