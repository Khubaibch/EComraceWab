
using EComraceWab.Entites;
using EComraceWab.Helpers;
using EComraceWab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;

namespace EComraceWab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DarazContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContext;


        public HomeController(ILogger<HomeController> logger, DarazContext context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
            _httpContext = httpContext;
            string Basepath = _hostEnvironment.WebRootPath;
            string ContentPath = _hostEnvironment.ContentRootPath;
        }
        public IActionResult MessageCenter()
        {
            return View();
        }
        [Authorize]
        public JsonResult MessageCenterData()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var Admin = _context.Users.Where(u => u.Id == userId).Select(u => new User()
            {

                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Image = u.Image,
            }).SingleOrDefault();
            var data = _context.StoreRooms.Where(x => x.ReceiverId == userId).ToList();
            var users = data.Where(x => x.SenderId != null)
                .Select(x => new CartView()
                {
                    SenderId = (int)x.SenderId,
                    Id = x.Id,
                    User = _context.Users.Where(u => u.Id == x.SenderId).Select(u => new User()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Image = u.Image,

                    }).SingleOrDefault(),
                    StoreRoomChats = _context.StoreRoomChats.Where(r =>r.StoreRoomId == x.Id && r.Check == false).ToList(),
                }).ToList();
            var Guests = data.Where(x => x.GuestId != null).Select(x => new CartView()
            {
                GuestId = (int)x.GuestId,
                Id = x.Id,
               
                StoreRoomChats = _context.StoreRoomChats.Where(r => r.StoreRoomId == x.Id && r.Check == false).ToList(),
            }).ToList();

            ChatTableView cart = new ChatTableView();
            cart.StoreGuestRoom = Guests;
            cart.UserRoom = users;
            cart.User = Admin;
            return Json(cart);
        }
        [Authorize]
        public JsonResult ReturnsData(int id)
        {
            var data = _context.ReturnProducts.Where(x => x.StoreId == id).ToList();
            var all = data.Where(f => f.Check == false).ToList();
            return Json(all);
        }
        [Authorize]
        public JsonResult AddToStock(int id)
        {
            var data = _context.ReturnProducts.Where(x => x.Id == id).Select(x => new ReturnProduct()
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Reson = x.Reson,
                Check =x.Check,
                Add = x.Add,
                OderItemId = x.OderItemId,
                User = new User() { FirstName = x.User.FirstName, },
                OderItem = new OderItems()
                {
                    Id = x.OderItem.Id,
                    InvoiceId = x.OderItem.InvoiceId,
                    Product = new Product()
                    {
                        Id = x.OderItem.Product.Id,
                        Name = x.OderItem.Product.Name,
                        Image = x.OderItem.Product.Image,
                    },
                    VariactionId = x.OderItem.VariactionId,
                },
            }).SingleOrDefault();
        //    var product = _context.ProductsVariactions.Where(x => x.ProductId == data.OderItem.VariactionId).Select(x =>x.Id).FirstOrDefault();
            Inventry inventry = _context.Inventries.Where(x => x.VariactionId == data.OderItem.VariactionId).FirstOrDefault();
            var quantity = inventry.Quantity + data.Quantity;
            inventry.Quantity = quantity;
            _context.Inventries.Update(inventry);
            _context.SaveChanges();
            ReturnProduct @return = _context.ReturnProducts.Where(x => x.Id == id).FirstOrDefault();
            @return.Add = true;
            _context.ReturnProducts.Update(@return);
            _context.SaveChanges();
           
            return Json(data);
        }
        [Authorize]
        public JsonResult Collect(int id)
        {
            var data = _context.ReturnProducts.Where(x => x.Id == id).Select(x => new ReturnProduct()
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Reson = x.Reson,
                Check = x.Check,
                Add = x.Add,
                OderItemId = x.OderItemId,
                User = new User() { FirstName = x.User.FirstName, },
                OderItem = new OderItems()
                {
                    Id = x.OderItem.Id,
                    InvoiceId = x.OderItem.InvoiceId,
                    Product = new Product()
                    {
                        Id = x.OderItem.Product.Id,
                        Name = x.OderItem.Product.Name,
                        Image = x.OderItem.Product.Image,
                    },
                    VariactionId = x.OderItem.VariactionId,
                },
            }).SingleOrDefault();
            ReturnProduct @return = _context.ReturnProducts.Where(x => x.Id == id).FirstOrDefault();
            @return.Check = true;
            _context.ReturnProducts.Update(@return);
            _context.SaveChanges();
            var OderItems_Quantity = _context.OderItems.Where(x => x.Id == data.OderItemId).Select(x => x.Quantity).SingleOrDefault();
            OderItems items = _context.OderItems.Where(x => x.Id == data.OderItemId).FirstOrDefault();
            var Quantity = OderItems_Quantity - data.Quantity;
            if (Quantity == 0)
            {
                _context.OderItems.Remove(items);
                _context.SaveChanges();
                return Json(data);
            }
            else
            {
                items.Quantity = Quantity;
                _context.OderItems.Update(items);
                _context.SaveChanges();
                return Json(data);
            }
          
        } 
        public IActionResult ReturnView()
        {
            //var data = _context.ReturnProducts.Where(x => x.StoreId == id).ToList();
            //var all = data.Where(f => f.Check == false).ToList();
            return View();
        }
        [Authorize]
        public JsonResult ReturnAllData(int id)
        {
          
            var data = _context.ReturnProducts.Where(x => x.StoreId == id).Select(x => new ReturnProduct()
            {
                Id = x.Id,
                Quantity = x.Quantity,
                Reson = x.Reson,
                Check = x.Check,
                Add = x.Add,
                User = new User() { FirstName = x.User.FirstName, },
                OderItem = new OderItems()
                {
                    Id = x.OderItem.Id,
                    InvoiceId = x.OderItem.InvoiceId,
                    Product = new Product()
                    {
                        Id = x.OderItem.Product.Id,
                        Name = x.OderItem.Product.Name,
                        Image = x.OderItem.Product.Image,
                    },
                },
            }).ToList();
            return Json(data);
        }

        [Authorize]
        public JsonResult UserMessage(int id)
        {
            
                var GuestId = _context.StoreRooms.Where(x => x.Id == id).Select(x => x.GuestId).SingleOrDefault();
                if (GuestId != null)
                {
                    var data = _context.StoreRoomChats.Where(c => c.StoreRoomId == id).Select(c => new RoomChats()
                    {
                        Id = c.Id,
                        Comment = c.Comment,
                        CreaterId = c.CreaterId,
                        CreatedDate = c.CreatedDate,
                    }).ToList();
                CartView cart = new CartView();
                foreach (var oneby in data)
                {
                    StoreRoomChats chats = _context.StoreRoomChats.Where(x => x.Id == oneby.Id).FirstOrDefault();
                    chats.Check = true;
                    _context.StoreRoomChats.Update(chats);
                    _context.SaveChanges();
                }
                cart.RoomChat = data;
                    cart.GuestId = (int)GuestId;
                    return Json(cart);
                }
                else
                {
                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    var data = _context.StoreRooms.Where(x => x.Id == id).Select(x => new ChatTableView()
                    {  SenderId = (int)x.SenderId,
                        User = _context.Users.Where(u => u.Id == x.SenderId).Select(u => new User()
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
                foreach(var oneby in data.Chat)
                {
                    StoreRoomChats chats =_context.StoreRoomChats.Where(x => x.Id == oneby.Id).FirstOrDefault();
                    chats.Check = true;
                    _context.StoreRoomChats.Update(chats);
                    _context.SaveChanges();
                }
                CartView cart = new CartView();
                cart.ChatsData = data;
                cart.Id = userId;
                return Json(cart);
            }
        
        }
        [Authorize]
        public JsonResult StoreNewMessage(RoomChats obj)
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
        public IActionResult SallerHomePage()
            {
                return View();
            }
            [Authorize]
            public JsonResult SallerHomeData(int id)
            {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var Storedata = _context.Invoice.Where(i => i.StoreId == id).ToList();
                AdmainModel Data = new AdmainModel();
                var Pending = Storedata.Where(x => (x.StatusId == 1)).ToList();
                var Shipped = Storedata.Where(x => (x.StatusId == 2)).ToList();
                var Waiting = Storedata.Where(x => (x.StatusId == 3)).ToList();
                var Received = Storedata.Where(x => (x.StatusId == 4)).ToList();
                Data.Pending = Pending;
                Data.Received = Received;
                Data.Waiting = Waiting;
                Data.Shipping = Shipped;
            var amount = _context.Invoice.Where(x => x.StoreId == id && x.StatusId == 4).Select(x => new Oders()
            {
                Id = x.Id,
                Items = _context.OderItems.Where(i => i.InvoiceId == x.Id).Select(i => new Items()
                {
                    ProductId = i.ProductId,
                    Price = i.Price,
                    Quantity = i.Quantity,

                }).ToList(),
            }).ToList();
            var Admin = _context.Users.Where(u => u.Id == userId).Select(u => new User()
            {

                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Image = u.Image,
            }).SingleOrDefault();
            var message = _context.StoreRoomChats.Where(x => x.Check == false).ToList();
            var totalCommission = 0;
            foreach (var data in amount)
            {
                foreach (var Commission in data.Items)
                {
                    totalCommission += Commission.Price;
                }
            }
            Data.Commission = totalCommission;
            Data.StoreRoomChats = message;
            Data.Admain = Admin;
            return Json(Data);
            }
            public JsonResult Role()
            {
                var Role = _context.Roles.ToList();
                var data = Role.Skip(1);
                var dataa = data.SkipLast(1);

                return Json(dataa);
            }
            [Authorize]
            public JsonResult ShippingStatuss()
            {
                var directions = from ShippingStatus d in Enum.GetValues(typeof(ShippingStatus))
                                 select new { Id = (int)d, Name = d.ToString() };
                return Json(directions);
            }

            public IActionResult History()
            {
                return View();
            }
            [Authorize]
            public JsonResult SelleHistory(InvoiceModel obj)
            {
                var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                var Storedata = _context.Invoice.Where(i => i.StoreId == obj.StoreId).ToList();
                AdmainModel Data = new AdmainModel();
                var Pending = Storedata.Where(x => (x.StatusId == obj.StatusId)).Select(x => new InvoiceModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Address = x.Address,
                    StoreId = x.StoreId,
                    City = x.City,
                    DateTime = x.DateTime,
                    Status = ((ShippingStatus)x.StatusId).ToString(),

                    Oders = _context.OderItems.Where(o => o.InvoiceId == x.Id).Select(o => new Oders()
                    {
                        ProductId = o.ProductId,
                        VariactionId = o.VariactionId,
                        Product = _context.Products.Where(y => y.Id == o.ProductId).Select(y => new ProductViewResponce()
                        {
                            Name = y.Name,
                            Description = y.Description,
                            Image = y.Image,
                            Variaction = _context.ProductsVariactions.Where(v => v.Id == o.VariactionId).Select(
                                    v => new ProductsVariactionView()
                                    {
                                        Id = v.Id,
                                        Variaction = v.Variaction,
                                        Inventrys = _context.Inventries.Where(i => i.VariactionId == v.Id).Select(i => new InventryView()
                                        {
                                            Id = i.Id,
                                            Price = i.Price,
                                            Quantity = o.Quantity,
                                        }).SingleOrDefault(),
                                    }).SingleOrDefault(),
                        }).ToList(),
                    }).ToList(),

                }).ToList();

                return Json(Pending);
            }

            public IActionResult Product()
            {
                return View();
            }
            [HttpGet]
            [Authorize]
            public JsonResult ProductList()
            {
                var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                // var SallerId = _context.Stores.Where(x => x.CreatedById == userId).Select(x => new CartView()

                var Products = _context.Products.Where(s => s.CreatedById == userId).Select(a => new ProductViewResponce()
                {

                    Id = a.Id,
                    Name = a.Name,
                    Image = a.Image,
                    Description = a.Description,
                    CreatedDate = a.CreatedDate,
                    SubCategoryId = a.SubCategoryId,
                    CreatedById = a.CreatedById,
                    // ModifiedById = a.ModifiedById,
                    // ModifiedDate =x.ModifiedDate,
                    CreatedBy = _context.Users.Where(x => x.Id == a.CreatedById).Select(x => new User() { FirstName = x.FirstName }).SingleOrDefault(),
                    // ModifiedBy = _context.Users.Where(x => x.Id == a.ModifiedById).Select(x => new User() { FirstName = x.FirstName }).SingleOrDefault(),
                    SubCategory = _context.SubCategories.Where(x => x.Id == a.SubCategoryId).Select(x => new SubCategory() { Name = x.Name }).SingleOrDefault(),
                }).ToList();
                return Json(Products);
            }

            [HttpPost]
            [Authorize]
            public JsonResult AddProduct(ProductView obj)
            {
                var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                var StoreId = _context.Store.Where(x => x.CreatedById == userId).Select(s => s.Id).SingleOrDefault();
                Product model = new Product();

                model.Name = obj.Name;
                model.StoreId = StoreId;
                model.CreatedById = userId;
                model.CreatedDate = DateTime.Now;
                model.Description = obj.Description;
                model.SubCategoryId = obj.SubCategoryId;
                var path = _hostEnvironment.WebRootPath;
                var filepath = "/images/" + obj.Image.FileName;
                var fullpath = Path.Combine(path + filepath);
                UplodImage(obj.Image, fullpath);
                model.Image = filepath;
                _context.Products.Add(model);
                _context.SaveChanges();
                var ProductId = model.Id;
                if (obj.Warranty != null)
                {
                    WarrantyInfo warranty = new WarrantyInfo();
                    warranty.ProductId = ProductId;
                    warranty.Warranty = obj.Warranty;
                    _context.WarrantyInfo.Add(warranty);
                    _context.SaveChanges();
                }
                if (obj.Specification != null)
                {
                    Specifications specifications = new Specifications();
                    specifications.ProductId = ProductId;
                    specifications.Specification = obj.Specification;
                    _context.Specifications.Add(specifications);
                    _context.SaveChanges();
                }
                if (obj.Images != null)
                {
                    foreach (var image in obj.Images)
                    {
                        ProductImagesDetail productImagesDetail = new ProductImagesDetail();

                        var pathe = _hostEnvironment.WebRootPath;
                        var filepathe = "/images/" + image.FileName;
                        var fullpathe = Path.Combine(pathe + filepathe);
                        UplodImages(image, fullpathe);
                        productImagesDetail.image = filepathe;
                        productImagesDetail.ProductId = ProductId;
                        _context.ProductImagesDetail.Add(productImagesDetail);
                        _context.SaveChanges();
                    }
                }

                if (obj.Type != null)
                {
                    ProductType productType = new ProductType();

                    productType.ProductId = ProductId;
                    productType.Type = obj.Type;
                    _context.ProductTypes.Add(productType);
                    _context.SaveChanges();
                }
                if (obj.Material != null)
                {
                    ProductMaterial productMaterial = new ProductMaterial();

                    productMaterial.ProductId = ProductId;
                    productMaterial.Material = obj.Material;
                    _context.ProductMaterials.Add(productMaterial);
                    _context.SaveChanges();
                }
                var data = _context.Brands.Where(b => b.Brand == obj.Brand).Select(b => b.Id).FirstOrDefault();
                if (data != 0)
                {
                    ProductBrand productBrand = new ProductBrand();
                    productBrand.BrandId = data;
                    productBrand.ProductId = ProductId;
                    _context.ProductBrands.Add(productBrand);
                    _context.SaveChanges();
                }
                if (data == 0)
                {
                    Brands Brand = new Brands();

                    // productBrand.ProductId = aa;
                    Brand.Brand = obj.Brand;
                    _context.Brands.Add(Brand);
                    _context.SaveChanges();
                    var BrandId = Brand.Id;
                    ProductBrand productBrand = new ProductBrand();
                    productBrand.BrandId = BrandId;
                    productBrand.ProductId = ProductId;
                    _context.ProductBrands.Add(productBrand);
                    _context.SaveChanges();


                }
                if (obj.Variactions != null)
                {
                    foreach (var Variactions in obj.Variactions)
                    {
                        ProductsVariaction productsVariaction = new ProductsVariaction();

                        productsVariaction.Variaction = Variactions;
                        productsVariaction.ProductId = obj.Id;
                        _context.ProductsVariactions.Add(productsVariaction);
                        _context.SaveChanges();
                        var Id = productsVariaction.Id;
                        Inventry inventry = new Inventry();
                        inventry.Price = obj.Price;
                        inventry.Quantity = obj.Quantity;
                        inventry.VariactionId = Id;
                        _context.Inventries.Add(inventry);
                        _context.SaveChanges();
                    }
                }

                return Json(obj);
            }
            public void UplodImage(IFormFile file, string path)
            {
                using FileStream stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                stream.Dispose();
            }
            public void UplodImages(IFormFile file, string pathe)
            {
                using FileStream stream = new FileStream(pathe, FileMode.Create);
                file.CopyTo(stream);
                stream.Dispose();
            }

            [Authorize]
            [HttpGet]
            public JsonResult EditProduct(int id)
            {

                ProductDetails details = new ProductDetails();
                var data = _context.Products.Where(x => x.Id == id).Select(s => new ProductViewResponce() { Id = s.Id, Name = s.Name, Image = s.Image, Description = s.Description, CreatedById = s.CreatedById }).FirstOrDefault();
                // var SizeDetail = _context.ProductSizesDetail.Where(s => s.ProductId == id).Select(x => x.SizesId).ToList();
                // var size = _context.Sizes.Where(x => SizeDetail.Contains(x.Id)).Select(x => new Sizes() { Size =x.Size}).ToList();
                var Image = _context.ProductImagesDetail.Where(s => s.ProductId == id).Select(x => x.image).ToList();
                //     var Imagev = _context.ProductImagesDetail.Where(x => Image.Contains(x.image)).ToList();
                //  var ColorDetail = _context.ProductColoresDetail.Where(s => s.ProductId == id).Select(x => x.ColorsId).ToList();
                //  var color = _context.Colores.Where(x => ColorDetail.Contains(x.Id)).Select(x => new Colors() { Color = x.Color }).ToList();
                var Brand = _context.ProductBrands.Where(s => s.ProductId == id).Select(x => new ProductBrand() { Id = x.Id, Brand = x.Brand }).SingleOrDefault();
                var Type = _context.ProductTypes.Where(s => s.ProductId == id).Select(x => new ProductType() { Id = x.Id, Type = x.Type }).SingleOrDefault();
                var Materiale = _context.ProductMaterials.Where(s => s.ProductId == id).Select(x => new ProductMaterial() { Id = x.Id, Material = x.Material }).SingleOrDefault();
                var cat = _context.Products.Where(s => s.Id == id).Select(x => x.SubCategoryId).FirstOrDefault();
                var Subcat = _context.SubCategories.Where(c => c.Id == cat).Select(c => new SubCategory() { Name = c.Name }).SingleOrDefault();
                var Specification = _context.Specifications.Where(x => x.ProductId == id).Select(x => x.Specification).SingleOrDefault();

                var Warranty = _context.WarrantyInfo.Where(x => x.ProductId == id).Select(x => x.Warranty).SingleOrDefault();
                details.Products = data;
                //  details.Sizes = size;
                details.Images = Image;
                details.Specification = Specification;
                details.Warranty = Warranty;
                // details.Colors = color;
                details.ProductBrand = Brand;
                details.ProductMaterial = Materiale;
                details.ProductType = Type;
                details.subCategory = Subcat;


                return Json(details);
            }
            [Authorize]
            [HttpPost]
            public JsonResult Update(ProductView obj, int id)
            {

                Product model = _context.Products.FirstOrDefault(r => r.Id == id);
                model.Id = obj.Id;
                model.Name = obj.Name;
                model.Description = obj.Description;
                model.SubCategoryId = obj.SubCategoryId;

                model.ModifiedDate = DateTime.Now;
                var path = _hostEnvironment.WebRootPath;
                var filepath = "/images/" + obj.Image.FileName;
                var fullpath = Path.Combine(path + filepath);
                UplodImage(obj.Image, fullpath);
                model.Image = filepath;
                _context.Products.Update(model);
                _context.SaveChanges();
                ProductType type = _context.ProductTypes.FirstOrDefault(x => x.Id == obj.TypeId);
                type.Type = obj.Type;
                _context.ProductTypes.Update(type);
                _context.SaveChanges();
                ProductMaterial material = _context.ProductMaterials.FirstOrDefault(x => x.Id == obj.MaterialId);
                material.Material = obj.Material;
                _context.ProductMaterials.Update(material);
                _context.SaveChanges();
                var Ids = _context.ProductImagesDetail.Where(x => x.ProductId == id).Select(x => x.Id).ToList();
                foreach (var imagesId in Ids)
                {
                    ProductImagesDetail productImagesId = new ProductImagesDetail();
                    productImagesId.Id = imagesId;
                    _context.ProductImagesDetail.Remove(productImagesId);
                    _context.SaveChanges();
                }
                foreach (var image in obj.Images)
                {
                    ProductImagesDetail productImagesDetail = new ProductImagesDetail();

                    var pathe = _hostEnvironment.WebRootPath;
                    var filepathe = "/images/" + image.FileName;
                    var fullpathe = Path.Combine(pathe + filepathe);
                    UplodImages(image, fullpathe);
                    productImagesDetail.image = filepathe;
                    productImagesDetail.ProductId = obj.Id;
                    _context.ProductImagesDetail.Add(productImagesDetail);
                    _context.SaveChanges();
                }
                //if (obj.Variactions != null)
                //{
                //    foreach (var Variactions in obj.Variactions)
                //    {
                //            ProductsVariaction productsVariaction = new ProductsVariaction();

                //            productsVariaction.Variaction = Variactions;
                //            productsVariaction.ProductId = obj.Id;
                //            _context.ProductsVariactions.Add(productsVariaction);
                //            _context.SaveChanges();
                //        var Id = productsVariaction.Id;
                //        Inventry inventry = new Inventry();
                //        inventry.Price = obj.Price;
                //        inventry.Quantity = obj.Quantity;
                //        inventry.VariactionId = Id;
                //        _context.Inventries.Add(inventry);
                //        _context.SaveChanges();
                //    }
                //}
                return Json(obj);
            }
            [Authorize]
            [HttpGet]
            public JsonResult DeleteProduct(int id)
            {
                var data = _context.Products.Where(x => x.Id == id).SingleOrDefault();
                _context.Products.Remove(data);
                _context.SaveChanges();

                return new JsonResult("Data Deleted ");

            }

            [Authorize]
            public JsonResult GetUser()
            {
                var UserId = (_httpContext.HttpContext.Items["User"] as User).Id;
                var data = _context.Users.Where(x => x.Id == UserId).Select(x => new User()
                {
                    Email = x.Email,
                    FirstName = x.FirstName,
                    Image = x.Image,
                    LastName = x.LastName,

                }).FirstOrDefault();
                return Json(data);

            }
            public JsonResult StatusShipping(Invoice obj)
            {
                Invoice data = _context.Invoice.FirstOrDefault(x => x.Id == obj.Id);
                data.StatusId = obj.StatusId;
                _context.Invoice.Update(data);
                _context.SaveChanges();
                return Json(data);
            }
            public JsonResult SearchBox(string Name)
            {
                var data = _context.Products.Where(x => x.Name.Contains(Name)).Select(x => new ProductViewResponce()
                {
                    Name = x.Name,
                    Id = x.Id,
                }).ToList();
                return Json(data);
            }
            public JsonResult ForgotPassword(User obj)
            {
                var data = _context.Users.Where(x => x.Email == obj.Email).Select(x => x.Id).FirstOrDefault();

                return Json(data);
            }
            public JsonResult ChangePassword(User obj)
            {
                User data = _context.Users.Where(x => x.Id == obj.Id).FirstOrDefault();
                data.ConfirmPassword = obj.ConfirmPassword;
                data.Password = obj.Password;
                _context.Users.Update(data);
                _context.SaveChanges();
                return Json(data);
            }
            public JsonResult VariactionsList(int id)
            {
                var data = _context.ProductsVariactions.Where(x => x.ProductId == id).ToList();

                return Json(data);
            }
            [HttpPost]
            public JsonResult AddToInventry(ProductView obj)
            {
                foreach (var Variactions in obj.Variactions)
                {
                    {
                        ProductsVariaction productsVariaction = new ProductsVariaction();


                        productsVariaction.Variaction = Variactions;
                        productsVariaction.ProductId = obj.Id;
                        _context.ProductsVariactions.Add(productsVariaction);
                        _context.SaveChanges();
                        var id = productsVariaction.Id;
                        Inventry inventry = new Inventry();
                        inventry.Price = obj.Price;
                        inventry.Quantity = obj.Quantity;
                        inventry.VariactionId = id;
                        _context.Inventries.Add(inventry);
                        _context.SaveChanges();
                    }

                }
                return Json(obj);
            }
        }


    }
