using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionMiddleware.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        List<User> userList = new List<User>();
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;

            userList.Add(new User { UserId = 1, UserName = "bkrclk", UserSize = 170, UserWeight = 0 });
            userList.Add(new User { UserId = 2, UserName = "aliveli", UserSize = 170, UserWeight = 80 });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Wellcome");
        }

        [HttpGet("UserBodyIndex")]
        public ActionResult<float> UserBodyIndex(int id)
        {
            if (userList.Count == 0)
                return NotFound("userList Not Found!");

            User user = userList.Where(x => x.UserId == id).FirstOrDefault();
            if (user == null)
                return BadRequest("User is null");

            var userBodyIndex = user.UserSize / user.UserWeight; // 0 ile bölme işlemi gerçekleştirlemez

            return userBodyIndex;
        }
    }
}
