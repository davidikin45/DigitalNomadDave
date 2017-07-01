using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlashpackerFlights.Core.Model
{
	public class Opportunity : BaseEntityAuditable<int>
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

		public Opportunity()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}