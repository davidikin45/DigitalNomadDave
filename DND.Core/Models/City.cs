using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace FlashpackerFlights.Core.Model
{
    public class City : Place
    {
        public string CityCode { get; set; }

        public int CountryId { get; set; }
        public virtual Country Country { get; set; }


        public City()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext);
            return errors;
        }
    }
}