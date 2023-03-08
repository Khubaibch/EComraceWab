using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class AdmainModel
    {
        public int Commission { get; set; }
        public List<Invoice> Pending { get; set; }
        public List<Invoice> Shipping { get; set; }
        public List<Invoice> Waiting { get; set; }
        public List<Invoice> Received { get; set; }
        public List<Room> Message { get; set; }
        public List<StoreRoom> StoreMessage { get; set; }
        public List<StoreRoomChats> StoreRoomChats { get; set; }
        public User Admain { get; set; }

    }
}
