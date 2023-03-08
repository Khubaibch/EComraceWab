using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class ProductsVariactionView
    {
        public int Id { get; set; }
        public string? Variaction { get; set; }
        public int ProductId { get; set; }
        public  Product Product { get; set; }
        public InventryView Inventrys { get; set; }
        public List<string> Colors { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
