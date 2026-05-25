using LoonyBin.DAL;

namespace LoonyBin
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
