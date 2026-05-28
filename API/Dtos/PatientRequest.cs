using LoonyBin.Infrastructure.Extensions;
using LoonyBin.Services.Patients;
using Mapster;

namespace LoonyBin.API.Dtos
{
    public class PatientRequest : IRegister
    {
        public NameRequestDto Name { get; set; } = default!;

        public string? Gender { get; set; }

        public DateTime BirthDate { get; set; }


        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PatientRequest, PatientCreateUpdateDto>()
                .Map(dest => dest.FamilyName, src => src.Name.Family)
                .Map(dest => dest.GivenNames, src => src.Name.Given ?? new())
                .Map(dest => dest.Gender, src => src.Gender.ToGender());
        }
    }

    public class NameRequestDto
    {
        public string Family { get; set; } = default!;

        public List<string>? Given { get; set; }
    }
}