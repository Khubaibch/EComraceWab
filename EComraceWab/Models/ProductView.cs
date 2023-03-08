using EComraceWab.Entites;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Models
{
    public class ProductView 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int SubCategoryId { get; set; }
        public  SubCategory SubCategory { get; set; }
        public int ModifiedById { get; set; }
        public  User ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedById { get; set; }
        public  User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public IFormFile Image { get; set; }
       
        public List<string> Variactions { get; set; }
        public List<IFormFile> Images { get; set; } 
        public ProductImagesDetail ProductImagesDetail { get; set; }  
        public string Type { get; set; } 
        public int TypeId { get; set; } 
        public string Brand { get; set; } 
        public int BrandId { get; set; } 
        public string Specification { get; set; }
        public string Warranty { get; set; }
        public string Material { get; set; }
        public int MaterialId { get; set; }
    }
}
