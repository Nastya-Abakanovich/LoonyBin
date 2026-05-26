using FluentValidation;

namespace LoonyBin.DateFilters
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
            var date = filter[2..];

            if (!Enum.TryParse<FilterPrefixes>(prefix, true, out _))
                return false;

            return DateTimeOffset.TryParse(date, out _);
        }
    }
}
