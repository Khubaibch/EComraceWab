using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EComraceWab.Models;

namespace EComraceWab.Entites
{
    public class ChatTableView
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public  User Sender { get; set; }
        public int ReceiverId { get; set; }
        public int CreaterId { get; set; }
        public  User Receiver { get; set; }
        public string Comment { get; set; }
        public string Token { get; set; }
        public int? ModifiedById { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public List<RoomChats> Chat { get; set; }
        public List<StoreRoomChats> StoreRoomChat { get; set; }
        public User User { get; set; }
        public List<Room> GuestRoom { get; set; }
        public List<CartView> StoreGuestRoom { get; set; }
        public List<CartView> UserRoom { get; set; }

    }
}
