using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.EFModels;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendingController : ControllerBase
    {
        private readonly ApplicationContext _Context;
        private readonly CoinBag _CoinBag;
        public VendingController(ApplicationContext Context, CoinBag CoinBag)
        {
            _Context = Context;
            _CoinBag = CoinBag;
        }

        [HttpGet("getDrinks")]
        public List<Drink> GetDrinks()
        {
            return _Context.Drinks.ToList();
        }

        [HttpPost("putCoin")]
        public IActionResult PutCoin(int value, int count = 1)
        {
            try
            {
                _CoinBag.PutCoin(value, count);
            }catch(Exception e) {
                return BadRequest(new JsonResult(new { error = e.Message }));
            };
            return Ok();
        }

        [HttpPost("buyDrink")]
        public IActionResult BuyDrink(Drink Drink)
        {
            if(_CoinBag.isEnough(Drink))
            {
                var drink = _Context.Drinks.Where(val => val.Name == Drink.Name).FirstOrDefault();
                if(drink == null)
                {
                    return BadRequest(new JsonResult(new { error = "drink not exist" }));
                }
                if(drink.Count <= 0)
                {
                    return BadRequest(new JsonResult(new { error = "the drink is over" }));
                }

                drink.Count--;
                _Context.Update(drink);
                _Context.SaveChanges();
                _CoinBag.calcChange(drink);
                return Ok(drink);
            }
            return BadRequest(new JsonResult(new { error = "not enough money" }));
        }

        [HttpPost("isThereAChange")]
        public IActionResult IsThereAChange(Drink Drink)
        {
            return Ok(_CoinBag.IsThereAChange(Drink.Price));
        }

        [HttpGet("getChange")]
        public IActionResult GetChange()
        {
            return Ok(new JsonResult(_CoinBag.getChange()));
        }
    }
}
