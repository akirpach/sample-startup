using Microsoft.AspNetCore.Mvc;

namespace SampleStartUp.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public IActionResult GetUsers()
        {
            var users = new[]
            {
                new { Name  = "Albs"},
                new { Name = "Bobs"}
            };
            return Ok(users);
        }
        
    }
}