using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace ProjektWPF.Models
{
    //Klasa reprezentująca obiekt exercise
    public class Exercise
    {
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

        public Exercise(int exerciseId, string name, string bodyPart, string? difficultyLevel, int? caloriesBurnedPerMinute, int? averageTime, string? description, string? gifPath)
        {
            this.ExerciseId = exerciseId;
            this.Name = name;
            this.BodyPart = bodyPart;
            this.DifficultyLevel = difficultyLevel;
            this.CaloriesBurnedPerMinute = caloriesBurnedPerMinute;
            this.AverageTime = averageTime;
            this.Description = description;
            this.GifPath = gifPath;
        }
        public override string ToString()
        {
            return $"{ExerciseId}, {Name}, {BodyPart}, {DifficultyLevel}, {CaloriesBurnedPerMinute}, {AverageTime}, {Description}, {GifPath}";
        }
    }
}
