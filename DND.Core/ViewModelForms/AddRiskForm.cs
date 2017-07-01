using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;

namespace FlashpackerFlights.Core.ViewModels
{
	public class AddRiskForm : IMapTo<Risk>
	{
		[HiddenInput]
		public int CustomerId { get; set; }

		[Required]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
	}
}