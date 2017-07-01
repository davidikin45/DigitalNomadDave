using Solution.Base.Implementation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using Solution.Base.Extensions;

namespace DND.Core.DTOs
{
    public class LocationRequestDTO : BaseEntity<string>
    {
        public string Locale { get; set; }
        public string[] Market { get; set; }
        public string Currency { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Market == null || Market.Count() == 0)
                yield return new ValidationResult("Markets Required", new string[] { this.Name(x => x.Market) });
            if (string.IsNullOrEmpty(Currency))
                yield return new ValidationResult("Currency Required", new string[] { this.Name(x => x.Currency) });
            if (string.IsNullOrEmpty(Locale))
                yield return new ValidationResult("Locale Required", new string[] { this.Name(x => x.Locale) });
            if (string.IsNullOrEmpty(Id))
                yield return new ValidationResult("Id Required", new string[] { this.Name(x => x.Id) });
        }
    }
}
