using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public class WorkoutPlan
    {
        [Column("plan_id")]
        public int PlanId { get; set; }
        public string Name { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("total_time")]
        public int? TotalTime { get; set; }
        [Column("total_calories_burned")]
        public int? TotalCaloriesBurned { get; set; }
        public string Description { get; set; }
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        public WorkoutPlan(int planId, string name, int userId, int? totalTime, int? totalCaloriesBurned, string description, DateTime? createdAt)
        {
            PlanId = planId;
            Name = name;
            UserId = userId;
            TotalTime = totalTime;
            TotalCaloriesBurned = totalCaloriesBurned;
            Description = description;
            CreatedAt = createdAt;
        }
        public override string ToString()
        {
            return $"{PlanId}, {Name}, {UserId}, {TotalTime}, {TotalCaloriesBurned}, {Description}, {CreatedAt}";
        }
    }
}
