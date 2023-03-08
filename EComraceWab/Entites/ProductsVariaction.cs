using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class ProductsVariaction
    {
        [Key]
        public int Id { get; set; }
        public string? Variaction { get; set; }
        public string? ColorVariaction { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public ICollection<Inventry> Inventryes { get; set; }
    }
}
