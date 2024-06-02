using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektWPF.Models;

namespace ProjektWPF.Data
{
    //Klasa statyczna zawierająca wszystkie metody potrzebne do wykonywania operacji na tabeli USERS
    static class DbUsers 
    {
        //Wypisanie wszystkich uzytkownikow
        public static List<User> GetUsers()
        {
            using (var db = new MyDbContext())
            {
                return db.users.ToList();
            }
        }
        //Dodanie uzytkownika do bazy
        public static void AddUserToDb(User user)
        {
            using (var db = new MyDbContext())
            {
                db.users.Add(user);
                db.SaveChanges();
            }
        }
        //Usuwanie uzytkownika z bazy
        public static void DeleteUserFromDb(int id)
        {
            using (var db = new MyDbContext())
            {
                var userToDelete = db.users.FirstOrDefault(u => u.UserId == id);
                if (userToDelete != null)
                {
                    db.users.Remove(userToDelete);
                    db.SaveChanges();
                }
            }
        }
    }
}
