using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public static void AddWorkoutPlan(WorkoutPlan wp)
        {
            using (var db = new MyDbContext())
            {
                db.workout_plans.Add(wp);
                db.SaveChanges();
            }
        }

        public static void ModifyWorkoutPlan(WorkoutPlan wp)
        {
            using (var db = new MyDbContext())
            {
                db.workout_plans.Update(wp);
                db.SaveChanges();
            }
        }

        public static void DeleteWorkoutPlan(WorkoutPlan wp)
        {
            using (var db = new MyDbContext())
            {

                db.Database.ExecuteSqlRaw("DELETE FROM workout_plans " +
                                          "WHERE workout_plans.plan_id = {0};", wp.PlanId);
                db.SaveChanges();
            }
        }

        public static void UpdateWorkoutData(int workoutID) // czas trwania ćwiczenia jest brany z PlanExercises
        {
            using (var db = new MyDbContext())
            {
                if (db.plan_exercises.Where(pe => pe.PlanId == workoutID).ToList().Count()!= 0) 
                {
                    //Pobranie zsumowanych wartości 
                    var tmp = db.Database.SqlQueryRaw<Double>("SELECT Sum(ex.calories_burned_per_minute*pe.duration) " +
                        "FROM plan_exercises pe join exercises ex on pe.exercise_id = ex.exercise_id " +
                        "where plan_id = {0};", workoutID).ToList();
                    List<Double> sumCalories = db.Database.SqlQueryRaw<Double>("SELECT Sum(ex.calories_burned_per_minute*pe.duration) " +
                        "FROM plan_exercises pe join exercises ex on pe.exercise_id = ex.exercise_id " +
                        "where plan_id = {0};", workoutID).ToList();

                    List<Double> sumTime = db.Database.SqlQueryRaw<Double>("SELECT Sum(pe.duration) " +
                        "FROM plan_exercises pe join exercises ex on pe.exercise_id = ex.exercise_id " +
                        "where plan_id = {0};", workoutID).ToList();

                    //Update WorkoutPlan
                    db.Database.ExecuteSqlRaw(
                    "UPDATE workout_plans SET total_time = {0}, total_calories_burned = {1} " +
                    "WHERE plan_id = {2};", sumTime[0], sumCalories[0], workoutID);
                }
                else // Przypadek w którym aktualizujemy WorkoutPlan, a plan nie ma ćwiczeń
                {
                    db.Database.ExecuteSqlRaw(
                    "UPDATE workout_plans SET total_time = {0}, total_calories_burned = {1} " +
                    "WHERE plan_id = {2};", 0, 0, workoutID);
                    
                }
                db.SaveChanges();

            }
        }

        public static string GetWorkoutName(int workoutId)
        {
            using (var db = new MyDbContext())
            {
                var plan = db.workout_plans.FirstOrDefault(u => u.PlanId == workoutId);
                if (plan != null)
                {
                    return plan.Name;
                }
                return "Brak Planu";
            }
        }

    }
}
