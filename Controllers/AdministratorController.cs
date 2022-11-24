using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.EFModels;
using VendingMachine.Models;

namespace VendingMachine.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly ApplicationContext _Context;
        public AdministratorController(ApplicationContext Context)
        {
            _Context = Context;
        }

        [HttpPost("addDrink")]
        public IActionResult AddDrink([FromBody] Drink Drink)
        {
            int exist = _Context.Drinks.Count(val => val.Name == Drink.Name);
            if(exist > 0)
            {
                return BadRequest(new JsonResult(new { error = "drink already exist" }));
            }
            _Context.Add(Drink);
            _Context.SaveChanges();
            return Ok();
        }

        [HttpPost("removeDrink")]
        public IActionResult RemoveDrink([FromBody] Drink Drink)
        {
            var exist = _Context.Drinks.Where(val => val.Id == Drink.Id).FirstOrDefault();
            if (exist == null)
            {
                return BadRequest(new JsonResult(new { error = "drink not exist" }));
            }
            _Context.Drinks.Remove(exist);
            _Context.SaveChanges();
            return Ok();
        }

        [HttpPost("changeDrink")]
        public IActionResult ChangePrice([FromBody] Drink Drink)
        {
            var exist = _Context.Drinks.Where(val => val.Id == Drink.Id).FirstOrDefault();
            if (exist == null)
            {
                return BadRequest(new JsonResult(new { error = "drink not exist" }));
            }
            exist.Price = Drink.Price;
            exist.Name = Drink.Name;
            exist.Count = Drink.Count;
            _Context.Drinks.Update(exist);
            _Context.SaveChanges();
            return Ok();
        }
    }
}
