namespace LoonyBin.DAL
{
    public class Patient: BaseDeletableEntity
    {
        public Guid Id { get; set; }

        public string FamilyName { get; set; } = default!;

        public List<string> GivenNames { get; set; } = default!;

        public Gender Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}