using AutoMapper;
using DND.Core.DTOs;
using Solution.Base.Interfaces.Automapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Services.Skyscanner.Model
{
    public class Locale : IHaveCustomMappings
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Locale, LocaleDTO>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Code))
             .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
        }
    }
}
