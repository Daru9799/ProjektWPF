using System.CodeDom;
using System.Configuration;
using System.Data;
using System.Windows;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF
{
    public partial class App : Application
    {
        //Tutaj są rozne testy zeby cos sprawdzic na szybko w konsoli potem to trzeba usunąć
        public App()
        {
            //Dodanie rekordu do bazy (jako id dajac 0 sam sobie uzupelnia auto_incrementem)
            //User user1 = new User(0, "test2137", "password2137", "johnpaulosecundo@example.com", 30, "M", 80.5f, 180.0f, 10, 5000, TimeSpan.FromHours(20), DateTime.Now, DateTime.Now);
            //DbUsers.AddUserToDb(user1);

            //Usuwanie
            //DbUsers.DeleteUserFromDb(4); 

            //Wyciaganie cwiczen z bazy do listy
            Console.WriteLine("Ćwiczenia:");
            var exercises = DbExercises.GetExercises();
            foreach (var exercise in exercises)
            {
                Console.WriteLine(exercise.ToString());
            }

            //Select z tabeli plan_exercises do listy
            Console.WriteLine("PlanExercises:");
            var planExercises = DbPlanExercises.GetPlanExercises();
            foreach (var planExercise in planExercises)
            {
                Console.WriteLine(planExercise.ToString());
            }

            //Select z tabeli user_progress do listy
            Console.WriteLine("UserProgress:");
            var userProgresses = DbUserProgress.GetUserProgresses();
            foreach (var userProgress in userProgresses)
            {
                Console.WriteLine(userProgress.ToString());
            }
            //Select z tabeli users do listy
            Console.WriteLine("Users:");
            var users = DbUsers.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
            }
            //Select z tabeli workout_plans do listy
            Console.WriteLine("Workout_plans:");
            var workouts = DbWorkoutPlans.GetWorkouts();
            foreach (var workout in workouts)
            {
                Console.WriteLine(workout.ToString());
            }
            //Select z tabeli workout_sessions do listy
            Console.WriteLine("Workout_sessions:");
            var sessions = DbWorkoutSessions.GetWorkoutSessions();
            foreach (var session in sessions)
            {
                Console.WriteLine(session.ToString());
            }
        }
    }

}
