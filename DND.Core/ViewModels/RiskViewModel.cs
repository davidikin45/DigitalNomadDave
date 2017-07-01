using System;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;

namespace FlashpackerFlights.Core.ViewModels
{
	public class RiskViewModel : IMapFrom<Risk>
	{
		public int CustomerId { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime DateCreated { get; set; }

		public string CustomerName { get; set; }
	}
}