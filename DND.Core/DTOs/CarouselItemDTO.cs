using DND.Core.Constants;
using DND.Core.Enums;
using DND.Core.Models;
using Solution.Base.Implementation.Models;
using Solution.Base.Interfaces.Automapper;
using Solution.Base.ModelMetadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;

namespace DND.Core.DTOs
{
    public class CarouselItemDTO : BaseEntity<int>, IMapFrom<CarouselItem>, IMapTo<CarouselItem>
    {
        [Render(AllowSortForGrid = false)]
        [FolderDropdown(Folders.Gallery, true)]
        public string Album { get; set; }

        public string Title { get; set; }

        public string CarouselText
        { get; set; }

        [UIHint("String")]
        public string ButtonText { get; set; }
        public string Link { get; set; }

        [Render(AllowSortForGrid = false)]
        [FileDropdown(Folders.Carousel, true)]
        public string File { get; set; }

        [Render(ShowForEdit = true, ShowForCreate = false, ShowForGrid = true)]
        public DateTime DateCreated { get; set; }

        [Required]
        public bool OpenInNewWindow
        { get; set; }

        [Required]
        public bool Published { get; set; }

        public CarouselItemDTO()
        {

        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();


            if (string.IsNullOrEmpty(Album) && (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(ButtonText) || string.IsNullOrEmpty(Link) || string.IsNullOrEmpty(File)))
            {
                errors.Add(new ValidationResult("If an Album is not selected Title, Button Text, Link and File must be entered"));
            }

            if (!string.IsNullOrEmpty(Album) && !string.IsNullOrEmpty(File))
            {
                errors.Add(new ValidationResult("Please select an Album or File, but not both"));
            }

            return errors;
        }
    }
}