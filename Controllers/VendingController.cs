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
        public VendingController(ApplicationContext Context)
        {
            _Context = Context;
        }

        [HttpGet("getDrinks")]
        public List<Drink> GetDrinks()
        {
            return _Context.Drinks.ToList();
        }
    }
}
