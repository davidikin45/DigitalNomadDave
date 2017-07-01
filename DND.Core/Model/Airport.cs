using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace FlashpackerFlights.Core.Model
{
    [Table("Airport")]
    public class Airport : Place
    {
        public string IATACode { get; set; }

        public int CityId { get; set; }
        public virtual City City { get; set; }

		public Airport()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext);
            return errors;
        }
    }
}