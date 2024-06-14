using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static void DeleteWorkoutExercises(int workoutID)
        {
            using (var db = new MyDbContext())
            {
                var workoutsToDelete = db.plan_exercises.Where(w => w.PlanId == workoutID).ToList();
                foreach (var w in workoutsToDelete)
                {
                    db.plan_exercises.Remove(w);
                }
                db.SaveChanges();

            }
        }

        /*public static void DeleteWorkoutExercise(WorkoutExercisePreview wep)
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
        }*/

        public static void SaveModifiedPlanExercises(ObservableCollection<WorkoutExercisePreview> wep)
        {
            List<PlanExercises> tmpList = new List<PlanExercises>();
            foreach(var i in wep)
            {
                tmpList.Add(new PlanExercises(i));
            }
            var tmp1 = tmpList;
            using (var db = new MyDbContext())
            {
                // Pobierz wszystkie wpisy PlanExercises z bazy danych związane z bieżącym planem
                var existingPlanExercises = db.plan_exercises.Where(pe => pe.PlanId == wep[0].PlanId).ToList();

                // Przetwórz każdy element w kolekcji WorkoutExercisePreview
                foreach (var item in wep)
                {
                    var existingPlanExercise = existingPlanExercises.FirstOrDefault(pe => pe.PlanExercisesId == item.PlanExercisesId);

                    if (existingPlanExercise != null)
                    {
                        // Jeśli istnieje wpis, zaktualizuj wartość Order, jeśli jest inna niż w elemencie WorkoutExercisePreview
                        if (existingPlanExercise.Order != item.Order || existingPlanExercise.Duration != item.Duration)
                        {
                            existingPlanExercise.Order = item.Order;
                            existingPlanExercise.Duration = item.Duration;
                            db.Entry(existingPlanExercise).State = EntityState.Modified;
                        }

                        // Usuń element z listy existingPlanExercises, aby na końcu pozostały tylko usunięte elementy
                        existingPlanExercises.Remove(existingPlanExercise);
                    }
                    else
                    {
                        // Jeśli nie istnieje, dodaj nowy wpis do tabeli plan_exercises
                        var newPlanExercise = new PlanExercises(item);
                        db.plan_exercises.Add(newPlanExercise);
                    }
                }

                // Usuń elementy, które nie są już obecne w kolekcji WorkoutExercisePreview
                foreach (var item in existingPlanExercises)
                {
                    db.plan_exercises.Remove(item);
                }

                // Zapisz zmiany w bazie danych
                db.SaveChanges();
            }
        }
    }
}
