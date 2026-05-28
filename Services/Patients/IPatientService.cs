using LoonyBin.Services.DateFilters;
using LoonyBin.Infrastructure.Entities;
using OneOf;
using OneOf.Types;

namespace LoonyBin.Services.Patients
{
    public interface IPatientService
    {
        public Task<OneOf<Patient, NotFound>> GetByIdAsync(Guid id,
            CancellationToken ct = default);

        public Task<Patient> CreateAsync(PatientCreateUpdateDto patientDto,
            CancellationToken ct = default);

        public Task<OneOf<Patient, NotFound>> UpdateAsync(Guid id, PatientCreateUpdateDto patientDto,
            CancellationToken ct = default);

        public Task<OneOf<Success, NotFound>> DeleteAsync(Guid id,
            CancellationToken ct = default);

        public Task<List<Patient>> SearchByBirthDateAsync(List<DateFilter> filters, 
            CancellationToken ct = default);
    }
}
