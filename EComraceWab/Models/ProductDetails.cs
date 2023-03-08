using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class ProductDetails
    {
        public ProductViewResponce Products { get; set; }
        public List<ProductsVariaction> ProductsVariaction { get; set; }
        public List<ReviewsView> Reviews { get; set; }
        public List<string> Images { get; set; }
        public ProductType ProductType { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public ProductMaterial ProductMaterial { get; set; } 
        public string ProductTypee { get; set; }
        public string ProductBrande { get; set; }
        public string ProductMateriale { get; set; }
        public SubCategory subCategory { get; set; }
        public List<SubCategoryView> SubCategoryView { get; set; }
        public Store Stores { get; set; }

        public List<ProductViewResponce> ProductViewResponce { get; set; }
        public List<int> Quantity { get; set; }
        public int Quantitye { get; set; }
        public string Specification { get; set; }
        public string Warranty { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public int VariactionId { get; set; } 
        public int ColorId { get; set; }
        public string Image { get; set; }


    }
}
