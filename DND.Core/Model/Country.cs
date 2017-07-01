using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace FlashpackerFlights.Core.Model
{
    [Table("Airport")]
    public class Country : Place
    {
        public string CountryCode { get; set; }

        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        public Country()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext);
            return errors;
        }
    }
}