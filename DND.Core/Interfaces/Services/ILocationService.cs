using DND.Core.Interfaces.Persistance;

using DND.Core.DTOs;
using Solution.Base.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using DND.Core.Models;

namespace DND.Core.Interfaces.Services
{
    public interface ILocationService : IBaseEntityService<LocationDTO>
    {
        Task<LocationDTO> GetLocationAsync(string urlSlug, CancellationToken cancellationToken);
    }
}
