using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlashpackerFlights.Core.Model
{
	public class Risk : BaseEntityAuditable<int>
    {

		public string Title { get; set; }

		public string Description { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

		public Risk()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}