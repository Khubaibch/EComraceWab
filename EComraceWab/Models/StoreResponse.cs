using EComraceWab.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Models
{
    public class StoreResponse
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public string City { get; set; }
        public bool Block { get; set; }
        public string? ShippingInfo { get; set; }

        public int CreatedById { get; set; }
        public  User CreatedBy { get; set; }
        public Oders Oders { get; set; }

        public List<ProductViewResponce> product { get; set; }
    }
}
