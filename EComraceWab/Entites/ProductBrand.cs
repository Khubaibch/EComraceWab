using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class ProductBrand
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public virtual Brands Brand { get; set; }
    }
}
