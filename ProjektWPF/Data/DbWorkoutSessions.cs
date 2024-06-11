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

        //Ilosc treningow w ostatnim tygodniu
        public static int CountTrainingsLastWeek(int? userId)
        {
            DateTime oneWeekAgo = DateTime.Now.AddDays(-7);
            using (var db = new MyDbContext())
            {
                return db.workout_sessions.Count(s => s.UserId == userId && s.Date >= oneWeekAgo);
            }
        }
    }
}
