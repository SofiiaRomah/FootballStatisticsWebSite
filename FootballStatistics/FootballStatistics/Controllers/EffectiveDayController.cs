using FootballStatistics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballStatistics.Controllers
{
    public class EffectiveDayController : Controller
    {
        public Legue legue1 = JsonConvert.DeserializeObject<Legue>(System.IO.File.ReadAllText(@"D:\Studying\FootballStatistics\FootballStatistics\en.1.json"));
        public Legue legue2 = JsonConvert.DeserializeObject<Legue>(System.IO.File.ReadAllText(@"D:\Studying\FootballStatistics\FootballStatistics\en.2.json"));
        public Legue legue3 = JsonConvert.DeserializeObject<Legue>(System.IO.File.ReadAllText(@"D:\Studying\FootballStatistics\FootballStatistics\en.3.json"));

        // GET: EffectiveDay
        public ActionResult EffectiveDay()
        {
            var legues = new List<Legue>();
            legues.Add(legue1);
            legues.Add(legue2);
            legues.Add(legue3);

            Dictionary<string, int> resultDay = new Dictionary<string, int>();
            foreach (var legue in legues)
            {
                foreach (var match in legue.Matches)
                {
                    if (resultDay.ContainsKey(match.Date))
                    {
                        resultDay[match.Date] += match.Score.Ft[0] + match.Score.Ft[1];
                    }
                    else
                    {
                        resultDay.Add(match.Date, match.Score.Ft[0] + match.Score.Ft[1]);
                    }
                }
            }

            int maxPoints = 0;
            foreach (KeyValuePair<string, int> entry in resultDay)
            {
                if (entry.Value > maxPoints)
                {
                    maxPoints = entry.Value;
                }
            }

            EffectiveDay result = new EffectiveDay();
            result.Date = resultDay.FirstOrDefault(x => x.Value == maxPoints).Key;
            result.Points = resultDay[result.Date];

            return View(result);
        }
    }
}