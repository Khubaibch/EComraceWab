using EComraceWab.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [NotMapped]
        public virtual User User { get; set; }
    }
}

