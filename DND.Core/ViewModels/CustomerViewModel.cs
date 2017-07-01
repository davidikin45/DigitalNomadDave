using System;
using System.Collections.Generic;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;

namespace FlashpackerFlights.Core.ViewModels
{
	public class CustomerViewModel : IMapFrom<Customer>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string WorkEmail { get; set; }

		public string HomeEmail { get; set; }

		public string WorkPhone { get; set; }

        public int Rating { get; set; }

        public string HomePhone { get; set; }

		public string HomeAddress { get; set; }

		public string WorkAddress { get; set; }

		public DateTime? TerminationDate { get; set; }

		public IList<CustomerOpportunityViewModel> Opportunities { get; set; }

		public IList<CustomerRiskViewModel> Risks { get; set; }
	}
}