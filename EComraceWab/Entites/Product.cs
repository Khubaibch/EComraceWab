using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EComraceWab.Models;

namespace EComraceWab.Entites;

public  class Product : AuditedEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }
  
    [ForeignKey(nameof(SubCategory))]
    public int SubCategoryId { get; set; }
    public virtual SubCategory SubCategory { get; set; } 
    [ForeignKey(nameof(Store))]
    public  int StoreId { get; set; }
    public virtual Store Store { get; set; }
    

    public virtual ICollection<ProductBrand> Brands { get; set; }
    public virtual ICollection<ProductsVariaction> ProductsVariaction { get; set; }
}
