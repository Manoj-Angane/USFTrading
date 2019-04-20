using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace USFTrading.Models
{
    public class Company
    {   
        [Key]
        public string symbol { get; set; }
        public string companyName { get; set; }
        public string exchange { get; set; }
        public string industry { get; set; }
        public string description { get; set; }
        public string CEO { get; set; }
        public string sector { get; set; }
        public string Gainer_Loser { get; set; }
    }
}
