using aYoTechTest.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aYoTechTest.DAL.EntityDbConfigurations
{
    public class SupportedConversionDbConfig : IEntityTypeConfiguration<SupportedConversion>
    {
        public void Configure(EntityTypeBuilder<SupportedConversion> builder)
        {
            builder.HasKey(k => k.SupportedConversionId);

            builder.Property(p => p.SourceUnitId).IsRequired();
            builder.Property(p => p.SourceUnitValue).IsRequired().HasDefaultValue(1);
            builder.Property(p => p.TargetUnitId).IsRequired();
            builder.Property(p => p.Multiplier).IsRequired().HasDefaultValue(0.0m);

            builder.HasOne(o => o.SourceMeasuringUnit).WithMany(m => m.SourceSupportedConversion).HasForeignKey(f => f.SourceUnitId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.TargetMeasuringUnit).WithMany(m => m.TargetSupportedConversion).HasForeignKey(f => f.TargetUnitId).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreatedById).HasMaxLength(255).IsRequired().HasColumnType("varchar(255)");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("timestamp");

            builder.Property(x => x.AuthorisedById).HasMaxLength(255).IsRequired(false).HasColumnType("varchar(255)").HasDefaultValue(null);
            builder.Property(x => x.AuthorisedAt).IsRequired(false).HasColumnType("timestamp").HasDefaultValue(null);


            builder.Property(x => x.LastUpdatedById).HasMaxLength(255).IsRequired(false).HasColumnType("varchar(255)").HasDefaultValue(null);
            builder.Property(x => x.LastUpdatedAt).IsRequired(false).HasColumnType("timestamp").HasDefaultValue(null);

            builder.Property(x => x.DeletedById).HasMaxLength(255).IsRequired(false).HasColumnType("varchar(255)").HasDefaultValue(null);
            builder.Property(x => x.DeletedAt).IsRequired(false).HasColumnType("timestamp").HasDefaultValue(null);


            builder.HasIndex(i => i.SourceUnitId).IsUnique(false);
            builder.HasIndex(i => i.SourceUnitValue).IsUnique(false);

            builder.HasIndex(i => i.TargetUnitId).IsUnique(false);


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
