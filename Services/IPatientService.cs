using LoonyBin.DAL;
using LoonyBin.DateFilters;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services
{
    public interface IPatientService
    {
        public Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid id,
            CancellationToken ct = default);

        public Task<Patient> CreateAsync(Patient patient,
            CancellationToken ct = default);

        public Task<OneOf<Patient, NotFound>> UpdateAsync(Guid id, Patient patient,
            CancellationToken ct = default);

        public Task<OneOf<Success, NotFound>> DeleteAsync(Guid id,
            CancellationToken ct = default);

        public Task<List<Patient>> SearchByBirthDateAsync(List<DateFilter> filters, 
            CancellationToken ct = default);
    }
}
