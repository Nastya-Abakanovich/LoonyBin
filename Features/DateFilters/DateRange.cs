namespace LoonyBin.Features.DateFilters
{
    public record DateRange(DateTimeOffset Start, DateTimeOffset End)
    {
        public static DateRange Parse(string input)
        {
            if (input.Length == 4) // yyyy
            {
                var start = DateTimeOffset.Parse($"{input}-01-01T00:00:00Z");
                return new(start, start.AddYears(1));
            }

            if (input.Length == 7) // yyyy-MM
            {
                var start = DateTimeOffset.Parse($"{input}-01T00:00:00Z");
                return new(start, start.AddMonths(1));
            }

            if (input.Length == 10) // yyyy-MM-dd
            {
                var start = DateTimeOffset.Parse($"{input}T00:00:00Z");
                return new(start, start.AddDays(1));
            }

            // full datetime
            var dt = DateTimeOffset.Parse(input);
            return new(dt, dt.AddSeconds(1));
        }
    }
}
