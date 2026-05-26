namespace LoonyBin.Infrastructure.Entities
{
    public class BaseDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDateTime {  get; set; }
    }
}
