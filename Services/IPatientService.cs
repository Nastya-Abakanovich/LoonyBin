using LoonyBin.DAL;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public interface IPatientService
    {
        public Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid id,
            CancellationToken ct = default);

        public Task<OneOf<Patient, Error>> CreateAsync(Patient patient,
            CancellationToken ct = default);

        public Task<OneOf<Patient, NotFound>> UpdateAsync(Patient patient,
            CancellationToken ct = default);

        public Task<OneOf<Success, NotFound>> DeleteAsync(Guid id,
            CancellationToken ct = default);
    }
}
