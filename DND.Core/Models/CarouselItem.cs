using DND.Core.Enums;
using Solution.Base.Extensions;
using Solution.Base.Implementation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;

namespace DND.Core.Models
{
	public class CarouselItem : BaseEntityAuditable<int>
    {

		public string Title { get; set; }

        [StringLength(200)]
        public string CarouselText
        { get; set; }

        public string Album { get; set; }

        public string ButtonText { get; set; }
        public string Link { get; set; }
        public string Image { get; set; }

        [Required]
        public bool OpenInNewWindow
        { get; set; }

        [Required]
        public bool Published
        { get; set; }


        public CarouselItem()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}