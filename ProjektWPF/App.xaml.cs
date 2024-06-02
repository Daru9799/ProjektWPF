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

            //Wyciaganie userow z bazy do listy
            var users = DbUsers.GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine(user.ToString());
            }
        }
    }

}
