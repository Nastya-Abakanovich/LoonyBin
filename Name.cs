namespace LoonyBin
{
    public class Name
    {
        public Guid Id { get; set; }

        public string Family { get; set; } = default!;

        public List<string>? Given { get; set; }
    }
}
