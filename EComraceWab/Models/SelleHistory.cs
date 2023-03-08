using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class SelleHistory
    {
        public int UserId { get; set; }
        public UserRole UserRole { get; set; }
        public UserAndStoreView User { get; set; }
        public StoreResponse store { get; set; }
    }
}
