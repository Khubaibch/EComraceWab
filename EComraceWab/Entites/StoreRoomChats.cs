using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class StoreRoomChats
    {
        [Key]
        public int Id { get; set; }
        public int CreaterId { get; set; }
        [ForeignKey(nameof(StoreRoom))]
        public int StoreRoomId { get; set; }
        public virtual StoreRoom StoreRoom { get; set; }
        public string Comment { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Check { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
