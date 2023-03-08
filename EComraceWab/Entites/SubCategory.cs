using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EComraceWab.Models;

namespace EComraceWab.Entites;

public partial class SubCategory : AuditedEntity
{

    public string Name { get; set; }
    public int Commission { get; set; }
    public string Description { get; set; }
    
    public string Image { get; set; }

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    //public ICollection<ProductViewResponce> Product { get; set; }

}
