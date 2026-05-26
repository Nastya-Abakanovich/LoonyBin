using LoonyBin.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoonyBin.DAL
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
                .Property(b => b.FamilyName)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.GivenNames)
                .IsRequired()
                .HasConversion(
                    v => string.Join(";", v),
                    v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList()
                )
                .Metadata
                .SetValueComparer(
                    new ValueComparer<List<string>>(
                        (a, b) => a.SequenceEqual(b),
                        a => a.Aggregate(0, (hash, v) => HashCode.Combine(hash, v.GetHashCode())),
                        a => a.ToList()
                    )
                );

            builder
                .Property(b => b.BirthDate)
                .IsRequired();

            builder
                .Property(p => p.Gender)
                .HasConversion<string>()
                .HasMaxLength(10)
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
