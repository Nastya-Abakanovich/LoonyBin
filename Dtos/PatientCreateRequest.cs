using FluentValidation;
using LoonyBin.DAL;
using Mapster;

namespace LoonyBin.Dtos
{
    public class PatientCreateRequest : IRegister
    {
        public NameCreateDto Name { get; set; } = default!;

        public string? Gender { get; set; }

        public DateTime BirthDate { get; set; }


        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PatientCreateRequest, Patient>()
                .Map(dest => dest.Id, src => src.Name.Id)
                .Map(dest => dest.FamilyName, src => src.Name.Family)
                .Map(dest => dest.GivenNames, src => src.Name.Given)
                .Map(dest => dest.Gender, src => src.Gender.ToGender());
        }
    }

    public class NameCreateDto
    {
        public Guid? Id { get; set; }

        public string Family { get; set; } = default!;

        public List<string>? Given { get; set; }
    }

    public class PatientCreateRequestValidator : AbstractValidator<PatientCreateRequest>
    {
        public PatientCreateRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required.")
            .SetValidator(new NameCreateDtoValidator());

            RuleFor(x => x.Gender)
                .IsEnumName(typeof(Gender), caseSensitive: false)
                .Unless(x => string.IsNullOrWhiteSpace(x.Gender))
                .WithMessage("Gender must be a valid value or empty.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.Today).WithMessage("Birth date must be in the past");
        }
    }

    public class NameCreateDtoValidator : AbstractValidator<NameCreateDto>
    {
        public NameCreateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .When(x => x.Id.HasValue)
                .WithMessage("Id must not be a valid Guid or empty.");

            RuleFor(x => x.Family)
                .NotEmpty().WithMessage("Family name is required.");
        }
    }
}