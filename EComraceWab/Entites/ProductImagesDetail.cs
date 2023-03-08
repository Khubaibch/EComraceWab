using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class ProductImagesDetail
    {
        [Key]
        public int Id { get; set; }
        public string image { get; set; }
       
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }


    }
}
