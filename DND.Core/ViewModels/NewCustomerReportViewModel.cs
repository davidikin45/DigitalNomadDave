using System;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;

namespace FlashpackerFlights.Core.ViewModels
{
	public class NewCustomerReportViewModel : IMapFrom<Customer>
	{
		public string Name { get; set; }

		public string WorkEmail { get; set; }

		public DateTime DateCreated { get; set; }
	}
}