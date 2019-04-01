using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Api.Services;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("User")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest();
            }

            var user = await _userService.GetUserByEmail(model.Email);
            if (user!=null)
            {
                return BadRequest("User already exist");
            }
            
            await _userService.CreateUser(model.Email);
            return Ok();
        }
    }
}