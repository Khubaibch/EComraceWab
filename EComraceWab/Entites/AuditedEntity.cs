

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class AuditedEntity
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(ModifiedBy))]
        public int? ModifiedById { get; set; }
        [NotMapped]
        public virtual User? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public int CreatedById { get; set; }
        [NotMapped]
        public virtual User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
