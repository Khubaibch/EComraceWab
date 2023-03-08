using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class Inventry
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        [ForeignKey(nameof(Variaction))]
        public int VariactionId { get; set; }
        public virtual ProductsVariaction Variaction { get; set; } 
    }
}
