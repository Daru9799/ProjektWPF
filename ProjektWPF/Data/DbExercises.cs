using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Models;

namespace ProjektWPF.Data
{
    //Klasa statyczna zawierająca wszystkie metody potrzebne do wykonywania operacji na tabeli EXERCISES
    static class DbExercises
    {
        //Wypisanie wszystkich cwiczen
        public static List<Exercise> GetExercises()
        {
            using (var db = new MyDbContext())
            {
                return db.exercises.ToList();
            }
        }
    }
}
