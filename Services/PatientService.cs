using LoonyBin.DAL;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public class PatientService(PatientDbContext dbContext): IPatientService
    {
        public async Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id, ct);

            return patient != null ? patient : new NotFound();
        }

        public async Task<OneOf<Patient, Error>> CreateAsync(Patient patient,
            CancellationToken ct = default)
        {
            if (patient.Id != default)
            {
                if (dbContext.Patients.Any(p => p.Id == patient.Id))
                    return new Error();
            }
            else
            {
                patient.Id = Guid.NewGuid();
            }
            
            await dbContext.Patients.AddAsync(patient, ct);
            await dbContext.SaveChangesAsync(ct);

            return patient;
        }

        public async Task<OneOf<Patient, NotFound>> UpdateAsync(Guid id, Patient patient,
            CancellationToken ct = default)
        {
            if (!dbContext.Patients.Any(p => p.Id == id))
                return new NotFound();

            dbContext.Patients.Update(patient);
            await dbContext.SaveChangesAsync(ct);

            return patient;
        }

        public async Task<OneOf<Success, NotFound>> DeleteAsync(Guid id,
            CancellationToken ct = default)
        {
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id, ct);
            if (patient == null)
                return new NotFound();

            dbContext.Patients.Remove(patient);
            await dbContext.SaveChangesAsync(ct);

            return new Success();
        }
    }
}
