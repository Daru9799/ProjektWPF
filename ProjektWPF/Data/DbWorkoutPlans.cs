using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Models;

namespace ProjektWPF.Data
{
    //Klasa statyczna zawierająca wszystkie metody potrzebne do wykonywania operacji na tabeli WORKOUTPLANS
    static class DbWorkoutPlans
    {
        public static List<WorkoutPlan> GetWorkouts()
        {
            using (var db = new MyDbContext())
            {
                return db.workout_plans.ToList();
            }
        }
    }
}
