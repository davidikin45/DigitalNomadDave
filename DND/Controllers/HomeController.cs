using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Solution.Base.Alerts;

using DND.Core.Interfaces.Services;
using Solution.Base.Implementation.Validation;
using Solution.Base.Implementation.Extensions;
using Solution.Base.Controllers;
using Solution.Base.Filters;
using Solution.Base.Interfaces.Model;
using System.Threading;
using Solution.Base.Extensions;
using System.Threading.Tasks;
using System.ServiceModel.Syndication;
using DND.Core.DTOs;
using Solution.Base.Email;
using Solution.Base.Interfaces.Logging;
using AutoMapper;
using System.Web.UI;
using Solution.Base.Interfaces.Repository;
using Solution.Base.Helpers;
using DND.Core.Constants;
using System.IO;
using Solution.Base.ModelMetadata;

namespace DND.Controllers
{
    [RoutePrefix("")]
    //[LogAction]
    public class HomeController : BaseHomeController
    {
        private IBlogService _blogService;
        private ILocationService _locationService;
        private readonly IFileSystemRepositoryFactory _fileSystemRepositoryFactory;

        public HomeController(IBlogService blogService, ILocationService locationService, IFileSystemRepositoryFactory fileSystemRepositoryFactory, ICurrentUser currentUser, IMapper mapper, ILogFactory logFactory , IEmailService emailService)
            :base(mapper, logFactory, emailService)
        {
            if (blogService == null) throw new ArgumentNullException("blogService");
            _blogService = blogService;
            _locationService = locationService;
            _fileSystemRepositoryFactory = fileSystemRepositoryFactory;
        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("", Name ="home")] //Specifies that this is the default action for the entire application. Route: /
        public ActionResult Index()
        {
            //Validate ViewModel
            if (ModelState.IsValid)
            {
                try
                {

                }
                catch (Exception ex)
                {
                    return HandleReadException();
                }
            }
            return View();
        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("contact")]
        public override ActionResult Contact()
        {
            return base.Contact();
        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("work-with-me")]
        public ActionResult WorkWithMe()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("about")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("resume")]
        public ActionResult Resume()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [OutputCache(CacheProfile = "Cache24HourNoParams")]
        [Route("help-faq")]
        public ActionResult HelpFAQ()
        {
            ViewBag.Message = @"Your Help\FAQ page.";

            return View();
        }

        public ActionResult News(CancellationToken cancellationToken = default(CancellationToken))
        {
            ViewBag.Message = "Your news page.";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            ViewBag.Message = "Your privacy policy page.";

            return View();
        }

        public ActionResult TermsOfService()
        {
            ViewBag.Message = "Your terms of service page.";

            return View();
        }

        protected async override Task<IList<SitemapNode>> GetSitemapNodes(CancellationToken cancellationToken)
        {
            IList<SitemapNode> nodes = await base.GetSitemapNodes(cancellationToken);

            //Locations
            nodes.Add(
                 new SitemapNode()
                 {
                     Url = Url.AbsoluteUrl<LocationController>(c => c.Index(1, 20, nameof(LocationDTO.Name), OrderByType.Ascending,""), false),
                     Priority = 0.9
                 });

            foreach (TagDTO t in (await _blogService.TagService.GetAllAsync(cancellationToken, null, null, null)))
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = Url.AbsoluteUrl(nameof(BlogController.Tag), "Blog", new { tagSlug = t.UrlSlug }),
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
            }

            foreach (CategoryDTO c in (await _blogService.CategoryService.GetAllAsync(cancellationToken, null, null, null)))
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = Url.AbsoluteUrl(nameof(BlogController.Category), "Blog", new { categorySlug = c.UrlSlug }),
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.8
                   });
            }

            foreach (BlogPostDTO p in (await _blogService.BlogPostService.GetPostsAsync(0, 200, cancellationToken)))
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = Url.AbsoluteUrl<BlogController>(c => c.Post(p.DateCreated.Year, p.DateCreated.Month, p.UrlSlug)),
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.7
                   });
            }

            var repository = _fileSystemRepositoryFactory.CreateFolderRepository(cancellationToken, Server.GetFolderPhysicalPathById(Folders.Gallery));
            foreach (DirectoryInfo f in (await repository.GetAllAsync(LamdaHelper.GetOrderByFunc<DirectoryInfo>(nameof(DirectoryInfo.LastWriteTime), OrderByType.Descending), null, null)))
            {
                nodes.Add(
                   new SitemapNode()
                   {
                       Url = Url.AbsoluteUrl("Gallery", "Gallery", new { name = f.Name.ToSlug() }),
                       Frequency = SitemapFrequency.Weekly,
                       Priority = 0.7
                   });
            }

            foreach (LocationDTO l in (await _locationService.GetAllAsync(cancellationToken, null, null, null)))
            {
                if (!string.IsNullOrEmpty(l.UrlSlug))
                {
                    nodes.Add(
                       new SitemapNode()
                       {
                           Url = Url.AbsoluteUrl<LocationController>(lc => lc.Location(l.UrlSlug)),
                           Frequency = SitemapFrequency.Weekly,
                           Priority = 0.6
                       });
                }
            }

            return nodes;
        }

        protected async override Task<IEnumerable<System.ServiceModel.Syndication.SyndicationItem>> RSSItems(CancellationToken cancellationToken)
        {
            var posts = (await _blogService.BlogPostService.GetPostsAsync(0, 200, cancellationToken)).Select
            (
              p => new SyndicationItem
                  (
                      p.Title,
                      HtmlOutputHelper.RelativeToAbsoluteUrls(p.Description, System.Configuration.ConfigurationManager.AppSettings["SiteUrl"]),
                      new Uri(Url.AbsoluteUrl<BlogController>(c => c.Post(p.DateCreated.Year, p.DateCreated.Month, p.UrlSlug)))
                  )
            );

            return posts;
        }

        protected override string SiteTitle
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SiteTitle"];
            }
        }

        protected override string SiteDescription
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SiteDescription"];
            }

        }

        protected override string SiteUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SiteUrl"];
            }

        }
    }
}
