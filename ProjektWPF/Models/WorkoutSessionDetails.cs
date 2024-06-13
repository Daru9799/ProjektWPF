using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public class WorkoutSessionDetails
    {
        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("time_spent")]
        public int? TimeSpent { get; set; }

        [Column("calories_burned")]
        public int? CaloriesBurned { get; set; }

        [Column("name")]
        public string? PlanName { get; set; }

        // Parameterless constructor needed by Entity Framework
        public WorkoutSessionDetails() { }

        public WorkoutSessionDetails(string planName, DateTime? date, int? timeSpent, int? caloriesBurned)
        {
            PlanName = planName;
            Date = date;
            TimeSpent = timeSpent;
            CaloriesBurned = caloriesBurned;
        }

        public override string ToString()
        {
            return $"{PlanName}, {Date}, {TimeSpent}, {CaloriesBurned}";
        }
    }
}
