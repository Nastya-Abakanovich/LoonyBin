using FluentValidation;
using LoonyBin.DAL;
using Mapster;

namespace LoonyBin.Dtos
{
    public class PatientUpdateRequest : IRegister
    {
        public NameUpdateDto Name { get; set; } = default!;

        public string? Gender { get; set; }

        public DateTime BirthDate { get; set; }


        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PatientUpdateRequest, Patient>()
                .Map(dest => dest.FamilyName, src => src.Name.Family)
                .Map(dest => dest.GivenNames, src => src.Name.Given)
                .Map(dest => dest.Gender, src => src.Gender.ToGender());
        }
    }

    public class NameUpdateDto
    {
        public string Family { get; set; } = default!;

        public List<string>? Given { get; set; }
    }

    public class PatientUpdateRequestValidator : AbstractValidator<PatientUpdateRequest>
    {
        public PatientUpdateRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .SetValidator(new NameUpdateDtoValidator());

            RuleFor(x => x.Gender)
                .IsEnumName(typeof(Gender), caseSensitive: false)
                .Unless(x => string.IsNullOrWhiteSpace(x.Gender))
                .WithMessage("Gender must be a valid value or empty.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Today).WithMessage("Birth date must be in the past");
        }
    }

    public class NameUpdateDtoValidator : AbstractValidator<NameUpdateDto>
    {
        public NameUpdateDtoValidator()
        {
            RuleFor(x => x.Family)
                .NotEmpty().WithMessage("Family name is required.");
        }
    }
}