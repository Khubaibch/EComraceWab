using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class Store
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public string City { get; set; }
        public string? ShippingInfo { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public int CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public bool Block { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
