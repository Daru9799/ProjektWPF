using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProjektWPF.Models
{
    public class PlanExercises
    {
        [Column("plan_exercises_id")]
        public int PlanExercisesId { get; set; }
        [Column("plan_id")]
        public int PlanId { get; set; }
        [Column("exercise_id")]
        public int ExerciseId { get; set; }
        public int? Order { get; set; }
        public int? Duration { get; set; }
        public PlanExercises(int planExercisesId, int planId, int exerciseId, int? order, int? duration) 
        {
            this.PlanExercisesId = planExercisesId;
            this.PlanId = planId;
            this.ExerciseId = exerciseId;
            this.Order = order;
            this.Duration = duration;
        }
        public override string ToString()
        {
            return $"{PlanExercisesId}, {PlanId}, {ExerciseId}, {Order}, {Duration}";
        }
    }
}
