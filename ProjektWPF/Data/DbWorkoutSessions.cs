using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektWPF.Models;

namespace ProjektWPF.Data
{
    //Klasa statyczna zawierająca wszystkie metody potrzebne do wykonywania operacji na tabeli WORKOUTSESSIONS
    static class DbWorkoutSessions
    {
        public static List<WorkoutSession> GetWorkoutSessions()
        {
            using (var db = new MyDbContext())
            {
                return db.workout_sessions.ToList();
            }
        }
        //Dodawanie nowej sesji do bazy
        public static void AddWorkoutSession(int userId, int planId, DateTime date, int timeSpent, int burnedCalories)
        {
            using (var db = new MyDbContext())
            {
                WorkoutSession ws = new WorkoutSession(0, userId, planId, date, timeSpent, burnedCalories);
                db.workout_sessions.Add(ws);
                db.SaveChanges();
            }
        }
        public static void DeleteWorkoutSessions(int workoutID) // usuwanie wszystkich sesji powiązanych z planem treningowym
        {
            using (var db = new MyDbContext())
            {
                var workoutsToDelete = db.workout_sessions.Where(w => w.PlanId == workoutID).ToList();
                foreach (var w in workoutsToDelete)
                {
                    db.workout_sessions.Remove(w);
                }
                db.SaveChanges();
            }
        }


        //Ilosc treningow w ostatnim tygodniu
        public static int CountTrainingsLastWeek(int? userId)
        {
            DateTime oneWeekAgo = DateTime.Now.AddDays(-7);
            using (var db = new MyDbContext())
            {
                return db.workout_sessions.Count(s => s.UserId == userId && s.Date >= oneWeekAgo);
            }
        }

        //Ilosc treningow w ostatnim miesiacu
        public static int CountTrainingsLastMonth(int? userId)
        {
            DateTime oneWeekAgo = DateTime.Now.AddDays(-31);
            using (var db = new MyDbContext())
            {
                return db.workout_sessions.Count(s => s.UserId == userId && s.Date >= oneWeekAgo);
            }
        }

        //Ilosc treningow w ostatnim roku
        public static int CountTrainingsLastYear(int? userId)
        {
            DateTime oneWeekAgo = DateTime.Now.AddDays(-365);
            using (var db = new MyDbContext())
            {
                return db.workout_sessions.Count(s => s.UserId == userId && s.Date >= oneWeekAgo);
            }
        }

        //Ulubiony plan
        public static int GetMostFrequentPlanIdForUser(int? userId)
        {
            using (var db = new MyDbContext())
            {
                var mostFrequentPlanId = db.workout_sessions.Where(s => s.UserId == userId).GroupBy(s => s.PlanId).OrderByDescending(g => g.Count()).Select(g => g.Key).FirstOrDefault();
                return mostFrequentPlanId;
            }
        }

        //public static List<WorkoutSession> GetUserSessions(int? userId)
        //{
        //    using (var db = new MyDbContext())
        //    {
        //        var UserList = db.workout_sessions.Where(s => s.UserId == userId).OrderByDescending(up => up.Date).ToList();
        //        return UserList;
        //    }
        //}
        public static List<WorkoutSessionDetails> GetUserSessions(int? userId)
        {
            using (var db = new MyDbContext())
            {
                return db.Database.SqlQueryRaw<WorkoutSessionDetails>(
                    @"SELECT p.name, s.date, s.time_spent, s.calories_burned
               FROM workout_sessions AS s
               JOIN workout_plans AS p ON s.plan_id = p.plan_id
               WHERE s.user_id = {0} order by s.date DESC", userId).ToList();
            }
        }

        //Odbyte treningi filtrujac po dacie
        public static List<WorkoutSessionDetails> GetWorkoutSessionsWithDateRange(int? userId, DateTime date1, DateTime date2)
        {
            using (var db = new MyDbContext())
            {
                return db.Database.SqlQueryRaw<WorkoutSessionDetails>(
                    @"SELECT p.name, s.date, s.time_spent, s.calories_burned
               FROM workout_sessions AS s
               JOIN workout_plans AS p ON s.plan_id = p.plan_id
               WHERE s.user_id = {0} AND s.date >= {1} AND s.date <= {2} order by s.date DESC", userId, date1, date2).ToList();
            }
        }
    }
}
