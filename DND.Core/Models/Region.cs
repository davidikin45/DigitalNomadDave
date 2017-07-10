using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace FlashpackerFlights.Core.Model
{
    [Table("Region")]
    public class Region : Place
    {
        public string RegionCode { get; set; }

        public Region()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = base.Validate(validationContext);
            return errors;
        }
    }
}