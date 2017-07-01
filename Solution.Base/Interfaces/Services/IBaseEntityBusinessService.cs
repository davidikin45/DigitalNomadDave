using Solution.Base.Interfaces.Model;
using Solution.Base.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution.Base.Interfaces.Services
{
    public interface IBaseEntityService<TDto> : IBaseEntityReadOnlyService<TDto>
          where TDto : class, IBaseEntity
    {
        TDto Create(TDto dto);

        Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken = default(CancellationToken));

        void Update(TDto dto);

        Task UpdateAsync(TDto dto, CancellationToken cancellationToken = default(CancellationToken));

        void Delete(object id);

        Task DeleteAsync(object id, CancellationToken cancellationToken = default(CancellationToken));

        void Delete(TDto dto);

        Task DeleteAsync(TDto dto, CancellationToken cancellationToken = default(CancellationToken));
    }
}
