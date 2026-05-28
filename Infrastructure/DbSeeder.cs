using LoonyBin.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoonyBin.Infrastructure
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(PatientDbContext dbContext, CancellationToken ct = default)
        {
            if (!await dbContext.Patients.AnyAsync(ct))
            {
                await dbContext.Patients.AddRangeAsync(
                    new Patient { Id = Guid.NewGuid(), FamilyName = "Abakanovich", GivenNames = ["Nastya", "Stasya"], Gender = Gender.Female, BirthDate = new DateTime(2003, 5, 8, 20, 40, 10)},
                    new Patient { Id = Guid.NewGuid(), FamilyName = "Smith", GivenNames = ["John", "Henry", "Bob"], Gender = Gender.Male, BirthDate = new DateTime(1993, 2, 10) },
                    new Patient { Id = Guid.NewGuid(), FamilyName = "Deleon", GivenNames = [], Gender = Gender.Other, BirthDate = new DateTime(2013, 12, 21) },
                    new Patient { Id = Guid.NewGuid(), FamilyName = "Summers", GivenNames = ["Deborah"], Gender = Gender.Female, BirthDate = new DateTime(2000, 3, 14, 5, 13, 15) },
                    new Patient { Id = Guid.NewGuid(), FamilyName = "Carson", GivenNames = [], Gender = Gender.Unknown, BirthDate = new DateTime(2006, 10, 1) },
                    new Patient { Id = Guid.NewGuid(), FamilyName = "Friedman", GivenNames = ["Skylar", "Emmy"], Gender = Gender.Other, BirthDate = new DateTime(1997, 7, 17) }
                );

                await dbContext.SaveChangesAsync(ct);
            }
        }
    }
}