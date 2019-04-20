using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace USFTrading.Models
{
    public class KeyStatLosers
    {
        [Key]
        public string symbol { get; set; }
        public double marketcap { get; set; }
        public float week52high { get; set; }
        public float week52low { get; set; }
        public float week52change { get; set; }
        public float ttmEPS { get; set; }
        public float ttmDividendRate { get; set; }
        public float peRatio { get; set; }

    }
}
