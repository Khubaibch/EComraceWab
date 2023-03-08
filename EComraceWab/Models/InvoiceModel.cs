using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Models
{
	public class InvoiceModel
	{
		
		public int Id { get; set; }	
		public int ShippingId { get; set; }	
		public int StatusId { get; set; }
		public int StoreId { get; set; }
		public string Status { get; set; }
		public string PhoneNumber { get; set; }
		public int Amount { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public DateTime DateTime { get; set; }	
		public string City { get; set; }
		public string Message { get; set; }
		
        public StoreResponse Store { get; set; }
        public List<Oders> Oders { get; set; }
        public Oders Oder { get; set; }
        public List<ProductViewResponce> Products { get; set; }

    }
}
