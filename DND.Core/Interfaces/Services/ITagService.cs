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
    public interface ITagService : IBaseEntityService<TagDTO>
    {
        Task<TagDTO> GetTagAsync(string tagSlug, CancellationToken cancellationToken);
    }
}
