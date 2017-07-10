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
    public class MarketService : BaseBusinessService, IMarketService
    {
        public MarketService(IBaseUnitOfWorkScopeFactory baseUnitOfWorkScopeFactory, IMapper mapper)
            : base(baseUnitOfWorkScopeFactory, mapper)
        {

        }

        public async Task<MarketDTO> GetAsync(string id, CancellationToken cancellationToken)
        {
            var marketSearchEngine = new LocaleMarketCurrencySearchEngine("skyscanner");

            var response = await marketSearchEngine.GetMarketByIDAsync(id, cancellationToken);

            var DTO = Mapper.Map<Skyscanner.Model.Country, MarketDTO>(response);
            await FormatDTO(DTO, cancellationToken);
            return DTO;
        }

        public async Task<IEnumerable<MarketDTO>> GetByLocale(string locale, CancellationToken cancellationToken)
        {
            if (locale == null)
                throw new ValidationErrors(new GeneralError("Invaid Request"));

            var marketSearchEngine = new LocaleMarketCurrencySearchEngine("skyscanner");

            var response = await marketSearchEngine.GetMarketsByLocaleAsync(locale, cancellationToken);

            var list = new List<MarketDTO>();

            foreach (Skyscanner.Model.Country c in response.Countries)
            {
                var DTO = Mapper.Map<Skyscanner.Model.Country, MarketDTO>(c);
                await FormatDTO(DTO, cancellationToken);
                list.Add(DTO);
            }

            return list;
        }

        private async Task FormatDTO(MarketDTO DTO, CancellationToken cancellationToken)
        {
            Task<CurrencyDTO> currency = null;

            if (!string.IsNullOrEmpty(DTO.CurrencyId))
            {
                var service = new CurrencyService(UnitOfWorkFactory, Mapper);
                currency = service.GetAsync(DTO.CurrencyId, cancellationToken);
            }

            if(currency != null)
            {
                DTO.Currency = await currency;
            }

        }
   
    }
}
