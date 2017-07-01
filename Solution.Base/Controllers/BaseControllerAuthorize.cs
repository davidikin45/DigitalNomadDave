using Solution.Base.ActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Solution.Base.Implementation.Extensions;
using Solution.Base.Interfaces.Validation;
using Solution.Base.Implementation.Validation;
using AutoMapper;
using System.Net;
using Solution.Base.Interfaces.Logging;
using Microsoft.Web.Mvc;
using System.Linq.Expressions;
using System.Threading;
using Solution.Base.Alerts;
using Solution.Base.Email;
using Solution.Base.Authorization;

namespace Solution.Base.Controllers
{
    [MvcAuthorize(Roles = "admin")]
    public abstract class BaseControllerAuthorize : BaseController
    {
        public BaseControllerAuthorize()
        {

        }

        public BaseControllerAuthorize(IMapper mapper = null, ILogFactory logFactory = null, IEmailService emailService = null)
            :base(mapper, logFactory, emailService)
        {
         
        }
    }
}
