using DND.Core.Models;
using Solution.Base.Implementation.DTOs;
using Solution.Base.Implementation.Models;
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
   public class BlogPostTagDTO : BaseDTO, IMapFrom<Tag>, IMapTo<BlogPostTag>
    {
        [Required]
        public int TagId { get; set; }

        public int BlogPostId { get; set; }

        public string Name { get; set; }
        public string UrlSlug { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }
}
