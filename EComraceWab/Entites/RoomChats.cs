using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class RoomChats
    {
        [Key]
        public int Id { get; set; }
        public int CreaterId { get; set; }
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public string Comment { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Check { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
