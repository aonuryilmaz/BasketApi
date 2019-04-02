using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.Api.Models;
using Basket.Api.Services;
using Basket.Core.Domain.Models;
using Basket.Core.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Internal;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Basket.Api.Controllers
{
    [Route("Basket")]
    public class BasketController:ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        public BasketController(
            IBasketService basketService, 
            IUserService userService)
        {
            _basketService = basketService;
            _userService = userService;
        }
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddToBasketItemRequestModel model)
        {
            var user = await _userService.GetUserByEmail(model.Email);
            if (user==null)
            {
                return BadRequest("User not found");
            }

            var result=await _basketService.AddToBasket(user.Id, model.Sku, model.Quantity);
            return Ok(result);
        }
        
    }
}