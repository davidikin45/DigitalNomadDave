using AutoMapper;
using DND.Core.DTOs;
using DND.Core.Interfaces.Services;
using Solution.Base.Controllers;
using Solution.Base.Controllers.Admin;
using Solution.Base.Helpers;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Infrastructure;
using Solution.Base.Interfaces.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DND.Controllers.Admin
{
    //[LayoutInjector("_LayoutAdmin")]
    [RoutePrefix("admin")]
    public class AdminController : BaseAdminControllerAuthorize
    {

        public AdminController(IMapper mapper, ILogFactory logFactory)
             : base(mapper, logFactory)
        {
        }

    }
}