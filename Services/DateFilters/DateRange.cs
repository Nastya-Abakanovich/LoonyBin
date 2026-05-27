using System.Globalization;

namespace LoonyBin.Features.DateFilters
{
    public record DateRange(DateTimeOffset Start, DateTimeOffset End)
    {
        private static readonly string[] YearFormats = { "yyyy" };
        private static readonly string[] YearMonthFormats = { "yyyy-MM" };
        private static readonly string[] DateFormats = { "yyyy-MM-dd" };
        private static readonly string[] DateTimeFormats =
        {
            "yyyy-MM-dd'T'HH:mm:ss'Z'",
            "yyyy-MM-dd'T'HH:mm:ss.FFF'Z'",
            "yyyy-MM-dd'T'HH:mm:ssK",
            "yyyy-MM-dd'T'HH:mm:ss.FFFK"
        };

        public static DateRange Parse(string input)
        {
            var styles = DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal;

            if (DateTimeOffset.TryParseExact(input, YearFormats, CultureInfo.InvariantCulture, styles, out var year))
                return new(year, year.AddYears(1));

            if (DateTimeOffset.TryParseExact(input, YearMonthFormats, CultureInfo.InvariantCulture, styles, out var yearMonth))
                return new(yearMonth, yearMonth.AddMonths(1));

            if (DateTimeOffset.TryParseExact(input, DateFormats, CultureInfo.InvariantCulture, styles, out var date))
                return new(date, date.AddDays(1));

            if (DateTimeOffset.TryParseExact(input, DateTimeFormats, CultureInfo.InvariantCulture, styles, out var dt))
                return new(dt, dt.AddSeconds(1));

            throw new FormatException($"Invalid date format: '{input}'.");
        }
    }
}
