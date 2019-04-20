using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace USFTrading.Models
{
    public class Infocus
    {   
        [Key]
        public string symbol { get; set; }
    }
}
