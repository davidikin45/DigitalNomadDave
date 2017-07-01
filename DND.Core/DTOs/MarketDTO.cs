using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND.Core.DTOs
{
   public class MarketDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LanguageId { get; set; }
        public string CurrencyId { get; set; }
        public CurrencyDTO Currency { get; set; }
    }
}
