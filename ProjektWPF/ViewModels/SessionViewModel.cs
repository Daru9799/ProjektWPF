using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

using System.Windows.Threading;

namespace ProjektWPF.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private WorkoutPlan workoutPlan;
        private List<WorkoutExercisePreview> exercisesList;
        private DispatcherTimer timer;
        private DateTime TimeStart;

        public SessionViewModel(WorkoutPlan wp)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(UpdateTimer);

            this.workoutPlan = wp;
            ExercisesList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
        }
        

        public List<WorkoutExercisePreview> ExercisesList
        {
            get { return exercisesList; }
            set { 
                exercisesList = value; 
                OnPropertyChanged();
            }
        }

        private void UpdateTimer(object sender, EventArgs e) // Funkcja która jest wykonywana co Tick timera 
        {
            TimeSpan tmpTime = System.DateTime.Now - TimeStart;
            SolvingTime = tmpTime.ToString(@"hh\:mm\:ss");
        }

        private string solvingTime; //Zmienna odpowiedzialna za wyświetlanie czasu timera
        public string SolvingTime 
        {
            get { return solvingTime; }
            set
            {
                solvingTime = value;
                OnPropertyChanged();
            }
        }
    }
}
