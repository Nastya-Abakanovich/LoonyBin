namespace LoonyBin.Features.DateFilters
{
    public record DateFilter(FilterPrefixes Prefix, DateRange Range)
    {
        public static DateFilter Parse(string raw)
        {
            var prefix = Enum.Parse<FilterPrefixes>(raw[..2], true);
            var range = DateRange.Parse(raw[2..]);

            return new (prefix, range);
        }
    }
}
