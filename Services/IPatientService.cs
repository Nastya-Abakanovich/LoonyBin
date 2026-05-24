using LoonyBin.DAL;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public interface IPatientService
    {
        public Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid Id);

        public Task<OneOf<Patient, Error>> CreateAsync(Patient patient);
    }
}
