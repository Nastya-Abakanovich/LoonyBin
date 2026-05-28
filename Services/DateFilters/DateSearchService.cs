using LoonyBin.Infrastructure.Entities;

namespace LoonyBin.Services.DateFilters
{
    public class DateSearchService: IDateSearchService
    {
        public IQueryable<Patient> ApplyDateFilter(IQueryable<Patient> query,
            DateFilter filter)
        {
            return filter.Prefix switch
            {
                FilterPrefixes.Eq => query.Where(p => p.BirthDate >= filter.Range.Start && p.BirthDate < filter.Range.End),
                FilterPrefixes.Ne => query.Where(p => p.BirthDate < filter.Range.Start || p.BirthDate >= filter.Range.End),
                FilterPrefixes.Lt => query.Where(p => p.BirthDate < filter.Range.Start),
                FilterPrefixes.Le => query.Where(p => p.BirthDate <= filter.Range.End),
                FilterPrefixes.Gt => query.Where(p => p.BirthDate > filter.Range.End),
                FilterPrefixes.Ge => query.Where(p => p.BirthDate >= filter.Range.Start),
                FilterPrefixes.Sa => query.Where(p => p.BirthDate > filter.Range.End),
                FilterPrefixes.Eb => query.Where(p => p.BirthDate < filter.Range.Start),
                FilterPrefixes.Ap => ApplyApparentlyFilter(query, filter),
                _ => query
            };
        }

        private IQueryable<Patient> ApplyApparentlyFilter(IQueryable<Patient> query, DateFilter filter)
        {
            var len = filter.Range.End - filter.Range.Start;
            var delta = TimeSpan.FromTicks((long)(len.Ticks * 0.1));

            return query.Where(p => p.BirthDate >= filter.Range.Start - delta
                && p.BirthDate < filter.Range.End + delta);
        }
    }
}
