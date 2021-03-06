using Solution.Base.Implementation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DND.Core.Models
{
	public class BlogPostLocation : BaseEntityAuditable<int>
    {
        [Required]
        public int BlogPostId { get; set; }
        public virtual BlogPost BlogPost { get; set; }

        [Required]
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public BlogPostLocation()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}