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
    }
}
