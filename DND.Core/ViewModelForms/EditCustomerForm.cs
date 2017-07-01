using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FlashpackerFlights.Core.Model;
using Solution.Base.Interfaces.Automapper;
using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;

namespace FlashpackerFlights.Core.ViewModels
{
	public class EditCustomerForm : BaseObjectValidatable, IMapTo<Customer>
	{
		[HiddenInput]
		public int Id { get; set; }

		[Required, Display(Name = "Full Name", Prompt = "Full Name (ex: John Doe)...")]
		public string Name { get; set; }

		[Required, DataType(DataType.EmailAddress)]
		public string WorkEmail { get; set; }

		[DataType(DataType.EmailAddress)]
		public string HomeEmail { get; set; }

		[Required, DataType(DataType.PhoneNumber)]
		public string WorkPhone { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string HomePhone { get; set; }

		[Required, DataType(DataType.MultilineText)]
		public string WorkAddress { get; set; }

		[DataType(DataType.MultilineText)]
		public string HomeAddress { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}