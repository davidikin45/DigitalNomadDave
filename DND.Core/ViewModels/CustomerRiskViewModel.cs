using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;

namespace FlashpackerFlights.Core.ViewModels
{
	public class CustomerRiskViewModel : IMapFrom<Risk>
	{
		public string Title { get; set; }

		public string Description { get; set; }
	}
}