using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class StoreRoom
    {
        [Key]
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int? GuestId { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
