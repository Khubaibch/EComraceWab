using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class Shipping
    {
        [Key]
        public int Id { get; set; }
        public string Delivery { get; set; }
    }
}
