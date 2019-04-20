using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace USFTrading.Models
{
    public class Sectors
    {

        public string type { get; set; }
        [Key]
        public string name { get; set; }
        public float performance { get; set; }
        public double lastUpdated { get; set; }
        public string time;
        public string Time
        {
            get {
                TimeSpan t = TimeSpan.FromMilliseconds(lastUpdated);
                time = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds,
                                        t.Milliseconds);
                return time; }

        }
    }
}
