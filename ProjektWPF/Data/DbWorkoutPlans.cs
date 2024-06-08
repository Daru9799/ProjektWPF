using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

        public static void UpdateWorkoutData(int workoutID)
        {
            using (var db = new MyDbContext())
            {
                //Pobranie zsumowanych wartości 
                List<Double> sumCalories = db.Database.SqlQueryRaw<Double>("SELECT Sum(ex.calories_burned_per_minute*ex.average_time) " +
                    "FROM plan_exercises pe JOIN exercises ex ON pe.exercise_id = ex.exercise_id " +
                    "WHERE plan_id = {0};", workoutID).ToList();

                List<Double> sumTime = db.Database.SqlQueryRaw<Double>("SELECT Sum(ex.average_time)" +
                    "FROM plan_exercises pe JOIN exercises ex ON pe.exercise_id = ex.exercise_id " +
                    "WHERE plan_id = {0};", workoutID).ToList();

                //Update WorkoutPlan
                db.Database.ExecuteSqlRaw(
                "UPDATE workout_plans SET total_time = {0}, total_calories_burned = {1} " +
                "WHERE plan_id = {2};", sumTime[0], sumCalories[0], workoutID);
                db.SaveChanges();
            }
        }

    }
}
