using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EComraceWab.Entites;

namespace EComraceWab.Models
{
    public class CategoryViewResponce 
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? ModifiedById { get; set; }
        public User? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
