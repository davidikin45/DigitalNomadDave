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

namespace Solution.Base.Implementation.Services
{
    public class MailingListService : BaseEntityService<IBaseDbContext, MailingList, MailingListDTO>, IMailingListService
    {
        public MailingListService(IBaseUnitOfWorkScopeFactory baseUnitOfWorkScopeFactory, IMapper mapper)
        : base(baseUnitOfWorkScopeFactory, mapper)
        {

        }
    }
}
