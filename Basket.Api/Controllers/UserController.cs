using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("User")]
    public class UserController:ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateUserRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return BadRequest();
            }

            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user!=null)
            {
                return BadRequest("User already exist");
            }
            
            await _userRepository.Create(new User(model.Email));
            return Ok();
        }
    }
}