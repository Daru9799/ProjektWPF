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
        //Uzyskanie Id uzytkownika o podanej nazwie (gdy nie istnieje taki zwraca 0)
        public static int GetIdByName(string name)
        {
            using (var db = new MyDbContext())
            {
                var user = db.users.FirstOrDefault(u => u.Username == name);
                if(user != null)
                {
                    return user.UserId;
                }
                return 0;
            }
        }
        public static string GetHashById(int id)
        {
            using (var db = new MyDbContext())
            {
                var user = db.users.FirstOrDefault(u => u.UserId == id);
                if (user != null)
                {
                    string hash = user.Password;
                    return hash;
                }
                return null;
            }
        }

        //Zwraca konkretnego usera jako obiekt
        public static User GetUserFromDb(int? id)
        {
            using (var db = new MyDbContext())
            {
                var userToGet = db.users.FirstOrDefault(u => u.UserId == id);
                if (userToGet != null)
                {
                    return userToGet;
                } else
                {
                    return null;
                }

            }
        }
    }
}
