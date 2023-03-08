using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class Reviews
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; } 
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(User))]
        public int BuyerId { get; set; } 
        public virtual User User { get; set; }
        public int Ratings { get; set; } 
        public string Comment { get; set; } 
    }
}
