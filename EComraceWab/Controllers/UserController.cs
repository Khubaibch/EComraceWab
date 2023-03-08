
using EComraceWab.Entites;
using EComraceWab.Models;
using EComraceWab.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using Microsoft.Extensions.Hosting;

namespace EComraceWab.Controllers
{

    public class UserController : Controller
    {  
        private readonly DarazContext _context;
        private IUserService _userService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public UserController(IUserService userService, DarazContext context, IHttpContextAccessor httpContext, IWebHostEnvironment hostEnvironment)
        {
            _userService = userService;
            _context = context;
            _httpContext = httpContext;
            _hostEnvironment = hostEnvironment;
            string Basepath = _hostEnvironment.WebRootPath;
            string ContentPath = _hostEnvironment.ContentRootPath;
        }
        
        public IActionResult UserLogin()
        {
            
                return View("Login"); 
        }
        public IActionResult Login()
        {
            

            
                return View(); 
        }
      [HttpPost]
        public IActionResult Authenticate(AuthenticationRequist model)
        {
          //  GetUser(model.Email,model.Password);
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(response);
        }
        public IActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserAndStoreView obj)
        {
            
                User user = new User();
                user.FirstName = obj.FirstName;
                user.LastName = obj.LastName;
                user.Email = obj.Email;
                user.Password = obj.Password;
                user.ConfirmPassword=obj.ConfirmPassword;
            var path = _hostEnvironment.WebRootPath;
            var filepath = "/images/" + obj.Image.FileName;
            var fullpath = Path.Combine(path + filepath);
            UplodImage(obj.Image, fullpath);
            user.Image = filepath;
            _context.Users.Add(user);
            _context.SaveChanges();
            var Id = user.Id;
            UserRole userRole = new UserRole();
            userRole.UserId = Id;
            userRole.RolesId = obj.RoleId;
            _context.UserRole.Add(userRole);
            _context.SaveChanges();
            if (obj.Name != null)
            {
                Store store = new Store();
                store.Address = obj.Address;
                store.Number = obj.Number;
                store.Name = obj.Name;
                store.ShippingInfo = obj.ShippingInfo;
                store.City = obj.City;
                store.CreatedById = Id;
                _context.Store.Add(store);
                _context.SaveChanges();
            }
                return Ok(obj);
        }
        public void UplodImage(IFormFile file, string path)
        {
            using FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            stream.Dispose();
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        public JsonResult GetUser(string Email,string Password)
        {
            var random = new Random();
            int otp = random.Next(100000, 999999);
            string recipientEmail = Email;
            string senderEmail = Email;
            string senderPassword = Password;

            var smtp = new SmtpClient
            {
                Host = Email,
                Port = 5112,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };
            using (var message = new MailMessage(senderEmail, recipientEmail)
            {
                Subject = "OTP",
                Body = otp.ToString()
            })
            {
                smtp.Send(message);
            }
            var OTP = otp.ToString();
            return Json(OTP);
        }
    }
}

