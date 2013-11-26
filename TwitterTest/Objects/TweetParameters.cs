using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsTwitterBot.Objects
{
    public class TweetParameters
    {
        public string Team { get; set; }
        public string Number { get; set; }
        public int? Season { get; set; }
        public string StatType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
