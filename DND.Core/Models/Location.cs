using DND.Core.Enums;
using Solution.Base.Extensions;
using Solution.Base.Implementation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;

namespace DND.Core.Models
{
	public class Location : BaseEntityAuditable<int>
    {
		public string Name { get; set; }

        [Column("LocationType")]
        public string LocationTypeString
        {
            get { return LocationType.ToString(); }
            private set { LocationType = EnumExtensions.ParseEnum<LocationType>(value); }
        }

        [NotMapped]
        public LocationType LocationType = LocationType.City;
        public string DescriptionBody { get; set; }
        public string TimeRequired { get; set; }
        public string Cost { get; set; }
        public string LinkText { get; set; }
        public string Link { get; set; }
        public Boolean ShowOnTravelMap { get; set; }
        public Boolean CurrentLocation { get; set; }
        public Boolean NextLocation { get; set; }

        public string PlaceId { get; set; }
        public DbGeography GPSLocation { get; set; }

        public string Album { get; set; }

        public string UrlSlug { get; set; }

        //public string SkyscannerCode { get; set; }

        public Location()
		{
			
		}

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            return errors;
        }
    }
}