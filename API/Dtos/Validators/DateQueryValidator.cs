using FluentValidation;
using LoonyBin.Services.DateFilters;

namespace LoonyBin.API.Dtos.Validators
{
    public class DateQueryValidator: AbstractValidator<List<string>>
    {
        public DateQueryValidator()
        {
            When(list => list != null, () =>
            {
                RuleForEach(list => list)
                    .Must(BeValidDateFilter)
                    .WithMessage("Invalid date filter format: {PropertyValue}");
            });
        }

        private bool BeValidDateFilter(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter) || filter.Length < 3)
                return false;

            var prefix = filter[..2];

            return Enum.TryParse<FilterPrefixes>(prefix, true, out _);
        }
    }
}
