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

        public static List<WorkoutExercisePreview> GetWorkoutExercises(int workoutID) // Zwraca listę Ćwiczeń dla PlanuTreningowego
        {
            using (var db = new MyDbContext())
            {
                return db.Database.SqlQueryRaw<WorkoutExercisePreview>("SELECT pe.plan_exercises_id, pe.plan_id, pe.order, pe.duration, e.* " +
                    "FROM plan_exercises pe Join exercises e On pe.exercise_id = e.exercise_id " +
                    "where pe.plan_id = {0} order by pe.order;", workoutID).ToList();
            }
        }

        public static void DeleteWorkoutExercise(WorkoutExercisePreview wep)
        {
            using (var db = new MyDbContext())
            {
                var planExerciseToDelete = db.plan_exercises.FirstOrDefault(rec => (rec.PlanExercisesId==wep.PlanExercisesId));
                if (planExerciseToDelete != null)
                {
                    var peToDelete = new PlanExercises(wep);
                    db.Database.ExecuteSqlRaw("DELETE FROM plan_exercises pe " +
                                              "WHERE pe.plan_exercises_id={0};", wep.PlanExercisesId);
                    db.SaveChanges();

                    DbWorkoutPlans.UpdateWorkoutData(wep.PlanId); // Przeliczenie czasu i kalori
                }
            }
        }
    }
}
