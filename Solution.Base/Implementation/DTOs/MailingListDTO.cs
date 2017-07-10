using Solution.Base.Implementation.Models;
using Solution.Base.Interfaces.Automapper;
using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Solution.Base.Implementation.DTOs
{
    public class MailingListDTO : BaseEntity<int>, IHaveCustomMappings
    {
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Render(ShowForEdit = false, ShowForGrid = true)]
        public DateTime DateCreated { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<MailingList, MailingListDTO>();

            configuration.CreateMap<MailingListDTO, MailingList>()
           .ForMember(bo => bo.DateCreated, dto => dto.Ignore());
        }

        public override IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            yield break;
        }
    }
}
