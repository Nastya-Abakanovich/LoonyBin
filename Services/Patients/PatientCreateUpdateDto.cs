using LoonyBin.Infrastructure.Entities;

namespace LoonyBin.Services.Patients
{
    public class PatientCreateUpdateDto
    {
        public string FamilyName { get; set; } = default!;

        public List<string> GivenNames { get; set; } = default!;

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
