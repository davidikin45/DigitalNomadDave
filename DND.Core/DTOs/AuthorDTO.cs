using DND.Core.Constants;
using DND.Core.Enums;
using DND.Core.Models;
using Solution.Base.Implementation.Models;
using Solution.Base.Interfaces.Automapper;
using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace DND.Core.DTOs
{
	public class AuthorDTO : BaseEntity<int> , IMapFrom<Author>, IMapTo<Author>
    {
        [Required]
		public string Name { get; set; }

        [StringLength(50)]
        public string UrlSlug { get; set; }

        public AuthorDTO()
		{

        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}