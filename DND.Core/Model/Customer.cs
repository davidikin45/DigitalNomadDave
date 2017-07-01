using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FlashpackerFlights.Core.Model
{
	public class Customer : BaseEntityAuditable<int>
    {
		public string Name { get; set; }

        public int Age { get; set; }

        public string WorkEmail { get; set; }

		public string HomeEmail { get; set; }

		public string WorkPhone { get; set; }

        public string MobilePhone { get; set; }

        public int Rating { get; set; }

        public string HomePhone { get; set; }

		public string HomeAddress { get; set; }

		public string WorkAddress { get; set; }

		public DateTime? TerminationDate { get; set; }

		public IList<Opportunity> Opportunities { get; set; }

		public IList<Risk> Risks { get; set; }

		public Customer()
		{
	
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}