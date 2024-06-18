using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;
using System.Windows.Input;
using System.IO.Packaging;
using System.Drawing;
using System.Web;
using System.Media;

namespace ProjektWPF.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private WorkoutPlan workoutPlan;
        private List<WorkoutExercisePreview> exercisesList;
        private MainViewModel mainViewModel;
        private PersonalTimer exerciseTimer;
        private WorkoutExercisePreview currentExercise;
        private int currentExerciseIndex;
        private string gifPath;
        private string nextExerciseName;
        private string previousExerciseName;
        private string exercisesCounter;
        private string timerButtonText;
        private int? calories = 0;
        private string? caloriesText;
        private int? totalTime = 0;
        private string? totalTimeText;

        public SessionViewModel(WorkoutPlan wp, MainViewModel mv)
        {
            mainViewModel = mv;
            this.workoutPlan = wp;
            try
            {
                ExercisesList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
            }
            catch (InvalidOperationException ex)
            {
                UserSession.CurrentSqlError += 1; //Jesli serwer nie dziala to przelacza na okno z informacją o tym
            }
            exerciseTimer = new PersonalTimer();
            exerciseTimer.Tick += ExerciseTimer_Tick;
            exerciseTimer.TimerFinished += ExerciseTimer_TimerFinished;
            TimerButtonText = "Start";
            if(ExercisesList != null) 
            {
                SetValues(ExercisesList);
            }
        }
        #region Publiczne   
        public List<WorkoutExercisePreview> ExercisesList
        {
            get { return exercisesList; }
            set
            {
                exercisesList = value;
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

        public string TotalTimeText
        {
            get { return totalTimeText; }
            set
            {
                if (value != totalTimeText)
                {
                    totalTimeText = value;
                    OnPropertyChanged(nameof(TotalTimeText));
                }
            }
        }

        public string Calories
        {
            get
            {
                return caloriesText;
            }
            set
            {
                caloriesText = value;
                OnPropertyChanged(nameof(Calories));
            }
        }

        public string GifPath
        {
            get { return gifPath; }
            set
            {
                if (gifPath != value)
                {
                    gifPath = value;
                    OnPropertyChanged(nameof(GifPath));
                }
            }
        }

        public string NextExerciseName
        {
            get { return nextExerciseName; }
            set
            {
                if (nextExerciseName != value)
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
                if (previousExerciseName != value)
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
                if (exercisesCounter != value)
                {
                    exercisesCounter = value;
                    OnPropertyChanged(nameof(ExercisesCounter));
                }
            }
        }

        public string FormattedTime
        {
            get
            {
                return IsTimerVisible ? exerciseTimer.FormattedTime : "";
            }
        }


        public string TimerButtonText
        {
            get { return timerButtonText; }
            set
            {
                if (timerButtonText != value)
                {
                    timerButtonText = value;
                    OnPropertyChanged(nameof(TimerButtonText));
                }
            }
        }

        private bool isStartStop;
        public bool IsStartStop
        {
            get { return isStartStop; }
            set
            {
                if (isStartStop != value)
                {
                    isStartStop = value;
                    OnPropertyChanged(nameof(IsStartStop));
                }
            }
        }

        private bool isTimerVisible = true;

        public bool IsTimerVisible
        {
            get { return isTimerVisible; }
            set
            {
                if (isTimerVisible != value)
                {
                    isTimerVisible = value;
                    OnPropertyChanged(nameof(IsTimerVisible));
                    OnPropertyChanged(nameof(FormattedTime));
                }
            }
        }

        private string nextButtonText = "Następne ćwiczenie";
        public string NextButtonText
        {
            get { return nextButtonText; }
            set
            {
                if (nextButtonText != value)
                {
                    nextButtonText = value;
                    OnPropertyChanged(nameof(NextButtonText));
                }
            }
        }

        private bool isNextButtonEnabled = true;
        public bool IsNextButtonEnabled
        {
            get { return isNextButtonEnabled; }
            set
            {
                if (isNextButtonEnabled != value)
                {
                    isNextButtonEnabled = value;
                    OnPropertyChanged(nameof(IsNextButtonEnabled));
                }
            }
        }
        #endregion

        private ICommand nextExercise = null;

        public ICommand NextExercise
        {
            get
            {
                if (nextExercise == null)
                {
                    nextExercise = new RelayCommand(arg => { Next(); }, arg => IsNextButtonEnabled);
                }
                return nextExercise;
            }
        }


        private ICommand toggleTimerCommand = null;

        public ICommand ToggleTimerCommand
        {
            get
            {
                if (toggleTimerCommand == null)
                {
                    toggleTimerCommand = new RelayCommand(arg => { ToggleTimer(); }, null);
                }
                return toggleTimerCommand;
            }
        }

        private ICommand goBack = null;
        public ICommand GoBack
        {
            get
            {
                if (goBack == null)
                {
                    goBack = new RelayCommand(arg => { ChangeView(); }, null);
                }
                return goBack;
            }
        }

        private void ChangeView()
        {
            mainViewModel.CheangeViewToWorkoutsPanel();
        }



        private void Next()
        {
            calories += CalculateCalories();
            totalTime += CalulateTime();

            if (currentExerciseIndex < exercisesList.Count - 1)
            {
                currentExerciseIndex++;
                SetValues(exercisesList);
                RestartTimer();
            }
            else
            {
                IsStartStop = false;
                ShowResults();
                IsNextButtonEnabled = false;
            }

            Console.WriteLine(calories);
            Console.WriteLine(totalTime);
        }


        private void SetValues(List<WorkoutExercisePreview> list)
        {
            CurrentExercise = list[currentExerciseIndex];
            CurrentExerciseName = list[currentExerciseIndex].Name;
            IsStartStop = true;
            IsTimerVisible = true;

            if (list.Count > currentExerciseIndex + 1)
            {
                NextExerciseName = "Następne:\n" + list[currentExerciseIndex + 1].Name;
            }
            else
            {
                if (list.Count == 1)
                {
                    NextExerciseName = "";

                }
                else
                {
                    NextExerciseName = "Ostatnie!";
                    // IsNextExerciseAvailable = false;
                }
                NextButtonText = "Podsumowanie";
            }

            GifPath = list[currentExerciseIndex].GifPath;
            ExercisesCounter = $"{currentExerciseIndex + 1}/{ExercisesList.Count}";
            RestartTimer();
        }


        private void RestartTimer()
        {
            exerciseTimer.Restart(CurrentExercise.Duration);
            TimerButtonText = "Start";
        }

        private void ToggleTimer()
        {
            exerciseTimer.Toggle();
            TimerButtonText = exerciseTimer.IsRunning ? "Stop" : "Start";

            if (exerciseTimer.FormattedTime == "00:00")
            {
                Next();
            }
        }

        private void ExerciseTimer_Tick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(FormattedTime));
        }

        private void ExerciseTimer_TimerFinished(object sender, EventArgs e)
        {
            Next();
        }



        private int? CalculateCalories()
        {
            int t = (int)Math.Round((decimal)((CurrentExercise.Duration * 60 - exerciseTimer.TimeInSeconds) / 60));
            return CurrentExercise.CaloriesBurnedPerMinute * t;
        }

        private int? CalulateTime()
        {
            return (int)Math.Round((decimal)((CurrentExercise.Duration * 60 - exerciseTimer.TimeInSeconds) / 60));
        }


        private void ShowResults()
        {
            IsTimerVisible = false;
            GifPath = "";
            NextExerciseName = "";
            CurrentExerciseName = "Podsumowanie";
            ExercisesCounter = "";
            TotalTimeText = $"Czas ćwiczeń: {totalTime} min";
            Calories = $"Spalone kalorie: {calories} kcal";

            //Dodawanie odbytej sesji do bazy i wywolanie zdarzenia ktore zaaktualizuje interfejsy  
            //Nie trzeba nic aktualizowac w profilu z tego poziomu bo ogarniają to już triggery
            if (totalTime > 0)
            {
                try
                {
                    DbWorkoutSessions.AddWorkoutSession(UserSession.CurrentUserId.Value, this.workoutPlan.PlanId, DateTime.Now, totalTime.Value, calories.Value);
                    UserSession.CurrentUserTrainingAdded += 1;
                }
                catch (InvalidOperationException ex)
                {
                    UserSession.CurrentSqlError += 1; //Jesli serwer nie dziala to przelacza na okno z informacją o tym
                }
            }
        }

    }
}