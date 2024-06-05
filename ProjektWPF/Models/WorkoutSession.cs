using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public class WorkoutSession
    {
        [Column("session_id")]
        public int SessionId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("plan_id")]
        public int PlanId { get; set; }
        public DateTime? Date { get; set; }
        [Column("time_spent")]
        public int? TimeSpent { get; set; }
        [Column("calories_burned")]
        public int? CaloriesBurned { get; set; }

        public WorkoutSession(int sessionId, int userId, int planId, DateTime? date, int? timeSpent, int? caloriesBurned)
        {
            this.SessionId = sessionId;
            this.PlanId = planId;
            this.UserId = userId;
            this.Date = date;
            this.TimeSpent = timeSpent;
            this.CaloriesBurned = caloriesBurned;
        }
        public override string ToString()
        {
            return $"{SessionId}, {UserId}, {PlanId}, {Date}, {TimeSpent}, {CaloriesBurned}";
        }
    }
}
