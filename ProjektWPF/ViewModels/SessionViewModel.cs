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

namespace ProjektWPF.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private WorkoutPlan workoutPlan;
        private List<WorkoutExercisePreview> exercisesList;
        private  MainViewModel mainViewModel;
        private PersonalTimer exerciseTimer;
        private WorkoutExercisePreview currentExercise;
        private int currentExerciseIndex;
        private string gifPath;
        private string nextExerciseName;
        private string previousExerciseName;
        private string exercisesCounter;
        private string timerButtonText;

        public SessionViewModel(WorkoutPlan wp,MainViewModel mv)
        {
            mainViewModel = mv;
            this.workoutPlan = wp;
            ExercisesList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
            exerciseTimer = new PersonalTimer();
            exerciseTimer.Tick += ExerciseTimer_Tick;
            exerciseTimer.TimerFinished += ExerciseTimer_TimerFinished;
            TimerButtonText = "Start";
            SetValues(ExercisesList);
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
            get { return exerciseTimer.FormattedTime; }
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

        private bool isNextExerciseAvailable;
        public bool IsNextExerciseAvailable
        {
            get { return isNextExerciseAvailable; }
            set
            {
                if (isNextExerciseAvailable != value)
                {
                    isNextExerciseAvailable = value;
                    OnPropertyChanged(nameof(IsNextExerciseAvailable));
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



        #endregion

        private ICommand nextExercise = null;

        public ICommand NextExercise
        {
            get
            {
                if (nextExercise == null)
                {
                    nextExercise = new RelayCommand(arg => { Next(); }, null);
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
                if(goBack == null)
                {
                    goBack=new RelayCommand(arg => { ChangeView(); },null);
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
            if (currentExerciseIndex < exercisesList.Count - 1)
            {
                currentExerciseIndex++;
                SetValues(exercisesList);
                RestartTimer();
            }
            else
            {
                IsStartStop = false;
            }

        }

        private void SetValues(List<WorkoutExercisePreview> list)
        {
            CurrentExercise = list[currentExerciseIndex];
            CurrentExerciseName = list[currentExerciseIndex].Name;
            IsStartStop = true;

            if (list.Count > currentExerciseIndex + 1)
            {
                NextExerciseName ="Następne:\n"+ list[currentExerciseIndex + 1].Name;
                IsNextExerciseAvailable = true;
                
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
                    IsNextExerciseAvailable = false;
                }
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
    }
}
