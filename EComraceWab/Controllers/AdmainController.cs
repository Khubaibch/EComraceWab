using EComraceWab.Entites;
using EComraceWab.Helpers;
using EComraceWab.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EComraceWab.Controllers
{
    public class AdmainController : Controller
    {
        private readonly ILogger<AdmainController> _logger;
        private readonly DarazContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContext;


        public AdmainController(ILogger<AdmainController> logger, DarazContext context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
            _httpContext = httpContext;
            string Basepath = _hostEnvironment.WebRootPath;
            string ContentPath = _hostEnvironment.ContentRootPath;
        }
        public IActionResult Wellcome()
        {
            return View();
        }
        [Authorize]
        public JsonResult WellcomeData()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            AdmainModel Data =new AdmainModel();
            var Pending = 1;
            var Shipped = 2;
            var Waiting = 3;
            var Received = 4;
            var PendingData = _context.Invoice.Where(x => x.StatusId == Pending).ToList();
            var ShippedData = _context.Invoice.Where(x => x.StatusId == Shipped).ToList();
            var WaitingData = _context.Invoice.Where(x => x.StatusId == Waiting).ToList();
            var ReceivedData = _context.Invoice.Where(x => x.StatusId == Received).ToList();
            Data.Pending = PendingData;
            Data.Received = ReceivedData;
            Data.Waiting = WaitingData;
            Data.Shipping = ShippedData;
            var amount = _context.Invoice.Where(x => x.StatusId == Received).Select(x => new Oders()
            {
                Id = x.Id,
                Items = _context.OderItems.Where(i => i.InvoiceId == x.Id).Select(i => new Items()
                {
                    ProductId = i.ProductId,
                    Commission = i.Product.SubCategory.Commission,


                }).ToList(),
            }).ToList();
            var Admin = _context.Users.Where(u => u.Id == userId).Select(u => new User()
            {

                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Image = u.Image,
            }).SingleOrDefault();
            var message = _context.Rooms.Where(x => x.IsChecked == false).ToList();
            var totalCommission = 0;
            foreach(var data in amount)
            {
                foreach (var Commission in data.Items)
                {
                    totalCommission += Commission.Commission;
                }
            }
            Data.Commission = totalCommission;
            Data.Message = message;
            Data.Admain = Admin;
            return Json(Data);
        }
        [Authorize]
        public JsonResult GetId()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var id = _context.UserRole.Where(x => x.UserId == userId).Select(s => new UserRole()
            {
                RolesId = s.RolesId,
                UserId = s.UserId,
                Id = _context.Store.Where(x => x.CreatedById == s.UserId).Select(x => x.Id).SingleOrDefault(),
            }).SingleOrDefault();
            return Json(id);
        }
        public IActionResult AdmainMessageCenter()
        {
            return View();
        }
        [Authorize]
        public JsonResult AdmainMessageCenterData()
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var Admin = _context.Users.Where(u => u.Id == userId).Select(u => new User()
            {

                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Image = u.Image,
            }).SingleOrDefault();
            var data = _context.Rooms.ToList();
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
                  //  Comment = _context.RoomChats.Where(r => r.RoomId == x.Id).OrderByDescending(x => x.CreatedDate).Select(r => r.Comment).SingleOrDefault(),
                }).ToList();
            var Guests = data.Where(x => x.GuestId != null).ToList();

            ChatTableView cart = new ChatTableView();
            cart.GuestRoom = Guests;
            cart.UserRoom = users;
            cart.User = Admin;
            return Json(cart);
        }
        public JsonResult UserSendMessagetoAdmain(int id)
        {
            var Condition = _context.Rooms.Where(x => x.Id == id).Select(x => x.IsChecked).SingleOrDefault();
            if (Condition == false)
            {
                var GuestId = _context.Rooms.Where(x => x.Id == id).Select(x => x.GuestId).SingleOrDefault();
                if (GuestId != null)
                {
                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    var data = _context.RoomChats.Where(c => c.RoomId == id).Select(c => new RoomChats()
                    {
                        Id = c.Id,
                        Comment = c.Comment,
                        CreaterId = c.CreaterId,
                        CreatedDate = c.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = data;
                    cart.Id = userId;
                    cart.GuestId = (int)GuestId;
                    Room room = _context.Rooms.Where(x => x.Id == id).SingleOrDefault();
                    room.AdmainId = userId;
                    room.IsChecked = true;
                    _context.Rooms.Update(room);
                    _context.SaveChanges();
                    return Json(cart);
                }
                else
                {
                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    var data = _context.Rooms.Where(x => x.Id == id).Select(x => new ChatTableView()
                    {
                        SenderId = (int)x.SenderId,
                        User = _context.Users.Where(u => u.Id == x.SenderId).Select(u => new User()
                        {

                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Image = u.Image,
                        }).SingleOrDefault(),
                        Chat = _context.RoomChats.Where(c => c.RoomId == id).Select(c => new RoomChats()
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
                    Room room = _context.Rooms.Where(x => x.Id == id).SingleOrDefault();
                    room.AdmainId = userId;
                    room.IsChecked = true;
                    _context.Rooms.Update(room);
                    _context.SaveChanges();
                    return Json(cart);
                }
            }
            else
            {
                var GuestId = _context.Rooms.Where(x => x.Id == id).Select(x => x.GuestId).SingleOrDefault();
                if (GuestId != null)
                {
                var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    var data = _context.RoomChats.Where(c => c.RoomId == id).Select(c => new RoomChats()
                    {
                        Id = c.Id,
                        Comment = c.Comment,
                        CreaterId = c.CreaterId,
                        CreatedDate = c.CreatedDate,
                    }).ToList();
                    CartView cart = new CartView();
                    cart.RoomChat = data;
                    cart.Id = userId;
                    cart.GuestId = (int)GuestId;
                    return Json(cart);
                }
                else
                {
                    var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                    var data = _context.Rooms.Where(x => x.Id == id).Select(x => new ChatTableView()
                    {
                        SenderId = (int)x.SenderId,
                        User = _context.Users.Where(u => u.Id == x.SenderId).Select(u => new User()
                        {

                            Id = u.Id,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Image = u.Image,
                        }).SingleOrDefault(),
                        Chat = _context.RoomChats.Where(c => c.RoomId == id).Select(c => new RoomChats()
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
            }
        }
        [Authorize]
        public JsonResult AsinDataToAdmain(RoomChats obj)
        {
            var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
            var data = _context.Rooms.Where(x => x.Id == obj.RoomId).SingleOrDefault();
             data.AssingToId = obj.CreaterId;
            data.AdmainId = userId;
            _context.Rooms.Update(data);
            _context.SaveChanges();
            return Json(data);
        }
        [Authorize]
        public JsonResult StoreCheck(int id)
        {
            Store store = _context.Store.Where(x => x.Id == id).FirstOrDefault();
            if(store.Block == false)
            {
                store.Block = true;
                _context.Store.Update(store);
                _context.SaveChanges();
                return Json(store);
            }
            else
            {
                store.Block = false;
                _context.Store.Update(store);
                _context.SaveChanges();
                return Json(store);
            }
            return Json(store);
        }
        [Authorize]
        public JsonResult NewMessage(RoomChats obj)
        {
           
                var userId = (_httpContext.HttpContext.Items["User"] as User).Id;
                RoomChats chat = new RoomChats();
                chat.CreaterId = userId;
                chat.Comment = obj.Comment;
                chat.RoomId = obj.RoomId;
                chat.CreatedDate = DateTime.Now;
                _context.RoomChats.Add(chat);
                _context.SaveChanges();
                var data = _context.RoomChats.Where(x => x.RoomId == obj.RoomId).Select(x => new RoomChats()
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
        public IActionResult Sellers()
        {
            return View();
        }
        [Authorize]
        public JsonResult SellersData(int id)
        {

            var userdata = _context.UserRole.Where(x => x.RolesId == id).Select(x => new SelleHistory()
            {
               
                User = new UserAndStoreView()
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    store =_context.Store.Where(s => s.CreatedById == x.User.Id).Select(s => new StoreResponse()
                    {
                        Id = s.Id,
                        City = s.City,
                        Address = s.Address,
                        Number = s.Number,
                        Name = s.Name,
                        Block = s.Block,
                    }).SingleOrDefault(),
                }
                
                

            }).ToList();

            return Json(userdata);
        } public IActionResult Admains()
        {
            return View();
        }
        [Authorize]
        public JsonResult AdmainsData(int id)
        {

            var userdata = _context.UserRole.Where(x => x.RolesId == id).Select(x => new SelleHistory()
            {
               
                User = new UserAndStoreView()
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                }
                
                

            }).ToList();

            return Json(userdata);
        }  
        
        public JsonResult All_Admains(int id)
        {
            var Users = _context.UserRole.Where(x => x.RolesId == id).Select(x => new UserAndStoreView()
            {
                UserId = x.UserId,
                User =_context.Users.Where(u => u.Id == x.UserId).Select(u => new User()
                {
                    FirstName = u.FirstName,
                    Id = u.Id,
                }).SingleOrDefault(),
            }).ToList();
            return Json(Users);
        } 
        public IActionResult Buyers()
        {
            return View();
        }
        [Authorize]
        public JsonResult BuyersData(int id)
        {

            var userdata = _context.UserRole.Where(x => x.RolesId == id).Select(x => new SelleHistory()
            {
               
                User = new UserAndStoreView()
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                }
                
                

            }).ToList();

            return Json(userdata);
        }
        public IActionResult StoreItems()
        {
            return View();
        }
        [Authorize]
        public JsonResult StoreItemsData(int id)
        {
            var Products = _context.Products.Where(s => s.StoreId == id).Select(a => new ProductViewResponce()
            {

               // Quantity = a.Quantity,
                Id = a.Id,
               // Price = a.Price,
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
        public IActionResult StoreOders()
        {
            return View();
        }
        public JsonResult StoreOdersData(int id)
        {
            List<InvoiceModel> orderDetails = _context.Invoice.Where(x => x.Orders.Any(y => y.Product.CreatedById == id))
                    .Select(x => new InvoiceModel()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        Address = x.Address,
                        City = x.City,
                        DateTime = x.DateTime,
                        Status = ((ShippingStatus)x.StatusId).ToString(),
                        Products = x.Orders.Where(y => y.Product.CreatedById == id).Select(y => new ProductViewResponce()
                        {
                            Name = y.Product.Name,
                            Quantity = y.Quantity,
                            Description = y.Product.Description,
                            Image = y.Product.Image,
                           // Price = y.Product.Price,

                        }).ToList(),

                    }).ToList();
            return Json(orderDetails);
        }
        public IActionResult BuyerHistory()
        {
            return View();
        }
        [Authorize]
        public JsonResult BuyerHistoryData(int id)
        {
            var items = _context.Invoice.Where(x => x.UserId == id)
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
                         //   Price = y.Product.Price,
                            Description = y.Product.Description,
                            Image = y.Product.Image,
                            Quantity = y.Quantity,
                        }).ToList(),
                    }
                });
            return Json(items);
        }
        public IActionResult CatagoryList()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public JsonResult CatagoryData()
        {
            var c = from a in _context.Categories
                    join b in _context.Users
                    on a.CreatedById equals b.Id
                    into dep
                    from b in dep.DefaultIfEmpty()
                    join s in _context.Users
                     on a.ModifiedById equals s.Id
                     into dep2
                    from s in dep2.DefaultIfEmpty()
                    select new Category
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Image = a.Image,
                        Description = a.Description,
                        CreatedDate = a.CreatedDate,
                        ModifiedDate = a.ModifiedDate,
                        CreatedById = a.CreatedById,
                        CreatedBy = b,
                        ModifiedById = a.ModifiedById,
                        ModifiedBy = s,

                    };
            _context.Categories.ToList();
            return Json(c);
        }
        //[Authorize]
        //[HttpPost]
        //public JsonResult AddCategory(CategoryView obj)
        //{
        //    Category model = new Category();
        //    model.Name = obj.Name;
        //    model.CreatedById = (_httpContext.HttpContext.Items["User"] as User).Id;
        //    model.CreatedDate = DateTime.Now;
        //    model.Description = obj.Description;
        //    var path = _hostEnvironment.WebRootPath;
        //    var filepath = "/images/" + obj.Image.FileName;
        //    var fullpath = Path.Combine(path + filepath);
        //    UplodImagee(obj.Image, fullpath);
        //    model.Image = filepath;
        //    _context.Categories.Add(model);
        //    _context.SaveChanges();


        //    return Json(obj);
        //}

        //[Authorize]
        //public JsonResult EditCategory(int id)
        //{
        //    var data = _context.Categories.Where(x => x.Id == id).SingleOrDefault();
        //    return new JsonResult(data);
        //}

        [Authorize]
        [HttpPost]
        public JsonResult UpdateCategory(IFormFile Image, string Name, string Description, int id)
        {
            Category obj = _context.Categories.FirstOrDefault(r => r.Id == id);
            obj.ModifiedById = (_httpContext.HttpContext.Items["User"] as User).Id;
            obj.Name = Name;
            obj.Description = Description;
            obj.ModifiedDate = DateTime.Now;
            var path = _hostEnvironment.WebRootPath;
            var filepath = "/images/" + Image.FileName;
            var fullpath = Path.Combine(path + filepath);
            UplodImagee(Image, fullpath);
            obj.Image = filepath;
            _context.Categories.Update(obj);
            _context.SaveChanges();
            return new JsonResult("Updated recod!");

        }
        [Authorize]
        [HttpGet]
        public JsonResult DeleteCategory(int id)
        {
            var data = _context.Categories.Where(x => x.Id == id).SingleOrDefault();
            _context.Categories.Remove(data);
            _context.SaveChanges();

            return new JsonResult("Data Deleted ");

        }
        public IActionResult SubCategory()
        {
            return View("SubCategoryList");
        }
        [HttpGet]
        public JsonResult SubCategoryList()
        {
            var StdList = from a in _context.SubCategories
                          join b in _context.Categories
                          on a.CategoryId equals b.Id
                          into cat
                          from b in cat.DefaultIfEmpty()
                          join d in _context.Users
                          on a.CreatedById equals d.Id
                          into mod
                          from d in mod.DefaultIfEmpty()
                          join c in _context.Users
                          on a.ModifiedById equals c.Id
                          into mod2
                          from c in mod2.DefaultIfEmpty()
                          select new SubCategory
                          {
                              Id = a.Id,
                              Name = a.Name,
                              ModifiedDate = a.ModifiedDate,
                              CreatedDate = a.CreatedDate,
                              Image = a.Image,
                              Description = a.Description,
                              CategoryId = a.CategoryId,
                              Category = b,
                              CreatedById = a.CreatedById,
                              CreatedBy = d,
                              ModifiedById = a.ModifiedById,
                              ModifiedBy = c,
                          };

            _context.SubCategories.ToList();
            return Json(StdList.ToList());
        }
        [Authorize]
        [HttpPost]
        public JsonResult AddSubCategory(SubCategoryView obje)
        {
            if (!ModelState.IsValid)
            {
                SubCategory model1 = new SubCategory();
                model1.Name = obje.Name;
                model1.CreatedById = (_httpContext.HttpContext.Items["User"] as User).Id;
                model1.CreatedDate = DateTime.Now;
                model1.Description = obje.Description;
                model1.CategoryId = obje.CategoryId;
                var path = _hostEnvironment.WebRootPath;
                var filepath = "/images/" + obje.Image.FileName;
                var fullpath = Path.Combine(path + filepath);
                UplodImagee(obje.Image, fullpath);
                model1.Image = filepath;
                _context.SubCategories.Add(model1);
                _context.SaveChanges();
            }
            return Json(obje);
        }
        public void UplodImagee(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

        }
        [Authorize]
        [HttpGet]
        public JsonResult EditSubCategory(int id)
        {
            var data = _context.SubCategories.Where(x => x.Id == id).SingleOrDefault();
            return new JsonResult(data);
        }
        [Authorize]
        [HttpPost]
        public JsonResult UpdateSub(IFormFile Image, string Name, string Description, int CategoryId, int id)
        {

            SubCategory model = _context.SubCategories.FirstOrDefault(r => r.Id == id);
            model.Name = Name;
            model.Description = Description;
            model.CategoryId = CategoryId;
            model.ModifiedById = (_httpContext.HttpContext.Items["User"] as User).Id;
            model.ModifiedDate = DateTime.Now;
            var path = _hostEnvironment.WebRootPath;
            var filepath = "/images/" + Image.FileName;
            var fullpath = Path.Combine(path + filepath);
            UplodImagee(Image, fullpath);
            model.Image = filepath;
            _context.SubCategories.Update(model);
            _context.SaveChanges();
            return new JsonResult("Updated recod!");

        }
        [Authorize]
        [HttpGet]
        public JsonResult DeleteSubCategory(int id)
        {
            var data = _context.SubCategories.Where(x => x.Id == id).SingleOrDefault();
            _context.SubCategories.Remove(data);
            _context.SaveChanges();

            return new JsonResult("Data Deleted ");

        }
     
     
    }
}
