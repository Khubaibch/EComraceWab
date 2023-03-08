using EComraceWab.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }   
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(Variaction))]
        public int VariactionId { get; set; }
        public virtual ProductsVariaction Variaction { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

