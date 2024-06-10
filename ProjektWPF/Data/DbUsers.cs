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

        //Uzyskanie maila
        public static int GetIdByEmail(string email)
        {
            using (var db = new MyDbContext())
            {
                var user = db.users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    return user.UserId;
                }
                return 0;
            }
        }

        //Uzyskanie hasha
        public static string GetHashById(int? id)
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

        //Updatuje ostatnie logowanie
        public static void UpdateLastLogin(int? id)
        {
            using (var db = new MyDbContext())
            {
                var userToUpdate = db.users.FirstOrDefault(u => u.UserId == id);
                if (userToUpdate != null)
                {
                    userToUpdate.LastLogin = DateTime.Now;
                    db.Entry(userToUpdate).Property(u => u.LastLogin).IsModified = true;
                    db.SaveChanges();
                }
            }
        }

        //Zmiana loginu
        public static void UpdateUsername(int? id, string newUsername)
        {
            using (var db = new MyDbContext())
            {
                var userToUpdate = db.users.FirstOrDefault(u => u.UserId == id);
                if (userToUpdate != null)
                {
                    userToUpdate.Username = newUsername;
                    db.Entry(userToUpdate).Property(u => u.Username).IsModified = true;
                    db.SaveChanges();
                }
            }
        }

        //Zmiana maila
        public static void UpdateEmail(int? id, string newEmail)
        {
            using (var db = new MyDbContext())
            {
                var userToUpdate = db.users.FirstOrDefault(u => u.UserId == id);
                if (userToUpdate != null)
                {
                    userToUpdate.Email = newEmail;
                    db.Entry(userToUpdate).Property(u => u.Email).IsModified = true;
                    db.SaveChanges();
                }
            }
        }

        //Zmiana wzrostu
        public static void UpdateHeight(int? id, float newHeight)
        {
            using (var db = new MyDbContext())
            {
                var userToUpdate = db.users.FirstOrDefault(u => u.UserId == id);
                if (userToUpdate != null)
                {
                    userToUpdate.Height = newHeight;
                    db.Entry(userToUpdate).Property(u => u.Height).IsModified = true;
                    db.SaveChanges();
                }
            }
        }

        //Zmiana hasła
        public static void UpdatePassword(int? id, string newPassword)
        {
            using (var db = new MyDbContext())
            {
                var userToUpdate = db.users.FirstOrDefault(u => u.UserId == id);
                if (userToUpdate != null)
                {
                    userToUpdate.Password = newPassword;
                    db.Entry(userToUpdate).Property(u => u.Password).IsModified = true;
                    db.SaveChanges();
                }
            }
        }
    }
}
