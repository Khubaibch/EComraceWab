using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class ReviewsView
    {
        public int Id { get; set; }
        public int ProductId { get; set; } 
        public  Product Product { get; set; }
        public int BuyerId { get; set; } 
        public  User User { get; set; }
        public int Ratings { get; set; } 
        public string Comment { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<ReviewsImages> ReviewsImages { get; set; }

    }
}
