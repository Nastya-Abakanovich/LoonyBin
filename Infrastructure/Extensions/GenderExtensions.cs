using LoonyBin.Infrastructure.Entities;

namespace LoonyBin.Infrastructure.Extensions
{
    public static class GenderExtensions
    {
        public static Gender ToGender(this string? value)
        {
            if (Enum.TryParse<Gender>(value, true, out var g))
                return g;

            return Gender.Unknown;
        }
    }
}
