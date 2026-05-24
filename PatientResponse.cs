using Mapster;

namespace LoonyBin.DAL
{
    public class PatientResponse: IRegister
    {
        public Name Name { get; set; } = default!;

        public string Gender { get; set; } = default!;

        public DateTime BirthDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Patient, PatientResponse>()
                .Map(dest => dest.Name, src => new Name
                {
                    Id = src.Id,
                    Family = src.FamilyName,
                    Given = src.GivenNames
                })
                .Map(dest => dest.Gender, src => src.Gender.ToString().ToLower());
        }
    }
}