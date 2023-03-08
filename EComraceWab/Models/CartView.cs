using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class CartView
    {
        public int Id { get; set; } 
        public int VariactionId { get; set; }
        public int SenderId { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; } 
        public int Price { get; set; } 
        public string Comment { get; set; } 
        public List<Shipping> Shipping { get; set; } 
        public List<ProductViewResponce> Products { get; set; }
        public ProductViewResponce Product { get; set; }
        public ChatTableView Chats { get; set; }
        public User User { get; set; }
        public List<ChatTable> Receiver { get; set; }
        public List<ChatTable> ChatData { get; set; }
        public ChatTableView ChatsData { get; set; }
        public List<RoomChats> RoomChat { get; set; }
        public List<StoreRoomChats> StoreRoomChats { get; set; }
      

    }
}
