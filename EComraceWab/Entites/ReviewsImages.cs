using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EComraceWab.Entites
{
    public class ReviewsImages
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }

        [ForeignKey(nameof(Reviews))]
        public int ReviewsId { get; set; }
        public virtual Reviews Reviews { get; set; }

    }
}
