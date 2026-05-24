using LoonyBin.DAL;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public class PatientService(PatientDbContext dbContext): IPatientService
    {
        public OneOf<Patient, NotFound> GetById(Guid Id)
        {
            var patient = dbContext.Patients.SingleOrDefault(p => p.Id == Id);

            return patient != null ? patient : new NotFound();
        }
    }
}
