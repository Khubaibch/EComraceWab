using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class ReturnProduct
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Reson { get; set; }
        [ForeignKey(nameof(OderItem))]
        public int OderItemId { get; set; }
        public virtual OderItems OderItem { get; set; } 
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime DateTime { get; set; }
        public bool Check { get; set; }
        public bool Add { get; set; }
        public int StoreId { get; set; }

    }
}
