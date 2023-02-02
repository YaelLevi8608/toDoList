using Microsoft.AspNetCore.Mvc;
using myTasks.Models;
using myTasks.Interfaces;
using myTasks.Service;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace myTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserService UserService;
        public UserController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet]
       // [Authorize(Policy = "Admin")]
        public ActionResult<List<MyUser>?> GetAll()=> 
            UserService.GetAll();
        

        [HttpGet("{id}")]
        public ActionResult<MyUser> Get(int id)
        {
            var user = UserService.Get(id);

            if (user == null)
                return NotFound();

            return user;
        }

       // [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult Create(MyUser user)
        {
            UserService.Add(user);
            return CreatedAtAction(nameof(Create), new { Password = user.Password }, user);

        }

        
        [HttpDelete("{id}")]
       // [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            var user = UserService.Get(id);
            if (user is null)
                return NotFound();
            UserService.Delete(user.Id);

            return Content(UserService.Count.ToString());
        }

       
        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] MyUser user)
        {   
         var claims = new List<Claim>();
        List< MyUser>users = UserService.GetAll();
        MyUser u=users.FirstOrDefault(u=>u.Password .Equals(user.Password)&& u.Name.Equals(user.Name));
       // Console.WriteLine(user.Name);
            if (user.Name!="yael" || user.Password!="3" )
            {
                if(u==null)
                     return Unauthorized();
                else

                claims = new List<Claim>
                {
                new Claim("type", "User"),
                new Claim("Name", user.Name),
                new Claim("Pasword",user.Password),
                new Claim("id", user.Id.ToString()),
                };
            }
            else{
             claims = new List<Claim>
            {
               new Claim("type", "Admin"),
                new Claim("Name", user.Name),
                new Claim("Pasword",user.Password),
                new Claim("id", user.Id.ToString()),
            };
            }
            var token = TokenService.GetToken(claims);

            return new OkObjectResult(TokenService.WriteToken(token));
        }

    }
}