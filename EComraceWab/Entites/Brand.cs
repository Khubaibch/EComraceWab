using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class Brands
    {
        [Key]
          public int Id { get; set; }
          public string Brand { get; set; }
    }
}
