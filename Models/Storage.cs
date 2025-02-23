namespace ASP.Seminar3.Models
{
    public class Storage : BaseModel
    {
        public int? Count { get; set; }
        public virtual List<ProductStorage> ProductStorages { get; set; } = new List<ProductStorage>();

    }
}
