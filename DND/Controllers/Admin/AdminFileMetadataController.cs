using AutoMapper;
using DND.Core.Interfaces.Services;
using DND.Core.Model;
using Solution.Base.Controllers;
using Solution.Base.Email;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Interfaces.Logging;
using Solution.Base.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Solution.Base.Helpers;
using Solution.Base.Interfaces.Repository;
using DND.Core.Constants;

namespace DND.Controllers.Admin
{
    [RoutePrefix("admin/file-metadata")]
    public class AdminFileMetadataController : BaseFileMetadataControllerAuthorize
    {
        public AdminFileMetadataController(IFileSystemRepositoryFactory fileSytemRepositoryFactory, IMapper mapper, ILogFactory logFactory, IEmailService emailService)
             : base(System.Web.HttpContext.Current.Server.GetFolderPhysicalPathById(Folders.Uploads), true, true, fileSytemRepositoryFactory, mapper, logFactory, emailService)
        {

        }
    }
}
