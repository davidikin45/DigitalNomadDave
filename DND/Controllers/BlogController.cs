using AutoMapper;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using DND.Core.ViewModels;
using Solution.Base.Controllers;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DND.Controllers
{
    [RoutePrefix("blog")] //Becomes controllername
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            _blogService = blogService;
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("")]
        public async Task<ViewResult> Posts(int page = 1)
        {
            int pageSize = 10;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            var postsTask = _blogService.BlogPostService.GetPostsAsync(page - 1, pageSize, cts.Token);
            var totalPostsTask = _blogService.BlogPostService.GetTotalPostsAsync(true, cts.Token);

            await TaskHelper.WhenAllOrException(cts, postsTask, totalPostsTask);

            var posts = postsTask.Result;
            var totalPosts = totalPostsTask.Result;

            var blogPostListViewModel = new BlogPostListViewModel
            {
                Page = page,
                PageSize = pageSize,
                Posts = posts.ToList(),
                TotalPosts = totalPosts
            };

            ViewBag.PageTitle = "Latest Posts";
            return View("PostList", blogPostListViewModel);
        }

        //[OutputCache(Duration = 86400, VaryByParam = "none")]
        [ChildActionOnly]
        public PartialViewResult LatestBlogPosts()
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            IEnumerable<BlogPostDTO> posts = null;

            var result = Task.Run(async () =>
            {

                var postsTask = _blogService.BlogPostService.GetPostsAsync(0, 3, cts.Token);

                await TaskHelper.WhenAllOrException(cts, postsTask);

                posts = postsTask.Result;

                return 0;
            }).Result;

            return PartialView("_LatestBlogPosts", posts.ToList());
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("archive/{year}/{month}/{title}")]
        public async Task<ViewResult> Post(int year, int month, string title)
        {
            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            var post = await _blogService.BlogPostService.GetPostAsync(year, month, title, cts.Token);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View("Post", post);
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("author/{authorSlug}")]
        public async Task<ViewResult> Author(string authorSlug, int page = 1)
        {
            int pageSize = 10;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            var postsTask = _blogService.BlogPostService.GetPostsForAuthorAsync(authorSlug, page - 1, pageSize, cts.Token);
            var totalPostsTask = _blogService.BlogPostService.GetTotalPostsForAuthorAsync(authorSlug, cts.Token);
            var authorTask = _blogService.AuthorService.GetAuthorAsync(authorSlug, cts.Token);

            await TaskHelper.WhenAllOrException(cts, postsTask, totalPostsTask, authorTask);

            var posts = postsTask.Result;
            var totalPosts = totalPostsTask.Result;
            var author = authorTask.Result;

            var blogPostListViewModel = new BlogPostListViewModel
            {
                Page = page,
                PageSize = pageSize,
                Posts = posts.ToList(),
                TotalPosts = totalPosts,
                Author = author
            };

            if (blogPostListViewModel.Author == null)
                throw new HttpException(404, "Author not found");

            ViewBag.PageTitle = String.Format(@"Latest posts for Author ""{0}""", blogPostListViewModel.Author.Name);

            return View("PostList", blogPostListViewModel);
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("category/{categorySlug}")]
        public async Task<ViewResult> Category(string categorySlug, int page = 1)
        {
            int pageSize = 10;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            var postsTask = _blogService.BlogPostService.GetPostsForCategoryAsync(categorySlug, page - 1, pageSize, cts.Token);
            var totalPostsTask = _blogService.BlogPostService.GetTotalPostsForCategoryAsync(categorySlug, cts.Token);
            var categoryTask = _blogService.CategoryService.GetCategoryAsync(categorySlug, cts.Token);

            await TaskHelper.WhenAllOrException(cts, postsTask, totalPostsTask, categoryTask);

            var posts = postsTask.Result;
            var totalPosts = totalPostsTask.Result;
            var category = categoryTask.Result;

            var blogPostListViewModel = new BlogPostListViewModel
            {
                Page = page,
                PageSize = pageSize,
                Posts = posts.ToList(),
                TotalPosts = totalPosts,
                Category = category
            };

            if (blogPostListViewModel.Category == null)
                throw new HttpException(404, "Category not found");

            ViewBag.PageTitle = String.Format(@"Latest posts on category ""{0}""", blogPostListViewModel.Category.Name);

            return View("PostList", blogPostListViewModel);
        }

        [OutputCache(CacheProfile = "Cache24HourParams")]
        [Route("tag/{tagSlug}")]
        public async Task<ViewResult> Tag(string tagSlug, int page = 1)
        {
            int pageSize = 10;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            var postsTask = _blogService.BlogPostService.GetPostsForTagAsync(tagSlug, page - 1, pageSize, cts.Token);
            var totalPostsTask = _blogService.BlogPostService.GetTotalPostsForTagAsync(tagSlug, cts.Token);
            var tagDTOTask = _blogService.TagService.GetTagAsync(tagSlug, cts.Token);

            await TaskHelper.WhenAllOrException(cts, postsTask, totalPostsTask, tagDTOTask);

            var posts = postsTask.Result;
            var totalPosts = totalPostsTask.Result;
            var tagDTO = tagDTOTask.Result;

            var blogPostListViewModel = new BlogPostListViewModel
            {
                Page = page,
                PageSize = pageSize,
                Posts = posts.ToList(),
                TotalPosts = totalPosts,
                Tag = tagDTO
            };

            if (blogPostListViewModel.Tag == null)
                throw new HttpException(404, "Tag not found");

            ViewBag.PageTitle = String.Format(@"Latest posts tagged on ""{0}""", blogPostListViewModel.Tag.Name);

            return View("PostList", blogPostListViewModel);
        }

        [Route("~/search")]
        public async Task<ViewResult> Search(string s, int page = 1)
        {
            int pageSize = 10;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            var postsTask = _blogService.BlogPostService.GetPostsForSearchAsync(s, page - 1, pageSize, cts.Token);
            var totalPostsTask = _blogService.BlogPostService.GetTotalPostsForSearchAsync(s, cts.Token);

            await TaskHelper.WhenAllOrException(cts, postsTask, totalPostsTask);

            var posts = postsTask.Result;
            var totalPosts = totalPostsTask.Result;

            var blogPostListViewModel = new BlogPostListViewModel
            {
                Page = page,
                PageSize = pageSize,
                Posts = posts.ToList(),
                TotalPosts = totalPosts,
                Search = s
            };

            ViewBag.PageTitle = String.Format(@"Lists of posts found for search text ""{0}""", s);

            return View("PostList", blogPostListViewModel);
        }

    }
}