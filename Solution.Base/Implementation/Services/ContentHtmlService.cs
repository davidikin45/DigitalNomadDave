using AutoMapper;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Implementation.Models;
using Solution.Base.Interfaces.Persistance;
using Solution.Base.Interfaces.Services;
using Solution.Base.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Solution.Base.Implementation.Validation;

namespace Solution.Base.Implementation.Services
{
    public class ContentHtmlService : BaseEntityService<IBaseDbContext, ContentHtml, ContentHtmlDTO>, IContentHtmlService
    {
        public ContentHtmlService(IBaseUnitOfWorkScopeFactory baseUnitOfWorkScopeFactory, IMapper mapper)
        : base(baseUnitOfWorkScopeFactory, mapper)
        {

        }

        public override Task DeleteAsync(ContentHtmlDTO dto, CancellationToken cancellationToken)
        {
            if(dto.PreventDelete)
            {
               throw new ServiceValidationErrors(new GeneralError("This CMS content cannot be deleted"));
            }

            return base.DeleteAsync(dto, cancellationToken);
        }
    }
}
