using System.ComponentModel.DataAnnotations.Schema;

namespace EComraceWab.Entites
{
    public class UserRole
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Roles))]
        public int RolesId { get; set; }
        public Roles Roles { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
