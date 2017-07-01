using DND.Core.Model;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Implementation.Model;
using Solution.Base.Interfaces.Automapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Core.DTOs
{
   public class BlogPostLocationDTO : BaseDTO, IMapFrom<Location>, IMapTo<BlogPostLocation>
    {
        [Required]
        public int LocationId { get; set; }

        public int BlogPostId { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
