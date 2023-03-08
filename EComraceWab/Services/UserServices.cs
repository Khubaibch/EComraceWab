using EComraceWab.Models;
using EComraceWab.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EComraceWab.Entites;

namespace EComraceWab.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequist model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        // private List<User> _users = new List<User>
        //  {
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Email = "test", Password = "1234" }
        // };
        private readonly DarazContext _context;
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings, DarazContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
           
        }

        public AuthenticationResponse Authenticate(AuthenticationRequist model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticationResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
             
        
        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var token = new JwtSecurityToken
            (
                claims : new List<Claim>() { new Claim("Id", user.Id.ToString()) },
                expires : DateTime.UtcNow.AddDays(7),
                signingCredentials : new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );
            return tokenHandler.WriteToken(token);
        }
    }
}
