using LoonyBin.Features.DateFilters;
using LoonyBin.Infrastructure.Entities;

namespace LoonyBin.Services.DateFilters
{
    public interface IDateSearchService
    {
        public IQueryable<Patient> ApplyDateFilter(IQueryable<Patient> query,
            DateFilter filter);
    }
}
