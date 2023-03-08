using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class Oders
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int StatusId { get; set; }
        public int ProductId { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public int VariactionId { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public int Quantity { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

        public List<ProductViewResponce> Product { get; set; }
        public List<Items> Items { get; set; }
        
       
        public Invoice Invoice { get; set; }
    }
}
