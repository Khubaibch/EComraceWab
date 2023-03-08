using EComraceWab.Entites;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Models
{
    public class ProductViewResponce 
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int VariactionId { get; set; }
        public string Price { get; set; }
        public int SubCategoryId { get; set; }
        public ProductsVariactionView Variaction { get; set; }

        public  SubCategory SubCategory { get; set; }
        public int ModifiedById { get; set; }
        public  User ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedById { get; set; }
        public  User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Specifications { get; set; }
        public string Warranty { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }
        public int Commission { get; set; }

        public string Image { get; set; }
        public List<ProductImagesDetail> Images { get; set; }
        public ProductMaterial Material { get; set; }
        public ProductBrand Brand { get; set; }
        public ProductType Type { get; set; }
        public Invoice Invoice { get; set; }
        public List<ProductViewResponce> Products { get; set; }
    }
}
