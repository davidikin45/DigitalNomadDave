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
    public interface IAuthorService : IBaseEntityService<AuthorDTO>
    {
        Task<AuthorDTO> GetAuthorAsync(string authorSlug, CancellationToken cancellationToken);
    }
}
