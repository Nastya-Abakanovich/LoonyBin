namespace LoonyBin.DAL
{
    public class BaseDeletableEntity
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDateTime {  get; set; }
    }
}
