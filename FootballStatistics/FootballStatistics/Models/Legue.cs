using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballStatistics.Models
{
    public class Legue
    {
        public string Name { get; set; }
        public List<Match> Matches { get; set; }
    }
}