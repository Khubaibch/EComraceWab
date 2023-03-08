using System.ComponentModel.DataAnnotations.Schema;
using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class ProductImagesView
    {
        public int Id { get; set; }
        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
        public int ProductId { get; set; }
        public  Product Product { get; set; }
    }
}
