using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlashpackerFlights.Core.Interfaces.Persistance;
using FlashpackerFlights.Core.Interfaces.Services;
using FlashpackerFlights.Core.Model;
using Solution.Base.Implementation.Services;
using Solution.Base.Interfaces.UnitOfWork;
using Solution.Base.Implementation.Validation;
using Solution.Base.Interfaces.Services;

namespace FlashpackerFlights.Services
{
    public class RiskService : BaseEntityService<IApplicationDbContext,Risk>, IRiskService
    {
        public RiskService(IBaseUnitOfWorkScopeFactory baseUnitOfWorkScopeFactory)
            : base(baseUnitOfWorkScopeFactory)
        {

        }

         //throw new ValidationErrors(new GeneralError("You have reach the limit of 3 workouts per month, you need premium or wait the next month"));  

    }
}
