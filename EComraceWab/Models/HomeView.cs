using EComraceWab.Entites;
using EComraceWab.Models;

namespace EComraceWab.Models
{
	public class HomeView
	{
	 public List<ProductViewResponce> Products { get; set; }
	 public List<ProductView> Product { get; set; }
     public List<SubCategoryViewResponce> SubCategories { get; set; }
     public SubCategoryViewResponce SubCategoriess { get; set; }
     public List<CategoryViewResponce> Categories { get; set; }
     public List<StoreResponse> StoreResponses { get; set; }
   

	

	}
}
