using System.Data.Entity.ModelConfiguration;
using FlashpackerFlights.Core.Model;
using Solution.Base.Implementation.Model;

namespace FlashpackerFlights.EFPersistance
{
    public class GigConfiguration : EntityTypeConfiguration<Risk>
    {
        public GigConfiguration()
        {
            //Property(g => g.ArtistId)
            //    .IsRequired();

            //Property(g => g.GenreId)
            //    .IsRequired();

            //Property(g => g.Venue)
            //    .IsRequired()
            //    .HasMaxLength(255);

            //HasMany(g => g.Attendances)
            //    .WithRequired(a => a.Gig)
            //    .WillCascadeOnDelete(false);
        }

    }
}
