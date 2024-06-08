using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public class WorkoutExercisePreview
    {
        [Column("plan_exercises_id")]
        public int PlanExercisesId { get; set; }
        [Column("plan_id")]
        public int PlanId { get; set; }
        public int? Order { get; set; }
        public int? Duration { get; set; }

        [Column("exercise_id")]
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        [Column("body_part")]
        public string BodyPart { get; set; }
        [Column("difficulty_level")]
        public string? DifficultyLevel { get; set; }
        [Column("calories_burned_per_minute")]
        public int? CaloriesBurnedPerMinute { get; set; }
        [Column("average_time")]
        public int? AverageTime { get; set; }
        public string? Description { get; set; }
        [Column("gif_path")]
        public string? GifPath { get; set; }

        public WorkoutExercisePreview(int planExercisesId, int planId, int? order, int? duration, int exerciseId, string name, string bodyPart, string? difficultyLevel, int? caloriesBurnedPerMinute, int? averageTime, string? description, string? gifPath)
        {
            PlanExercisesId = planExercisesId;
            PlanId = planId;
            Order = order;
            Duration = duration;
            ExerciseId = exerciseId;
            Name = name;
            BodyPart = bodyPart;
            DifficultyLevel = difficultyLevel;
            CaloriesBurnedPerMinute = caloriesBurnedPerMinute;
            AverageTime = averageTime;
            Description = description;
            GifPath = gifPath;
        }
    }
}
