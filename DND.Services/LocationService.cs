using DND.Core.Interfaces.Services;
using DND.Core.Models;
using Solution.Base.Implementation.Services;
using Solution.Base.Implementation.Validation;
using Solution.Base.Interfaces.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Solution.Base.Extensions;
using AutoMapper;
using DND.Services.Skyscanner.Model;
using DND.Core.DTOs;
using DND.Services.SearchEngines;
using System.Threading;
using DND.Core.Interfaces.Persistance;
using System.Linq;
using System.Linq.Expressions;
using Solution.Base.Helpers;
using Solution.Base.Infrastructure;

namespace DND.Services
{
    public class LocationService : BaseEntityService<IApplicationDbContext, Location, LocationDTO>, ILocationService
    {
        public LocationService(IBaseUnitOfWorkScopeFactory baseUnitOfWorkScopeFactory, IMapper mapper)
        : base(baseUnitOfWorkScopeFactory, mapper)
        {

        }

        public override Task<LocationDTO> CreateAsync(LocationDTO dto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(dto.UrlSlug))
            {
                dto.UrlSlug = UrlSlugger.ToUrlSlug(dto.Name);
            }

            return base.CreateAsync(dto, cancellationToken);
        }

        public async Task<LocationDTO> GetLocationAsync(string urlSlug, CancellationToken cancellationToken)
        {
            using (var UoW = UnitOfWorkFactory.CreateReadOnly(BaseUnitOfWorkScopeOption.JoinExisting, cancellationToken))
            {
                var result = await UoW.Repository<IApplicationDbContext, Location>().GetFirstAsync(t => t.UrlSlug.Equals(urlSlug));
                return Mapper.Map<LocationDTO>(result);
            }
        }

        public override Task UpdateAsync(LocationDTO dto, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(dto.UrlSlug))
            {
                dto.UrlSlug = UrlSlugger.ToUrlSlug(dto.Name);
            }

            return base.UpdateAsync(dto, cancellationToken);
        }


    }
}