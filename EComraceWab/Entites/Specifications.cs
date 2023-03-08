using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class Specifications
    {
        public int Id { get; set; }
        public string? Specification { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
