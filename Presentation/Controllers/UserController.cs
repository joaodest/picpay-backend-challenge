using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicpayChallenge.Infra.Data;

namespace PicpayChallenge.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        [HttpGet]


    }
}
