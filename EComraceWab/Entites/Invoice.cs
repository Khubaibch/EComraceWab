using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
	public class Invoice
	{
		[Key]
		public int Id { get; set; }	
		public string PhoneNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string?  Address { get; set; }
		public string? City { get; set; }
		public string Message { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; } 
		[ForeignKey(nameof(Shipping))]
        public int ShippingId { get; set; }
        public virtual Shipping Shipping { get; set; }
      
		public int StoreId { get; set; }	
		public int StatusId { get; set; }	
		//[ForeignKey(nameof(PaymentMethod))]
  //      public int PaymentMethodId { get; set; }
  //      public virtual PaymentMethod PaymentMethod { get; set; }
		//public bool IsPaid { get; set; }
		public ICollection<OderItems> Orders { get; set; }
    }
}
