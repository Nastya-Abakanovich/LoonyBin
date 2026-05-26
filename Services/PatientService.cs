using LoonyBin.DAL;
using LoonyBin.DateFilters;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public class PatientService(PatientDbContext dbContext, 
        ILogger<PatientService> logger) : IPatientService
    {
        public async Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id, ct);

            if (patient == null)
            {
                logger.LogWarning("Patient {PatientId} not found", id);
                return new NotFound();
            }

            logger.LogInformation("Patient {PatientId} retrieved", id);
            return patient;
        }

        public async Task<Patient> CreateAsync(Patient patient,
            CancellationToken ct = default)
        {
            patient.Id = Guid.NewGuid();

            await dbContext.Patients.AddAsync(patient, ct);
            await dbContext.SaveChangesAsync(ct);

            logger.LogInformation("Patient {PatientId} successfully created", patient.Id);

            return patient;
        }

        public async Task<OneOf<Patient, NotFound>> UpdateAsync(Guid id, Patient patient,
            CancellationToken ct = default)
        {
            if (!dbContext.Patients.Any(p => p.Id == id))
            {
                logger.LogWarning("Attempt to update non-existing patient {PatientId}", id);
                return new NotFound();
            }

            dbContext.Patients.Update(patient);
            await dbContext.SaveChangesAsync(ct);

            logger.LogInformation("Patient {PatientId} successfully updated", id);

            return patient;
        }

        public async Task<OneOf<Success, NotFound>> DeleteAsync(Guid id,
            CancellationToken ct = default)
        {
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id, ct);
            if (patient == null)
            {
                logger.LogWarning("Attempt to delete non-existing patient {PatientId}", id);
                return new NotFound();
            }

            dbContext.Patients.Remove(patient);
            await dbContext.SaveChangesAsync(ct);

            logger.LogInformation("Patient {PatientId} successfully deleted", id);

            return new Success();
        }

        public async Task<List<Patient>> SearchByBirthDateAsync(List<DateFilter> filters,
            CancellationToken ct = default)
        {
            logger.LogInformation("Searching patients by birth date with filters: {Filters}",
                string.Join(", ", filters.Select(f => $"{f.Prefix}{f.Range}")));

            var query = dbContext.Patients.AsQueryable();

            foreach (var f in filters)
            {
                query = ApplyDateFilter(query, f);
            }

            return await query.ToListAsync(ct);
        }

        private IQueryable<Patient> ApplyDateFilter(IQueryable<Patient> query,
            DateFilter filter)
        {
            return filter.Prefix switch
            {
                FilterPrefixes.Eq => query.Where(p => p.BirthDate >= filter.Range.Start && p.BirthDate < filter.Range.End),
                FilterPrefixes.Ne => query.Where(p => p.BirthDate < filter.Range.Start || p.BirthDate >= filter.Range.End),
                FilterPrefixes.Lt => query.Where(p => p.BirthDate < filter.Range.Start),
                FilterPrefixes.Le => query.Where(p => p.BirthDate <= filter.Range.End),
                FilterPrefixes.Gt => query.Where(p => p.BirthDate > filter.Range.End),
                FilterPrefixes.Ge => query.Where(p => p.BirthDate >= filter.Range.Start),
                FilterPrefixes.Sa => query.Where(p => p.BirthDate > filter.Range.End),
                FilterPrefixes.Eb => query.Where(p => p.BirthDate < filter.Range.Start),
                FilterPrefixes.Ap => ApplyApparentlyFilter(query, filter),
                _ => query
            };
        }

        private IQueryable<Patient> ApplyApparentlyFilter(IQueryable<Patient> query, DateFilter filter)
        {
            var len = filter.Range.End - filter.Range.Start;
            var delta = TimeSpan.FromTicks((long)(len.Ticks * 0.1));

            return query.Where(p => p.BirthDate >= filter.Range.Start - delta 
                && p.BirthDate < filter.Range.End + delta);
        }
    }
}
