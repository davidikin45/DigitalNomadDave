using DND.Core.Interfaces.Persistance;
using DND.Core.Model;
using DND.Core.DTOs;
using Solution.Base.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DND.Core.Interfaces.Services
{
    public interface IMarketService : IBaseBusinessService
    {
        Task<IEnumerable<MarketDTO>> GetByLocale(string locale, CancellationToken cancellationToken);
        Task<MarketDTO> GetAsync(string id, CancellationToken cancellationToken);
    }
}
