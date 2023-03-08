using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class WarrantyInfo
    {
        public int Id { get; set; }
        public string? Warranty { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
