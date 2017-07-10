using DND.Core.Enums;
using Solution.Base.Extensions;
using Solution.Base.Implementation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;

namespace DND.Core.Models
{
	public class Author : BaseEntityAuditable<int>
    {
        [Required]
		public string Name { get; set; }

        [StringLength(50)]
        public string UrlSlug { get; set; }

        public Author()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}