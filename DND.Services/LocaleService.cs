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

using DND.Services.Skyscanner.Model;
using DND.Core.DTOs;
using DND.Services.SearchEngines;
using AutoMapper;
using System.Threading;

namespace DND.Services
{
    public class LocaleService : BaseBusinessService, ILocaleService
    {

        public LocaleService(IBaseUnitOfWorkScopeFactory baseUnitOfWorkScopeFactory, IMapper mapper)
            : base(baseUnitOfWorkScopeFactory, mapper)
        {
          
        }

        public async Task<LocaleDTO> GetAsync(string id, CancellationToken cancellationToken)
        {
            var marketSearchEngine = new LocaleMarketCurrencySearchEngine("skyscanner");

            var response = await marketSearchEngine.GetLocaleByIDAsync(id, cancellationToken);

            return Mapper.Map<Skyscanner.Model.Locale, LocaleDTO>(response);
        }

        public async Task<IEnumerable<LocaleDTO>> GetAllAsync(CancellationToken cancellationToken)
        {

            var marketSearchEngine = new LocaleMarketCurrencySearchEngine("skyscanner");

            var response = await marketSearchEngine.GetLocalesAsync(cancellationToken);

            var list = new List<LocaleDTO>();

            foreach (Skyscanner.Model.Locale l in response.Locales)
            {
                var DTO = Mapper.Map<Skyscanner.Model.Locale, LocaleDTO>(l);
                list.Add(DTO);
            }

            return list;
        }
    }
}
