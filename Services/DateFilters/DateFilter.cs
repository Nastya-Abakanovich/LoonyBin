namespace LoonyBin.Features.DateFilters
{
    public class DateFilter()
    {
        public FilterPrefixes Prefix { get; set; }

        public DateRange Range { get; set; } = default!;

        public static DateFilter Parse(string raw)
        {
            return new DateFilter
            {
                Prefix = Enum.Parse<FilterPrefixes>(raw[..2], true),
                Range = DateRange.Parse(raw[2..])
            };
        }
    }
}
