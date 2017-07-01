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
    public class TestimonialController : BaseController
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IFileSystemRepositoryFactory FileSystemRepository;

        public TestimonialController(ITestimonialService testimonialService, IFileSystemRepositoryFactory fileSystemRepository, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            FileSystemRepository = fileSystemRepository;
            _testimonialService = testimonialService;
        }

        [OutputCache(Duration= 86400, VaryByParam = "None", VaryByCustom = "CacheExpiryKey")]
        [ChildActionOnly]
        public PartialViewResult Testimonials()
        {
            string orderColumn = nameof(TestimonialDTO.DateCreated);
            string orderType = OrderByType.Descending;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            IEnumerable<TestimonialDTO> testimonials = null;

            var result = Task.Run(async () =>
            {
                var testimonialsTask = _testimonialService.GetAllAsync(cts.Token, LamdaHelper.GetOrderBy<TestimonialDTO>(orderColumn, orderType),null,null);

                await TaskHelper.WhenAllOrException(cts, testimonialsTask);

                testimonials = testimonialsTask.Result;
                return 0;
            }).Result;

            var viewModel = new TestimonialsViewModel
            {
                Testimonials = testimonials.ToList()
            };

            return PartialView("_Testimonials", viewModel);
        }
    }
}