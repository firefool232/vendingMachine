using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    public class Drink
    {
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
    }
}
