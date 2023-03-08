using EComraceWab.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Models
{
    public class UserAndStoreView
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public IFormFile Image { get; set; }
        public string Imagee { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public string City { get; set; }
        public string? ShippingInfo { get; set; }

        public int RoleId { get; set; }
        public Roles Role { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public int CreatedById { get; set; }
        public  User User { get; set; }
        public StoreResponse store { get; set; }
    }
}
