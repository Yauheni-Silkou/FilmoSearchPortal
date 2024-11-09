using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmoSearchPortal.DAL.Data;

public partial class ApplicationDbContext
{
    public class FilmActorConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasMany(f => f.Actors)
                   .WithMany(a => a.Films);
        }
    }
}