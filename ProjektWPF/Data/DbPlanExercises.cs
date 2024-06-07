using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public static List<Exercise> GetWorkoutExercises(int workoutID) // Zwraca listę Ćwiczeń dla PlanuTreningowego
        {
            using (var db = new MyDbContext())
            {
                return db.Database.SqlQueryRaw<Exercise>("SELECT e.* FROM plan_exercises pe Join exercises e On pe.exercise_id = e.exercise_id where pe.plan_id = {0} order by pe.order;", workoutID).ToList();
            }
        }
    }
}
