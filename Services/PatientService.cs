using LoonyBin.DAL;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public class PatientService(PatientDbContext dbContext): IPatientService
    {
        public async Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid id)
        {
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id);

            return patient != null ? patient : new NotFound();
        }

        public async Task<OneOf<Patient, Error>> CreateAsync(Patient patient)
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
            
            await dbContext.Patients.AddAsync(patient);
            await dbContext.SaveChangesAsync();

            return patient;
        }

        public async Task<OneOf<Patient, NotFound>> UpdateAsync(Patient patient)
        {
            if (!dbContext.Patients.Any(p => p.Id == patient.Id))
                return new NotFound();

            dbContext.Patients.Update(patient);
            await dbContext.SaveChangesAsync();

            return patient;
        }

        public async Task<OneOf<Success, NotFound>> DeleteAsync(Guid id)
        {
            var patient = await dbContext.Patients.SingleOrDefaultAsync(p => p.Id == id);
            if (patient == null)
                return new NotFound();

            dbContext.Patients.Remove(patient);
            await dbContext.SaveChangesAsync();

            return new Success();
        }
    }
}
