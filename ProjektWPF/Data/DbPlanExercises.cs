using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Models;

namespace ProjektWPF.Data
{
    //Klasa statyczna zawierająca wszystkie metody potrzebne do wykonywania operacji na tabeli PLAN_EXERCISES
    static class DbPlanExercises
    {
        public static List<PlanExercises> GetPlanExercises()
        {
            using (var db = new MyDbContext())
            {
                return db.plan_exercises.ToList();
            }
        }
    }
}
