using System;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;

namespace FlashpackerFlights.Core.ViewModels
{
	public class OpportunityViewModel : IMapFrom<Opportunity>
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime DateCreated { get; set; }

		public int CustomerId { get; set; }

		public string CustomerName { get; set; }
	}
}