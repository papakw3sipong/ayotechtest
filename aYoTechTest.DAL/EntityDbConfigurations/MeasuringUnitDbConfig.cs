using aYoTechTest.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aYoTechTest.DAL.EntityDbConfigurations
{
    public class MeasuringUnitDbConfig : IEntityTypeConfiguration<MeasuringUnit>
    {
        public void Configure(EntityTypeBuilder<MeasuringUnit> builder)
        {
            builder.HasKey(k => k.MeasuringUnitId);

            builder.Property(p => p.MetricUnitDesc).HasMaxLength(255).IsRequired().HasColumnType("varchar(255)");

            builder.HasMany(m => m.SourceSupportedConversion);
            builder.HasMany(m => m.TargetSupportedConversion);

            builder.Property(x => x.CreatedById).HasMaxLength(255).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("timestamp");

            builder.Property(x => x.AuthorisedById).HasMaxLength(255).IsRequired(false).HasColumnType("varchar(255)").HasDefaultValue(null);
            builder.Property(x => x.AuthorisedAt).IsRequired(false).HasColumnType("timestamp").HasDefaultValue(null);


            builder.Property(x => x.LastUpdatedById).HasMaxLength(255).IsRequired(false).HasColumnType("varchar(255)").HasDefaultValue(null);
            builder.Property(x => x.LastUpdatedAt).IsRequired(false).HasColumnType("timestamp").HasDefaultValue(null);

            builder.Property(x => x.DeletedById).HasMaxLength(255).IsRequired(false).HasColumnType("varchar(255)").HasDefaultValue(null);
            builder.Property(x => x.DeletedAt).IsRequired(false).HasColumnType("timestamp").HasDefaultValue(null);


            builder.HasIndex(i => i.CreatedById);
            builder.HasIndex(i => i.CreatedAt);
            builder.HasIndex(i => i.AuthorisedAt);
            builder.HasIndex(i => i.AuthorisedById);

            builder.HasIndex(i => i.LastUpdatedAt);
            builder.HasIndex(i => i.LastUpdatedById);
            builder.HasIndex(i => i.DeletedById);
            builder.HasIndex(i => i.DeletedAt);
        }
    }
}
