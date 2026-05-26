using FluentValidation;
using LoonyBin.Infrastructure.Entities;
using LoonyBin.Infrastructure.Extensions;
using Mapster;

namespace LoonyBin.Features.Patients
{
    public class PatientRequest : IRegister
    {
        public NameRequestDto Name { get; set; } = default!;

        public string? Gender { get; set; }

        public DateTime BirthDate { get; set; }


        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PatientRequest, Patient>()
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

    public class PatientRequestValidator : AbstractValidator<PatientRequest>
    {
        public PatientRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .SetValidator(new NameRequestDtoValidator());

            RuleFor(x => x.Gender)
                .IsEnumName(typeof(Gender), caseSensitive: false)
                .Unless(x => string.IsNullOrWhiteSpace(x.Gender))
                .WithMessage("Gender must be a valid value or empty.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Today).WithMessage("Birth date must be in the past");
        }
    }

    public class NameRequestDtoValidator : AbstractValidator<NameRequestDto>
    {
        public NameRequestDtoValidator()
        {
            RuleFor(x => x.Family)
                .NotEmpty().WithMessage("Family name is required.");

            RuleFor(x => x.Given)
                .Must(list => list == null || list.All(s => !string.IsNullOrWhiteSpace(s)))
                .WithMessage("Each given name must be non-empty.");
        }
    }
}