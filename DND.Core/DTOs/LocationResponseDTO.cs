using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Core.DTOs
{
   public class LocationResponseDTO
    {
        public string PlaceId { get; set; }
        public string PlaceName { get; set; }
        public string CountryId { get; set; }
        public string RegionId { get; set; }
        public string CityId { get; set; }
        public string CountryName { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //public DbGeography Location { get; set; }

    }
}
