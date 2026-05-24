using LoonyBin.DAL;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public interface IPatientService
    {
        public Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid id);

        public Task<OneOf<Patient, Error>> CreateAsync(Patient patient);

        public Task<OneOf<Patient, NotFound>> UpdateAsync(Patient patient);

        public Task<OneOf<Success, NotFound>> DeleteAsync(Guid id);
    }
}
