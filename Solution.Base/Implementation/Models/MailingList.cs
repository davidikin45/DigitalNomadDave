using Solution.Base.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Base.Implementation.Models
{
    public class MailingList : BaseEntityAuditable<int>
    {
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
