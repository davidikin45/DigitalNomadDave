using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;

namespace FlashpackerFlights.Core.ViewModels
{
	public class CustomerOpportunityViewModel : IMapFrom<Opportunity>
	{
		public string Title { get; set; }

		public string Description { get; set; }
	}
}