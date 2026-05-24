namespace LoonyBin.DAL
{
    public class Patient
    {
        public Guid Id { get; set; }

        public string FamilyName { get; set; } = default!;

        public List<string>? GivenNames { get; set; }

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}