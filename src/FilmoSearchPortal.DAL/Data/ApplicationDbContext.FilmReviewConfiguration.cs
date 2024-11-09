using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmoSearchPortal.DAL.Data;

public partial class ApplicationDbContext
{
    public class FilmReviewConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasMany(f => f.Reviews)
                   .WithOne(r => r.Film)
                   .HasForeignKey(r => r.FilmId);
        }
    }
}