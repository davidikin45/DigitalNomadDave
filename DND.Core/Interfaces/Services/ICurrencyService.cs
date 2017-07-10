using DND.Core.Interfaces.Persistance;
using DND.Core.Models;
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
    public interface ILocaleService : IBaseBusinessService
    {
        Task<IEnumerable<LocaleDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<LocaleDTO> GetAsync(string id, CancellationToken cancellationToken);
    }
}
