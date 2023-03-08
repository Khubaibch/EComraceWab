using System;
using System.Collections.Generic;
using EComraceWab.Models;
using Microsoft.EntityFrameworkCore;

namespace EComraceWab.Entites;

public partial class DarazContext : DbContext
{
    public DarazContext(DbContextOptions<DarazContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<SubCategory> SubCategories { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<ProductImagesDetail> ProductImagesDetail { get; set; }
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Wishlist> Wishlist { get; set; }
    public virtual DbSet<ProductBrand> ProductBrands { get; set; }
    public virtual DbSet<Brands> Brands { get; set; }
    public virtual DbSet<ProductMaterial> ProductMaterials { get; set; }
    public virtual DbSet<ProductType> ProductTypes { get; set; }
    public virtual DbSet<Invoice> Invoice { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethod { get; set; }
    public virtual DbSet<OderItems> OderItems { get; set; }
    public virtual DbSet<Shipping> Shipping { get; set; }
    public virtual DbSet<WarrantyInfo> WarrantyInfo { get; set; }
    public virtual DbSet<Specifications> Specifications { get; set; }
    public virtual DbSet<Roles> Roles { get; set; }
    public virtual DbSet<Store> Store { get; set; }
    public virtual DbSet<UserRole> UserRole { get; set; }
    public virtual DbSet<Reviews> Reviews { get; set; }
    public virtual DbSet<ReviewsImages> ReviewsImages { get; set; }
    public virtual DbSet<ProductsVariaction> ProductsVariactions { get; set; }
    public virtual DbSet<Inventry> Inventries { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<RoomChats> RoomChats { get; set; }
    public virtual DbSet<StoreRoom> StoreRooms { get; set; }
    public virtual DbSet<StoreRoomChats> StoreRoomChats { get; set; }
    public virtual DbSet<ReturnProduct> ReturnProducts { get; set; }



}
