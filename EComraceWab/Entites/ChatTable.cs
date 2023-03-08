using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class ChatTable
    {
        [Key]
        public int Id { get; set; }
        public int? SenderId { get; set; }
        public virtual User Sender { get; set; }
        [ForeignKey(nameof(Receiver))]
        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
        public string Comment { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? GuestId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
