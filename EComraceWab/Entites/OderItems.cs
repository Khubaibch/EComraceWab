using EComraceWab.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class OderItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey(nameof(Variaction))]
        public int VariactionId { get; set; }
        public virtual ProductsVariaction Variaction { get; set; }
        [ForeignKey(nameof(Invoice))]
        public int InvoiceId { get; set; }
        
        public virtual Invoice Invoice { get; set; }


    }
}
