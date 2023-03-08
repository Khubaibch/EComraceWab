using EComraceWab.Entites;
using EComraceWab.Models;
using EComraceWab.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using Invoice = EComraceWab.Entites.Invoice;
using Newtonsoft.Json.Linq;
using OtpNet;

namespace EComraceWab.Controllers
{
    public class DarazController : Controller
    {
        private readonly ILogger<DarazController> _logger;
        private readonly DarazContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContext;
        public DarazController(ILogger<DarazController> logger, DarazContext context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
            _httpContext = httpContext;
            string Basepath = _hostEnvironment.WebRootPath;
            string ContentPath = _hostEnvironment.ContentRootPath;
        }
        //public IActionResult Home()
        //{
        //    return View();
        //}
        public JsonResult HomeData()
        {
            HomeView homeView = new HomeView();
            var Store = _context.Store.Where(x => x.Block == true).Select(x => new StoreResponse()
            {
                Id = x.Id,

                product = x.Products.Select(p => new ProductViewResponce()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Image = p.Image,
                    CreatedDate = p.CreatedDate,
                    Variaction = p.ProductsVariaction.Select(v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Inventrys = v.Inventryes.Select(i => new InventryView()
                        {
                            Price = i.Price,
                            Id = i.Id,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),

                }).ToList(),


            }).ToList();


            var subcategory = _context.SubCategories.Select(s => new SubCategoryViewResponce() { Id = s.Id, Name = s.Name, Description = s.Description, Image = s.Image, }).ToList();
            var Category = _context.Categories.Select(s => new CategoryViewResponce() { Id = s.Id, Name = s.Name, Description = s.Description, Image = s.Image }).ToList();
            homeView.StoreResponses = Store;
            homeView.SubCategories = subcategory;
            homeView.Categories = Category;
            return Json(homeView);
        }

        public IActionResult SubCategoryOrProduct()
        {


            return View();

        }
        public JsonResult SubCategoryOrProducts(int id)
        {
            var SubCategory = _context.SubCategories.Where(s => s.CategoryId == id).
                Select(s => new HomeView()
                {
                    SubCategoriess = _context.SubCategories.Where(s => s.CategoryId == id).Select(x => new SubCategoryViewResponce()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Description = s.Description,
                        Image = s.Image,
                        Products = _context.Products.Where(x => x.SubCategoryId == s.Id).Select(x => new ProductViewResponce()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Image = x.Image,
                            Description = x.Description,
                            CreatedDate = x.CreatedDate,
                            Variaction = _context.ProductsVariactions.Where(v => v.ProductId == x.Id).Select(
                                v => new ProductsVariactionView()
                                {
                                    Id = v.Id,
                                    Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                                    {
                                        Price = i.Price,
                                        Quantity = i.Quantity,
                                    }).SingleOrDefault(),
                                }).SingleOrDefault(),
                        }).ToList(),
                        Brands = _context.Brands.ToList(),
                        Variactions = _context.ProductsVariactions.ToList(),


                    }).SingleOrDefault(),
                });

            return Json(SubCategory);

        }
       
        public IActionResult Products()
        {

            return View();
        }
        public JsonResult Productss(int id)
        {
            if (id == 0)
            {
                var Product = _context.Products.Select(s => new ProductViewResponce()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    CreatedDate = s.CreatedDate,
                    Variaction = _context.ProductsVariactions.Where(v => v.ProductId == s.Id).Select(
                        v => new ProductsVariactionView()
                        {
                            Id = v.Id,
                            Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                            {
                                Price = i.Price,
                                Quantity = i.Quantity,
                            }).SingleOrDefault(),
                        }).SingleOrDefault(),
                }).ToList();
                return Json(Product);
            }
            else
            {
                var Products = _context.Products.Where(s => s.SubCategoryId == id).Select(s => new ProductViewResponce()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    CreatedDate = s.CreatedDate,
                    Variaction = _context.ProductsVariactions.Where(v => v.ProductId == s.Id).Select(
                        v => new ProductsVariactionView()
                        {
                            Id = v.Id,
                            Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                            {
                                Price = i.Price,
                                Quantity = i.Quantity,
                            }).SingleOrDefault(),
                        }).SingleOrDefault(),
                }).ToList();
                return Json(Products);
            }
        }
        public IActionResult SearchProducts()
        {

            return View();
        }
        public JsonResult ProductsName(string id)
        {
            if (id == null)
            {

                var Product = _context.Products.Select(s => new ProductViewResponce()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    CreatedDate = s.CreatedDate,
                    Variaction = _context.ProductsVariactions.Where(v => v.ProductId == s.Id).Select(
                           v => new ProductsVariactionView()
                           {
                               Id = v.Id,
                               Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                               {
                                   Price = i.Price,
                                   Quantity = i.Quantity,
                               }).SingleOrDefault(),
                           }).SingleOrDefault(),
                }).ToList();

                return Json(Product);
            }
            else
            {
                var Product = _context.Products.Where(s => s.Name.Contains(id)).Select(s => new ProductViewResponce()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    CreatedDate = s.CreatedDate,
                    Variaction = _context.ProductsVariactions.Where(v => v.ProductId == s.Id).Select(
                        v => new ProductsVariactionView()
                        {
                            Id = v.Id,
                            Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                            {
                                Price = i.Price,
                                Quantity = i.Quantity,
                            }).SingleOrDefault(),
                        }).SingleOrDefault(),
                }).ToList();

                return Json(Product);
            }
        }
        [HttpGet]
        public IActionResult ProductDetail()
        {

            return View();
        }

        public JsonResult ProductDetails(int id)
        {


            ProductDetails details = new ProductDetails();
            var data = _context.Products.Where(x => x.Id == id).Select(s => new ProductViewResponce()
            {
                Id = s.Id,
                Name = s.Name,
                Image = s.Image,
                Description = s.Description,
                StoreId = s.StoreId,
                Variaction = _context.ProductsVariactions.Where(v => v.ProductId == s.Id).Select(
                    v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                        {
                            Price = i.Price,
                            Quantity = i.Quantity,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),

            }).FirstOrDefault();
            var Image = _context.ProductImagesDetail.Where(s => s.ProductId == id).Select(x => x.image).ToList();
            var BrandId = _context.ProductBrands.Where(s => s.ProductId == id).Select(x => x.BrandId).SingleOrDefault();
            var Brand = _context.Brands.Where(b => b.Id == BrandId).Select(b => b.Brand).SingleOrDefault();
            var Type = _context.ProductTypes.Where(s => s.ProductId == id).Select(x => x.Type).SingleOrDefault();
            var Material = _context.ProductMaterials.Where(s => s.ProductId == id).Select(x => x.Material).SingleOrDefault();
            var cat = _context.Products.Where(s => s.Id == id).Select(x => x.SubCategoryId).SingleOrDefault();
            var Subcat = _context.Products.Where(x => x.SubCategoryId == cat).Select(x => new ProductViewResponce()
            {
                Id = x.Id,
                Image = x.Image,
                Name = x.Name,
                Variaction = _context.ProductsVariactions.Where(v => v.ProductId == x.Id).Select(
                    v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                        {
                            Price = i.Price,
                            Quantity = i.Quantity,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),
            }).ToList();
            var Specification = _context.Specifications.Where(x => x.ProductId == id).Select(x => x.Specification).SingleOrDefault();
            var Variaction = _context.ProductsVariactions.Where(v => v.ProductId == id).Select(v => new ProductsVariaction()
            {
                Id = v.Id,
                Variaction = v.Variaction,
            }).ToList();

            var Storedata = _context.Store.Where(p => p.Id == data.StoreId).Select(p => new Store()
            {
                Name = p.Name,
                Number = p.Number,
                ShippingInfo = p.ShippingInfo,
                Address = p.Address,
                City = p.City,
            }).SingleOrDefault();
            var Review = _context.Reviews.Where(r => r.ProductId == id).Select(r => new ReviewsView()
            {
                Id = r.Id,
                Comment = r.Comment,
                Ratings = r.Ratings,
                BuyerId = r.BuyerId,
                User = new User() { FirstName = r.User.FirstName },
                ReviewsImages = _context.ReviewsImages.Where(i => i.ReviewsId == r.Id).Select(i => new ReviewsImages()
                {
                    Id = i.Id,
                    Image = i.Image,
                }).ToList(),

            }).ToList();

            var Warranty = _context.WarrantyInfo.Where(x => x.ProductId == id).Select(x => x.Warranty).SingleOrDefault();
            details.Products = data;
            details.Stores = Storedata;
            details.Images = Image;
            details.Specification = Specification;
            details.Warranty = Warranty;
            details.ProductBrande = Brand;
            details.ProductMateriale = Material;
            details.ProductTypee = Type;
            details.ProductViewResponce = Subcat;
            details.Reviews = Review;
            details.ProductsVariaction = Variaction;


            return Json(details);
        }
        public JsonResult VariactionData(int id)
        {
            var data = _context.Inventries.Where(x => x.VariactionId == id).Select(x => new Inventry()
            {
                Price = x.Price,
                Quantity = x.Quantity,
                Id = x.Id,
            }).SingleOrDefault();
            return Json(data);
        }

        [Authorize]
        public JsonResult AddToCart(Items obj)
        {

            var UserId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var data = _context.Items.Where(x => x.UserId == UserId).ToList();
            var select = data.Where(x => x.ProductId == obj.ProductId).ToList();
            var variation = select.Where(x => x.VariactionId == obj.VariactionId).Select(x => x.Id).FirstOrDefault();
            if (variation != 0)
            {
                Item items = _context.Items.Where(x => x.Id == variation).FirstOrDefault();
                var Quantity = items.Quantity + obj.Quantity;
                items.Quantity = Quantity;
                _context.Items.Update(items);
                _context.SaveChanges();
                return Json(items);
            }
            else
            {
                Item item = new Item();
                item.ProductId = obj.ProductId;
                if (obj.VariactionId != null)
                {
                    item.VariactionId = obj.VariactionId;
                }
                if (obj.Quantity != null)
                {
                    item.Quantity = obj.Quantity;
                }
                item.UserId = UserId;
                _context.Items.Add(item);
                _context.SaveChanges();


                return Json(item);
            }
           
        }
        public IActionResult CartView()
        {
            return View("CartData");
        }
        [Authorize]
        public JsonResult CartData()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var items = _context.Items.Where(x => x.UserId == userId)
                .Select(x => new CartView()
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    VariactionId = x.VariactionId,
                    Product = new ProductViewResponce()
                    {
                        Id = x.Product.Id,
                        Image = x.Product.Image,
                        Name = x.Product.Name,
                        Description = x.Product.Description,
                        Variaction = _context.ProductsVariactions.Where(v => v.Id == x.VariactionId).Select(
                    v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Variaction = v.Variaction,
                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                        {
                            Id = i.Id,
                            Price = i.Price,
                            Quantity = i.Quantity,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),
                    }
                }).ToList();


            return Json(items);

        }
        [Authorize]
        public JsonResult Quantity()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var data = _context.Items.Where(x => x.UserId == userId).Select(x => x.Quantity).ToList();
            var Quantity = 0;
            foreach (var quantity in data)
            {
                Quantity += quantity;
            }
            return Json(Quantity);
        }
        public JsonResult RemoveItemInCart(int id)
        {
            var data = _context.Items.Where(x => x.Id == id).SingleOrDefault();
            _context.Items.Remove(data);
            _context.SaveChanges();
            return new JsonResult("Data Deleted ");
        }
        public JsonResult RemoveItemInWishlist(int id)
        {
            var data = _context.Wishlist.Where(x => x.Id == id).SingleOrDefault();
            _context.Wishlist.Remove(data);
            _context.SaveChanges();
            return new JsonResult("Data Deleted ");
        }

        public IActionResult Checkoute()
        {
            return View("Checkout");
        }
        [Authorize]
        public JsonResult Checkout(Items obj)
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var items = _context.Items.Where(x => x.UserId == userId)
                .Select(x => new CartView()
                {
                    Id = x.Id,
                    VariactionId = x.VariactionId,
                    Shipping = _context.Shipping.Select(x => new Shipping() { Id = x.Id, Delivery = x.Delivery }).ToList(),
                    Products = _context.Items.Where(x => x.UserId == userId).Select(x => new ProductViewResponce()
                    {
                        Id = x.Product.Id,
                        Quantity = x.Quantity,
                        Image = x.Product.Image,
                        Description = x.Product.Description,
                        Variaction = _context.ProductsVariactions.Where(v => v.Id == x.VariactionId).Select(
                    v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Variaction = v.Variaction,
                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                        {
                            Id = i.Id,
                            Price = i.Price,
                            Quantity = i.Quantity,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),
                    }).ToList(),
                }).FirstOrDefault();


            return Json(items);

        }
        public JsonResult UpdateQuantity(Items obj)
        {
            Item data = _context.Items.Where(x => x.Id == obj.Id).FirstOrDefault();
           
            data.Quantity = obj.Quantity;
            _context.Items.Update(data);
            _context.SaveChanges();
            return Json(data);
        }
        [Authorize]
        public JsonResult Invoice(InvoiceModel obj)
        {

            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var items = _context.Items.Where(x => x.UserId == userId)
                .Select(x => new CartView()
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    VariactionId = x.VariactionId,
                    Product = new ProductViewResponce()
                    {
                        Id = x.Product.Id,
                        Image = x.Product.Image,
                        StoreId = x.Product.StoreId,
                        Description = x.Product.Description,
                        Variaction = _context.ProductsVariactions.Where(v => v.Id == x.VariactionId).Select(
                    v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Variaction = v.Variaction,
                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                        {
                            Price = i.Price,
                            Quantity = i.Quantity,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),
                    },
                }).ToList().GroupBy(g => g.Product.StoreId).ToList();
            foreach (var item in items)
            {

                var statusid = ((int)ShippingStatus.Pending);
                Invoice invoice = new Invoice();
                invoice.FirstName = obj.FirstName;
                invoice.LastName = obj.LastName;
                invoice.Email = obj.Email;
                invoice.City = obj.City;
                invoice.Address = obj.Address;
                invoice.PhoneNumber = obj.PhoneNumber;
                invoice.Message = obj.Message;
                invoice.DateTime = DateTime.Now;
                invoice.UserId = userId;
                invoice.ShippingId = obj.ShippingId;
                invoice.StatusId = statusid;
                invoice.StoreId = item.Key;
                _context.Invoice.Add(invoice);
                _context.SaveChanges();
                var InvoiceId = invoice.Id;
                foreach (var Ids in item)
                {
                    var price = Ids.Product.Variaction.Inventrys.Price + 50;
                    var Quantity = Ids.Quantity;
                    var total = (Quantity * price);
                    OderItems items1 = new OderItems();
                    items1.ProductId = Ids.Product.Id;
                    items1.Quantity = Ids.Quantity;
                    items1.VariactionId = Ids.VariactionId;
                    items1.InvoiceId = InvoiceId;
                    items1.Price = total;
                    _context.OderItems.Add(items1);
                    _context.SaveChanges();
                    Product product = _context.Products.Where(x => x.Id == Ids.Product.Id).FirstOrDefault();
                    //  product.Quantity = (Ids.Product.Quantity - Ids.Quantity);
                    _context.Products.Update(product);
                    _context.SaveChanges();
                }
            }
            var UserId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var ItemsId = _context.Items.Where(x => x.UserId == UserId).Select(x => x.Id).ToList();
            foreach (var item in ItemsId)
            {
                Item items1 = new Item();
                items1.Id = item;
                _context.Items.Remove(items1);
                _context.SaveChanges();
            }

            return Json(obj);
        }
        public IActionResult OdersView()
        {
            return View("OdersData");
        }
        [Authorize]
        public JsonResult OdersData()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var items = _context.Invoice.Where(x => x.UserId == userId).OrderByDescending(x => x.DateTime).Select(x => x.Id).ToList();
            var tesmp = _context.Invoice.Where(n => items.Contains(n.Id))
                .Select(x => new InvoiceModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    City = x.City,
                    PhoneNumber = x.PhoneNumber,
                    Email = x.Email,
                    DateTime = x.DateTime,
                    StoreId = x.StoreId,
                    Store = _context.Invoice.Where(s => s.Id == x.Id).Select(s => new StoreResponse()
                    {
                        // Id = s.Product.StoreId,
                        Oders = new Oders()
                        {
                            Id = x.Id,

                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Address = x.Address,
                            City = x.City,
                            PhoneNumber = x.PhoneNumber,
                            Email = x.Email,
                            StoreId = x.StoreId,
                            DateTime = x.DateTime,
                            Status = ((ShippingStatus)x.StatusId).ToString(),
                            Product = _context.OderItems.Where(p => p.InvoiceId == x.Id).Select(p => new ProductViewResponce()
                            {
                                Id = p.Id,
                                ProductId = p.Product.Id,
                                Name = p.Product.Name,
                                Description = p.Product.Description,
                                Image = p.Product.Image,
                                Quantity = p.Quantity,
                                VariactionId = p.VariactionId,
                                Variaction = _context.ProductsVariactions.Where(v => v.Id == p.VariactionId).Select(
                    v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Variaction = v.Variaction,
                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                        {
                            Id = i.Id,
                            Price = i.Price,
                            Quantity = i.Quantity,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),

                            }).ToList(),

                        }
                    }).SingleOrDefault(),
                }).ToList();

            return Json(tesmp);
        }
        public IActionResult Ordershistory()
        {
            return View("OrdershistoryData");
        }
        [Authorize]
        public JsonResult OrdershistoryData()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var items = _context.Invoice.Where(x => x.UserId == userId)
                //  var tesmp = _context.OderItems.Where(x => x.InvoiceId == items)
                .Select(x => new InvoiceModel()
                {
                    Oder = new Oders()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = x.Address,
                        City = x.City,
                        PhoneNumber = x.PhoneNumber,
                        Email = x.Email,
                        DateTime = x.DateTime,
                        Status = ((ShippingStatus)x.StatusId).ToString(),
                        Product = _context.OderItems.Where(y => y.InvoiceId == x.Id).Select(y => new ProductViewResponce()
                        {

                            Name = y.Product.Name,
                            Description = y.Product.Description,
                            Image = y.Product.Image,
                            Quantity = y.Quantity,
                            VariactionId = y.VariactionId,
                            Variaction = _context.ProductsVariactions.Where(v => v.Id == y.VariactionId).Select(
                            v => new ProductsVariactionView()
                            {
                                Id = v.Id,
                                Variaction = v.Variaction,
                                Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                                {
                                    Id = i.Id,
                                    Price = i.Price,
                                    Quantity = i.Quantity,
                                }).SingleOrDefault(),
                            }).SingleOrDefault(),
                        }).ToList(),
                    }
                });
            return Json(items);
        }
        public IActionResult Profile()
        {
            return View("ProfileSetting");
        }
        [Authorize]
        [HttpGet]
        public JsonResult ProfileSetting()
        {
            var UserId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var data = _context.Users.Where(x => x.Id == UserId).Select(x => new User()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
                ConfirmPassword = x.ConfirmPassword,
                Image = x.Image,
            }).FirstOrDefault();
            var store = _context.Store.Where(x => x.CreatedById == UserId).Select(x => new StoreResponse()
            {
                Id = x.Id,
                Address = x.Address,
                Name = x.Name,
                Number = x.Number,
                City = x.City,
                ShippingInfo = x.ShippingInfo,
            }).FirstOrDefault();
            UserAndStoreView view = new UserAndStoreView();
            view.store = store;
            view.User = data;
            return Json(view);
        }
        [Authorize]
        public JsonResult UpdateUser(UserAndStoreView obj)
        {
            User model = _context.Users.Where(x => x.Id == obj.Id).FirstOrDefault();
            model.FirstName = obj.FirstName;
            model.LastName = obj.LastName;
            model.Password = obj.Password;
            model.ConfirmPassword = obj.ConfirmPassword;
            model.Email = obj.Email;
            var path = _hostEnvironment.WebRootPath;
            var filepath = "/images/" + obj.Image.FileName;
            var fullpath = Path.Combine(path + filepath);
            UplodImage(obj.Image, fullpath);
            model.Image = filepath;
            _context.Users.Update(model);
            _context.SaveChanges();
            if (obj.StoreId != 0)
            {
                Store store = _context.Store.Where(x => x.Id == obj.StoreId).FirstOrDefault();
                store.Name = obj.Name;
                store.Number = obj.Number;
                store.Address = obj.Address;
                store.City = obj.City;
                store.ShippingInfo = obj.ShippingInfo;
                _context.Store.Update(store);
                _context.SaveChanges();
                return Json(store);
            }
            return Json(model);
        }
        public void UplodImage(IFormFile file, string path)
        {
            using FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            stream.Dispose();
        }
        [Authorize]
        public JsonResult Wishlist(int Id)
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            Wishlist wishlist = new Wishlist();
            wishlist.ProductId = Id;
            wishlist.UserId = userId;
            _context.Wishlist.Add(wishlist);
            _context.SaveChanges();

            return Json(Id);
        }
        public IActionResult Wishes()
        {
            return View("WishListView");
        }
        [Authorize]
        public JsonResult WishListView()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var items = _context.Wishlist.Where(x => x.UserId == userId)
              .Select(x => new CartView()
              {
                  Id = x.Id,
                  Product = new ProductViewResponce()
                  {
                      Id = x.Product.Id,
                      Image = x.Product.Image,
                      Description = x.Product.Description,
                      Variaction = _context.ProductsVariactions.Where(v => v.ProductId == x.Product.Id).Select(
                            v => new ProductsVariactionView()
                            {
                                Id = v.Id,
                                Variaction = v.Variaction,
                                Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                                {
                                    Id = i.Id,
                                    Price = i.Price,
                                    Quantity = i.Quantity,
                                }).SingleOrDefault(),
                            }).SingleOrDefault(),
                  }
              }).ToList();
            return Json(items);
        }

        [Authorize]
        public JsonResult OdersCancel(int id)
        {
            var dataa = _context.OderItems.Where(x => x.InvoiceId == id).Select(x => x.Id).ToList();

            foreach (var dd in dataa)
            {
                var @return = _context.ReturnProducts.Where(x => x.OderItemId == dd).FirstOrDefault();
                if (@return != null)
                {
                    _context.ReturnProducts.Remove(@return);
                    _context.SaveChanges();
                }
            }

            var data = _context.Invoice.Where(x => x.Id == id).FirstOrDefault();
            _context.Invoice.Remove(data);
            _context.SaveChanges();
            return Json(data);
        }
        [Authorize]
        public JsonResult DeleteItem(int id)
        {
            var @return = _context.ReturnProducts.Where(x => x.OderItemId == id).FirstOrDefault();
            if (@return != null)
            {
                _context.ReturnProducts.Remove(@return);
                _context.SaveChanges();
            }
            var data = _context.OderItems.Where(x => x.Id == id).FirstOrDefault();
            _context.OderItems.Remove(data);
            _context.SaveChanges();
            return Json(data);
        }
        [Authorize]
        public JsonResult Review(int id)
        {
            var data = _context.Products.Where(x => x.Id == id).Select(x => new ProductViewResponce()
            {
                Image = x.Image,
                Name = x.Name,
                Id = x.Id,
            }).SingleOrDefault();
            return Json(data);
        }
        [Authorize]
        public JsonResult ReviewData(ReviewsView obj)
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            Reviews reviews = new Reviews();
            reviews.ProductId = obj.ProductId;
            reviews.Comment = obj.Comment;
            reviews.Ratings = obj.Ratings;
            reviews.BuyerId = userId;
            _context.Reviews.Add(reviews);
            _context.SaveChanges();
            var ReviewId = reviews.Id;
            foreach (var image in obj.Images)
            {
                ReviewsImages images = new ReviewsImages();
                var pathe = _hostEnvironment.WebRootPath;
                var filepathe = "/images/" + image.FileName;
                var fullpathe = Path.Combine(pathe + filepathe);
                UplodImages(image, fullpathe);
                images.ReviewsId = ReviewId;
                images.Image = filepathe;
                _context.ReviewsImages.Add(images);
                _context.SaveChanges();
            }
            return Json(reviews);
        }
        public void UplodImages(IFormFile file, string pathe)
        {
            using FileStream stream = new FileStream(pathe, FileMode.Create);
            file.CopyTo(stream);
            stream.Dispose();
        }
        public JsonResult Fillter(FillterData obj)
        {
            IQueryable<Product> Data = _context.Products.AsQueryable<Product>();
            if (obj.BrandId != null)
            {
                Data = Data.Where(x => (x.Brands.Any(y => obj.BrandId.Contains(y.BrandId))));
            }
            //if (obj.ColorId != null)
            //{
            //    Data = Data.Where(x => (x.Colors.Any(y => obj.ColorId.Contains(y.ColorsId))));
            //}
            if (obj.SizeIds != null)
            {
                Data = Data.Where(x => (x.ProductsVariaction.Any(y => obj.SizeIds.Contains(y.ProductId))));
            }
            //if (obj.Price != null)
            //{
            //    Data = Data.Where(x => x.Price == obj.Price);
            //}
            var Products = Data.Select(x => new ProductViewResponce()
            {
                Name = x.Name,
                Image = x.Image,
                Id = x.Id,
                Variaction = _context.ProductsVariactions.Where(v => v.ProductId == x.Id).Select(
                    v => new ProductsVariactionView()
                    {
                        Id = v.Id,
                        Variaction = v.Variaction,
                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                        {
                            Id = i.Id,
                            Price = i.Price,
                            Quantity = i.Quantity,
                        }).SingleOrDefault(),
                    }).SingleOrDefault(),
            }).ToList();
            return Json(Products);
        }
        public JsonResult BuyerChat(ChatTableView obj)
        {
            if (obj.RoomId != 0)
            {
                if (obj.GuestId != 0)
                {
                    StoreRoomChats chat = new StoreRoomChats();
                    chat.CreaterId = obj.GuestId;
                    chat.Comment = obj.Comment;
                    chat.StoreRoomId = obj.RoomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.StoreRoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.StoreRoomChats.Where(x => x.StoreRoomId == obj.RoomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.GuestId = obj.GuestId;
                    return Json(cart);
                }
                else
                {
                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    StoreRoomChats chat = new StoreRoomChats();
                    chat.CreaterId = userId;
                    chat.Comment = obj.Comment;
                    chat.StoreRoomId = obj.RoomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.StoreRoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.StoreRoomChats.Where(x => x.StoreRoomId == obj.RoomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.Id = userId;
                    return Json(cart);
                }
            }
            else
            {
                if (obj.GuestId != 0)
                {
                    var ReceiverId = _context.Store.Where(x => x.Id == obj.ReceiverId).Select(x => x.CreatedById).SingleOrDefault();
                    StoreRoomChats chat = new StoreRoomChats();
                    StoreRoom room = new StoreRoom();
                    room.GuestId = obj.GuestId;
                    room.ReceiverId = ReceiverId;
                    room.CreatedDate = DateTime.Now;
                    _context.StoreRooms.Add(room);
                    _context.SaveChanges();
                    var roomId = room.Id;
                    chat.CreaterId = obj.GuestId;
                    chat.Comment = obj.Comment;
                    chat.StoreRoomId = roomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.StoreRoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.StoreRoomChats.Where(x => x.StoreRoomId == obj.RoomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.GuestId = obj.GuestId;
                    return Json(cart);
                }
                if (obj.Token == "null" || obj.GuestId == null)
                {
                    var ReceiverId = _context.Store.Where(x => x.Id == obj.ReceiverId).Select(x => x.CreatedById).SingleOrDefault();

                    var random = new Random();
                    int GuestId = random.Next(100, 999);
                    StoreRoomChats chat = new StoreRoomChats();
                    StoreRoom room = new StoreRoom();
                    room.GuestId = GuestId;
                    room.ReceiverId = ReceiverId;
                    room.CreatedDate = DateTime.Now;
                    _context.StoreRooms.Add(room);
                    _context.SaveChanges();
                    var roomId = room.Id;
                    chat.CreaterId = GuestId;
                    chat.Comment = obj.Comment;
                    chat.StoreRoomId = roomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.StoreRoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.StoreRoomChats.Where(x => x.StoreRoomId == roomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.GuestId = GuestId;
                    return Json(cart);
                }
                else
                {
                    var ReceiverId = _context.Store.Where(x => x.Id == obj.ReceiverId).Select(x => x.CreatedById).SingleOrDefault();

                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    StoreRoomChats chat = new StoreRoomChats();
                    StoreRoom room = new StoreRoom();
                    room.SenderId = userId;
                    room.ReceiverId = ReceiverId;
                    room.CreatedDate = DateTime.Now;
                    _context.StoreRooms.Add(room);
                    _context.SaveChanges();
                    var roomId = room.Id;
                    chat.CreaterId = userId;
                    chat.Comment = obj.Comment;
                    chat.StoreRoomId = roomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.StoreRoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.StoreRoomChats.Where(x => x.StoreRoomId == obj.RoomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.Id = userId;
                    return Json(cart);

                }
            }
            return Json(obj);
        }
        public JsonResult BuyerChatData(int id)
        {

            if (id != 0)
            {

                var roomId = _context.StoreRooms.Where(x => x.GuestId == id).Select(x => x.Id).SingleOrDefault();
                var messae = _context.StoreRoomChats.Where(x => x.StoreRoomId == roomId).Select(x => new RoomChats()
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    CreaterId = x.CreaterId,
                    CreatedDate = x.CreatedDate,
                }).ToList();
                CartView cart = new CartView();
                cart.RoomChat = messae;
                cart.Id = id;
                cart.RoomId = roomId;
                return Json(cart);

            }
            else
            {
                var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                var roomId = _context.StoreRooms.Where(x => x.SenderId == userId).Select(x => x.Id).SingleOrDefault();
                var messae = _context.StoreRoomChats.Where(x => x.StoreRoomId == roomId).Select(x => new RoomChats()
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    CreaterId = x.CreaterId,
                    CreatedDate = x.CreatedDate,
                }).ToList();
                CartView cart = new CartView();
                cart.RoomChat = messae;
                cart.Id = userId;
                cart.RoomId = roomId;
                return Json(cart);
            }
        }
        public IActionResult BuyerChats()
        {
            return View();
        }
        [Authorize]
        public JsonResult BuyerChatsData()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var Admin = _context.Users.Where(u => u.Id == userId).Select(u => new User()
            {

                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Image = u.Image,
            }).SingleOrDefault();
            var data = _context.StoreRooms.Where(x => x.SenderId == userId).ToList();
            var users = data.Where(x => x.SenderId != null)
                .Select(x => new CartView()
                {
                    SenderId = x.ReceiverId,
                    Id = x.Id,
                    User = _context.Users.Where(u => u.Id == x.ReceiverId).Select(u => new User()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Image = u.Image,

                    }).SingleOrDefault(),
                    //  Comment = _context.RoomChats.Where(r => r.RoomId == x.Id).OrderByDescending(x => x.CreatedDate).Select(r => r.Comment).SingleOrDefault(),
                }).ToList();

            ChatTableView cart = new ChatTableView();
            cart.UserRoom = users;
            cart.User = Admin;
            return Json(cart);
        }
        [Authorize]
        public JsonResult StoreMessage(int id)
        {

            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var data = _context.StoreRooms.Where(x => x.Id == id).Select(x => new ChatTableView()
            {
                SenderId = (int)x.ReceiverId,
                User = _context.Users.Where(u => u.Id == x.ReceiverId).Select(u => new User()
                {

                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Image = u.Image,
                }).SingleOrDefault(),
                Chat = _context.StoreRoomChats.Where(c => c.StoreRoomId == id).Select(c => new RoomChats()
                {
                    Id = c.Id,
                    Comment = c.Comment,
                    CreaterId = c.CreaterId,
                    CreatedDate = c.CreatedDate,
                }).ToList(),
            }).SingleOrDefault();
            CartView cart = new CartView();
            cart.ChatsData = data;
            cart.Id = userId;
            return Json(cart);

        }
        [Authorize]
        public JsonResult UserSendNewMessage(RoomChats obj)
        {

            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            StoreRoomChats chat = new StoreRoomChats();
            chat.CreaterId = userId;
            chat.Comment = obj.Comment;
            chat.StoreRoomId = obj.RoomId;
            chat.CreatedDate = DateTime.Now;
            _context.StoreRoomChats.Add(chat);
            _context.SaveChanges();
            var data = _context.StoreRoomChats.Where(x => x.StoreRoomId == obj.RoomId).Select(x => new RoomChats()
            {
                Id = x.Id,
                Comment = x.Comment,
                CreaterId = x.CreaterId,
                CreatedDate = x.CreatedDate,
            }).ToList();
            CartView cart = new CartView();
            cart.RoomChat = data;
            cart.Id = userId;
            return Json(cart);
        }
        public IActionResult HeplCenter()
        {
            return View();
        }
        public JsonResult AdminChat(int id)
        {
            if (id != 0)
            {

                var roomId = _context.Rooms.Where(x => x.GuestId == id).Select(x => x.Id).SingleOrDefault();
                var messae = _context.RoomChats.Where(x => x.RoomId == roomId).Select(x => new RoomChats()
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    CreaterId = x.CreaterId,
                    CreatedDate = x.CreatedDate,
                }).ToList();
                CartView cart = new CartView();
                cart.RoomChat = messae;
                cart.Id = id;
                cart.RoomId = roomId;
                return Json(cart);

            }
            else
            {
                var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                var roomId = _context.Rooms.Where(x => x.SenderId == userId).Select(x => x.Id).SingleOrDefault();
                var messae = _context.RoomChats.Where(x => x.RoomId == roomId).Select(x => new RoomChats()
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    CreaterId = x.CreaterId,
                    CreatedDate = x.CreatedDate,
                }).ToList();
                CartView cart = new CartView();
                cart.RoomChat = messae;
                cart.Id = userId;
                cart.RoomId = roomId;
                return Json(cart);
            }
        }
        public JsonResult NewMessagetoAdmin(ChatTableView obj)
        {
            if (obj.RoomId != 0)
            {
                if (obj.GuestId != 0)
                {
                    RoomChats chat = new RoomChats();
                    chat.CreaterId = obj.GuestId;
                    chat.Comment = obj.Comment;
                    chat.RoomId = obj.RoomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.RoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.RoomChats.Where(x => x.RoomId == obj.RoomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.GuestId = obj.GuestId;
                    return Json(cart);
                }
                else
                {
                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    RoomChats chat = new RoomChats();
                    chat.CreaterId = userId;
                    chat.Comment = obj.Comment;
                    chat.RoomId = obj.RoomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.RoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.RoomChats.Where(x => x.RoomId == obj.RoomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.Id = userId;
                    return Json(cart);
                }
            }
            else
            {
                if (obj.Token == "null")
                {
                    var random = new Random();
                    int GuestId = random.Next(100, 999);
                    RoomChats chat = new RoomChats();
                    Room room = new Room();
                    room.GuestId = GuestId;
                    room.CreatedDate = DateTime.Now;
                    _context.Rooms.Add(room);
                    _context.SaveChanges();
                    var roomId = room.Id;
                    chat.CreaterId = GuestId;
                    chat.Comment = obj.Comment;
                    chat.RoomId = roomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.RoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.RoomChats.Where(x => x.RoomId == roomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.GuestId = GuestId;
                    return Json(cart);
                }
                else
                {
                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    RoomChats chat = new RoomChats();
                    Room room = new Room();
                    room.SenderId = userId;
                    room.CreatedDate = DateTime.Now;
                    _context.Rooms.Add(room);
                    _context.SaveChanges();
                    var roomId = room.Id;
                    chat.CreaterId = userId;
                    chat.Comment = obj.Comment;
                    chat.RoomId = roomId;
                    chat.CreatedDate = DateTime.Now;
                    _context.RoomChats.Add(chat);
                    _context.SaveChanges();
                    var messae = _context.RoomChats.Where(x => x.RoomId == obj.RoomId).Select(x => new RoomChats()
                    {
                        Id = x.Id,
                        Comment = x.Comment,
                        CreaterId = x.CreaterId,
                        CreatedDate = x.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = messae;
                    cart.Id = userId;
                    return Json(cart);

                }
            }
            return Json(obj);
        }
        [Authorize]
        public JsonResult Return(int id)
        {
            var data = _context.OderItems.Where(x => x.Id == id).Select(x => new ProductViewResponce()
            {
                ProductId = x.Product.Id,
                Name = x.Product.Name,
                Image = x.Product.Image,
            }).SingleOrDefault();
            return Json(data);
        }
        [Authorize]
        public JsonResult ReturnData(ReturnProduct obj)
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var StoreId = _context.OderItems.Where(x => x.Id == obj.OderItemId).Select(x => x.Invoice.StoreId).FirstOrDefault();
            ReturnProduct returnProduct = new ReturnProduct();
            returnProduct.Reson = obj.Reson;
            returnProduct.Quantity = obj.Quantity;
            returnProduct.UserId = userId;
            returnProduct.OderItemId = obj.OderItemId;
            returnProduct.StoreId = StoreId;
            returnProduct.DateTime = DateTime.Now;
            _context.ReturnProducts.Add(returnProduct);
            _context.SaveChanges();

            return Json(returnProduct);
        }
        public IActionResult StoreView()
        {
            return View();
        }
        public JsonResult StoreViewData(int id)
        {
            return Json(id);
        }
    }
}
