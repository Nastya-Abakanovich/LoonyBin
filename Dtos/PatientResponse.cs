using LoonyBin.DAL;
using Mapster;

namespace LoonyBin.Dtos
{
    public class PatientResponse: IRegister
    {
        public NameDto Name { get; set; } = default!;

        public string Gender { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientResponse>()
                .Map(dest => dest.Name, src => new NameDto
                {
                    Id = src.Id,
                    Family = src.FamilyName,
                    Given = src.GivenNames
                })
                .Map(dest => dest.Gender, src => src.Gender.ToString().ToLower());
        }
    }

    public class NameDto
    {
        public Guid Id { get; set; }

        public string Family { get; set; } = default!;

        public List<string>? Given { get; set; }
    }
}