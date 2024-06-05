using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektWPF.Models;

namespace ProjektWPF.Data
{
    //Klasa ogarniająca połączenie z bazą i mapowanie tabel na obiekty
    class MyDbContext : DbContext
    {
        public DbSet<User> users { get; set; } //Mapowanie tabeli na obiekt (nazwa musi byc taka sama jak nazwa tabeli w bazie)
        public DbSet<Exercise> exercises { get; set; }
        public DbSet<PlanExercises> plan_exercises { get; set; }
        public DbSet<UserProgress> user_progress { get; set; }
        public DbSet<WorkoutPlan> workout_plans { get; set; }
        public DbSet<WorkoutSession> workout_sessions { get; set; }

        //Polaczenie z baza
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=mysql-project-daru979.k.aivencloud.com;user=avnadmin;database=workout-project;port=19750;password=AVNS_iCL-WRfqAYjDGNJDlRx;", new MySqlServerVersion(new Version(8, 0, 21)));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProgress>().HasKey(up => up.ProgressId); //Przypisuje recznie bo inaczej wywala blad
            modelBuilder.Entity<WorkoutPlan>().HasKey(up => up.PlanId);
            modelBuilder.Entity<WorkoutSession>().HasKey(up => up.SessionId);
        }
    }
}
