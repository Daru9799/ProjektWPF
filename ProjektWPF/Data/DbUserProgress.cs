using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Models;

namespace ProjektWPF.Data
{
    //Klasa statyczna zawierająca wszystkie metody potrzebne do wykonywania operacji na tabeli USER_PROGRESS
    static class DbUserProgress
    {
        public static List<UserProgress> GetUserProgresses()
        {
            using (var db = new MyDbContext())
            {
                return db.user_progress.ToList();
            }
        }

        //Wszystkie dla danego usera
        public static List<UserProgress> GetAllProgressForUser(int? userId)
        {
            using (var db = new MyDbContext())
            {
                return db.user_progress.Where(up => up.UserId == userId).ToList();
            }
        }

        //Ostatni wpis w user_progress
        public static UserProgress GetLatestProgressForUser(int? userId)
        {
            using (var db = new MyDbContext())
            {
                return db.user_progress.Where(up => up.UserId == userId).OrderByDescending(up => up.Date).FirstOrDefault();
            }
        }

        //Waga z przedostatniego wpisu user_progress (przy zwróceni nulla oznacza to ze user nie dodawal swoich pomiarów)
        public static float? GetSecondLatestWeightForUser(int? userId)
        {
            using (var db = new MyDbContext())
            {
                return db.user_progress.Where(up => up.UserId == userId).OrderByDescending(up => up.Date).Skip(1).Select(up => up.Weight).FirstOrDefault();
            }
        }

        //Nowy wpis 
        public static void AddMeasurementToDb(UserProgress userProgress)
        {
            using (var db = new MyDbContext())
            {
                db.user_progress.Add(userProgress);
                db.SaveChanges();
            }
        }
    }
}
