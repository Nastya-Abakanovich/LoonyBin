using LoonyBin.Features.DateFilters;
using LoonyBin.Infrastructure;
using LoonyBin.Infrastructure.Entities;
using LoonyBin.Services.DateFilters;
using LoonyBin.Services.Patients;
using Mapster;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Features.Patients
{
    public class PatientService(PatientDbContext dbContext,
        IDateSearchService dateSearchService, ILogger<PatientService> logger) : IPatientService
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

        public async Task<Patient> CreateAsync(PatientCreateUpdateDto patientDto,
            CancellationToken ct = default)
        {
            var patient = patientDto.Adapt<Patient>();
            patient.Id = Guid.NewGuid();

            await dbContext.Patients.AddAsync(patient, ct);
            await dbContext.SaveChangesAsync(ct);

            logger.LogInformation("Patient {PatientId} successfully created", patient.Id);

            return patient;
        }

        public async Task<OneOf<Patient, NotFound>> UpdateAsync(Guid id, PatientCreateUpdateDto patientDto,
            CancellationToken ct = default)
        {
            var existingPatient = await dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id, ct);
            if (existingPatient is null)
            {
                logger.LogWarning("Attempt to update non-existing patient {PatientId}", id);
                return new NotFound();
            }

            patientDto.Adapt(existingPatient);
            await dbContext.SaveChangesAsync(ct);

            logger.LogInformation("Patient {PatientId} successfully updated", id);

            return existingPatient;
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
                query = dateSearchService.ApplyDateFilter(query, f);
            }

            return await query.ToListAsync(ct);
        }
    }
}
