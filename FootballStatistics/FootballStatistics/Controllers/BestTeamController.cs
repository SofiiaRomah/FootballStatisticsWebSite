using FootballStatistics.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballStatistics.Controllers
{
    public class BestTeamController : Controller
    {
        public Legue legue1 = JsonConvert.DeserializeObject<Legue>(System.IO.File.ReadAllText(@"D:\Studying\FootballStatistics\FootballStatistics\en.1.json"));
        public Legue legue2 = JsonConvert.DeserializeObject<Legue>(System.IO.File.ReadAllText(@"D:\Studying\FootballStatistics\FootballStatistics\en.2.json"));
        public Legue legue3 = JsonConvert.DeserializeObject<Legue>(System.IO.File.ReadAllText(@"D:\Studying\FootballStatistics\FootballStatistics\en.3.json"));

        // GET: BestTeam
        public ActionResult BestAttack()
        {
            var legues = new List<Legue>();
            legues.Add(legue1);
            legues.Add(legue2);
            legues.Add(legue3);

            var first = new Dictionary<string, int>();
            foreach(var legue in legues)
            {
                foreach (var match in legue.Matches)
                {
                    if (first.ContainsKey(match.Team1))
                    {
                        first[match.Team1] += match.Score.Ft[0];
                    }
                    else if (first.ContainsKey(match.Team2))
                    {
                        first[match.Team2] += match.Score.Ft[1];
                    }
                    else
                    {
                        first.Add(match.Team1, match.Score.Ft[0]);
                        first.Add(match.Team2, match.Score.Ft[1]);
                    }
                }
            }
            
            int maxScore = 0;
            foreach (KeyValuePair<string, int> entry in first)
            {
                if (entry.Value > maxScore)
                {
                    maxScore = entry.Value;
                }
            }

            string legueName = "";
            foreach (var legue in legues)
            {
                foreach(var match in legue.Matches)
                {
                    if(match.Team1 == first.FirstOrDefault(x => x.Value == maxScore).Key ||
                        match.Team2 == first.FirstOrDefault(x => x.Value == maxScore).Key)
                    {
                        legueName = legue.Name;
                    }
                }
            }

            BestTeam bestTeam = new BestTeam();
            bestTeam.Name = first.FirstOrDefault(x => x.Value == maxScore).Key;
            bestTeam.Points = first[bestTeam.Name];
            bestTeam.LegueName = legueName;

            return PartialView(bestTeam);
        }

        public ActionResult BestDefense()
        {
            var legues = new List<Legue>();
            legues.Add(legue1);
            legues.Add(legue2);
            legues.Add(legue3);

            var second = new Dictionary<string, int>();
            foreach (var legue in legues)
            {
                foreach (var match in legue.Matches)
                {
                    if (second.ContainsKey(match.Team1))
                    {
                        second[match.Team1] += match.Score.Ft[1];
                    }
                    else if (second.ContainsKey(match.Team2))
                    {
                        second[match.Team2] += match.Score.Ft[0];
                    }
                    else
                    {
                        second.Add(match.Team1, match.Score.Ft[1]);
                        second.Add(match.Team2, match.Score.Ft[0]);
                    }
                }
            }

            int minScore = 10000;
            foreach (KeyValuePair<string, int> entry in second)
            {
                if (entry.Value < minScore)
                {
                    minScore = entry.Value;
                }
            }

            string legueName = "";
            foreach (var legue in legues)
            {
                foreach (var match in legue.Matches)
                {
                    if (match.Team1 == second.FirstOrDefault(x => x.Value == minScore).Key ||
                        match.Team2 == second.FirstOrDefault(x => x.Value == minScore).Key)
                    {
                        legueName = legue.Name;
                    }
                }
            }

            BestTeam bestTeam = new BestTeam();
            bestTeam.Name = second.FirstOrDefault(x => x.Value == minScore).Key;
            bestTeam.Points = second[bestTeam.Name];
            bestTeam.LegueName = legueName;

            return View(bestTeam);
        }

        public ActionResult BestScoredMissed()
        {
            var legues = new List<Legue>();
            legues.Add(legue1);
            legues.Add(legue2);
            legues.Add(legue3);

            var third = new Dictionary<string, int>();
            foreach (var legue in legues)
            {
                foreach (var match in legue.Matches)
                {
                    int diff1 = match.Score.Ft[0] - match.Score.Ft[1];
                    int diff2 = match.Score.Ft[1] - match.Score.Ft[0];
                    if (third.ContainsKey(match.Team1))
                    {
                        third[match.Team1] += diff1;
                    }
                    else if (third.ContainsKey(match.Team2))
                    {
                        third[match.Team2] += diff2;
                    }
                    else
                    {
                        third.Add(match.Team1, diff1);
                        third.Add(match.Team2, diff2);
                    }
                }
            }

            int maxDiff = 0;
            foreach (KeyValuePair<string, int> entry in third)
            {
                if (entry.Value > maxDiff)
                {
                    maxDiff = entry.Value;
                }
            }

            var temp = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> entry in third)
            {
                if (entry.Value == maxDiff)
                {
                    temp.Add(entry.Key, entry.Value);
                }
            }

            BestTeam bestTeam = new BestTeam();

            if (temp.Count() > 1)
            {
                var maxGoals = 0;
                foreach(var t in temp)
                {
                    if(t.Value > maxGoals)
                    {
                        maxGoals = t.Value;
                    }
                }
                bestTeam.Name = temp.FirstOrDefault(x => x.Value == maxGoals).Key;
                bestTeam.Points = temp[bestTeam.Name];
            }
            else if (temp.Count() == 1)
            {
                bestTeam.Name = third.FirstOrDefault(x => x.Value == maxDiff).Key;
                bestTeam.Points = third[bestTeam.Name];
            }


            string legueName = "";
            foreach (var legue in legues)
            {
                foreach (var match in legue.Matches)
                {
                    if (match.Team1 == bestTeam.Name ||
                        match.Team2 == bestTeam.Name)
                    {
                        legueName = legue.Name;
                    }
                }
            }
            bestTeam.LegueName = legueName;

            return View(bestTeam);
        }
    }
}