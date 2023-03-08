namespace EComraceWab.Models
{
    public class Items
    {
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int VariactionId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Commission { get; set; }
        public List<ProductViewResponce> Product { get; set; }

    }
}
