using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

using System.Windows.Threading;
using System.Windows.Input;
using System.Web;

namespace ProjektWPF.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private WorkoutPlan workoutPlan;
        private List<WorkoutExercisePreview> exercisesList;
        private DispatcherTimer timer;
        private DateTime TimeStart;
        private WorkoutExercisePreview currentExercise;
        private int currentExerciseIndex;
        private string gifPath;
        private string nextExerciseName;
        private string previousExerciseName;
        private string exercisesCounter;

        public SessionViewModel(WorkoutPlan wp)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(UpdateTimer);

            this.workoutPlan = wp;
            ExercisesList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
            SetValues(ExercisesList);

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


        public WorkoutExercisePreview CurrentExercise
        {
            get { return currentExercise; }
            set
            {
                if (currentExercise != value)
                {
                    currentExercise = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GifPath
        {
            get { return gifPath; }
            set
            {
                if(gifPath != value)
                {
                    gifPath = value;
                    OnPropertyChanged(nameof(GifPath));
                }
            }

        }

        public string NextExerciseName
        {
            get { return nextExerciseName; }
            set { 
                if(nextExerciseName != value)
                {
                    nextExerciseName = value;
                    OnPropertyChanged(nameof(NextExerciseName));
                }
            }
        }

        public string CurrentExerciseName
        {
            get { return previousExerciseName; }
            set
            {
                if(previousExerciseName != value)
                {
                    previousExerciseName = value;
                    OnPropertyChanged(nameof(CurrentExerciseName));
                }
            }
        }

        public string ExercisesCounter
        {
            get { return exercisesCounter; }
            set
            {
                if(exercisesCounter != value)
                {
                    exercisesCounter = value;
                    OnPropertyChanged(nameof(ExercisesCounter));
                }
            }
        }








        private ICommand nextExercise=null;

        public ICommand NextExercise
        {
            get
            {
                if (nextExercise == null)
                {
                    nextExercise = new RelayCommand(arg =>{ Next(); },null) ;
                }
                return nextExercise;
            }
        }

        private void Next()
        {
            if (currentExerciseIndex<exercisesList.Count-1)
            {

            }
        }

        private void SetValues(List<WorkoutExercisePreview> list)
        {
            CurrentExercise = list[0];
            CurrentExerciseName = list[0].Name;
            currentExerciseIndex = 0;
            if(list.Count > 1)
            {
                NextExerciseName = list[1].Name;
            }
            GifPath = list[0].GifPath;
            ExercisesCounter = $"{currentExerciseIndex+1}/{ExercisesList.Count}";

        }




     





    }
}
