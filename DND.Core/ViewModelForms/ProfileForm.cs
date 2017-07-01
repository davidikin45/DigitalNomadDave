using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;
using Solution.Base.Implementation.Model;

namespace FlashpackerFlights.Core.ViewModels
{
	public class ProfileForm : IMapFrom<User>, IHaveCustomMappings
    {
		public string FullName { get; set; }

		public string EmailAddress { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, ProfileForm>()
                .ForMember(d => d.FullName, opt => opt.MapFrom(s => s.UserName))
                .ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.Email));
        }
    }
}