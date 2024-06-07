using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public static List<WorkoutPlan> GetCurrentUserWorkouts()
        {
            using (var db = new MyDbContext())
            {
                int? userId = UserSession.CurrentUserId;
                //var tmp = db.Database.SqlQueryRaw<WorkoutPlan>("Select * From workout_plans Where user_id = {0}", userId).ToList();
                return db.Database.SqlQueryRaw<WorkoutPlan>("Select * From workout_plans Where user_id = {0}", userId).ToList();
            }
        }

    }
}
