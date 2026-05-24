using Mapster;

namespace LoonyBin.DAL
{
    public class PatientRequest : IRegister
    {
        public Name Name { get; set; } = default!;

        public string? Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PatientRequest, Patient>()
                .Map(dest => dest.Id, src => src.Name.Id)
                .Map(dest => dest.FamilyName, src => src.Name.Family)
                .Map(dest => dest.GivenNames, src => src.Name.Given)
                .Map(dest => dest.Gender, src => ParseGender(src.Gender));
        }

        public static Gender ParseGender(string? value)
        {
            if (Enum.TryParse<Gender>(value, true, out var g))
                return g;

            return LoonyBin.Gender.Unknown;
        }

    }
}