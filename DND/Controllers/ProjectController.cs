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
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IFileSystemRepositoryFactory FileSystemRepository;

        public ProjectController(IProjectService projectService, IFileSystemRepositoryFactory fileSystemRepository, IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
            FileSystemRepository = fileSystemRepository;
            _projectService = projectService;
        }

        [OutputCache(Duration= 86400, VaryByParam = "None", VaryByCustom = "CacheExpiryKey")]
        [ChildActionOnly]
        public PartialViewResult Projects()
        {
            string orderColumn = nameof(ProjectDTO.DateCreated);
            string orderType = OrderByType.Descending;

            var cts = TaskHelper.CreateChildCancellationTokenSource(HttpContext.Response.ClientDisconnectedToken);

            IEnumerable<ProjectDTO> projects = null;

            var result = Task.Run(async () =>
            {
                var projectsTask = _projectService.GetAllAsync(cts.Token, LamdaHelper.GetOrderBy<ProjectDTO>(orderColumn, orderType),null,null);

                await TaskHelper.WhenAllOrException(cts, projectsTask);

                projects = projectsTask.Result;
                return 0;
            }).Result;

            var viewModel = new ProjectsViewModel
            {
                Projects = projects.ToList()
            };

            return PartialView("_Projects", viewModel);
        }
    }
}