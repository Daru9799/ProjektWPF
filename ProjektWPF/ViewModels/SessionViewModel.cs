using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;
using System.Windows.Input;

namespace ProjektWPF.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private WorkoutPlan workoutPlan;
        private List<WorkoutExercisePreview> exercisesList;

        private PersonalTimer exerciseTimer;
        private WorkoutExercisePreview currentExercise;
        private int currentExerciseIndex;
        private string gifPath;
        private string nextExerciseName;
        private string previousExerciseName;
        private string exercisesCounter;
        private string timerButtonText;

        public SessionViewModel(WorkoutPlan wp)
        {
            this.workoutPlan = wp;
            ExercisesList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
            exerciseTimer = new PersonalTimer();
            exerciseTimer.Tick += ExerciseTimer_Tick;
            exerciseTimer.TimerFinished += ExerciseTimer_TimerFinished;
            TimerButtonText = "Start";
            SetValues(ExercisesList);
        }

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

        private void Next()
        {
            if (currentExerciseIndex < exercisesList.Count - 1)
            {
                currentExerciseIndex++;
                SetValues(exercisesList);
                RestartTimer();
            }
        }

        private void SetValues(List<WorkoutExercisePreview> list)
        {
            CurrentExercise = list[currentExerciseIndex];
            CurrentExerciseName = list[currentExerciseIndex].Name;

            if (list.Count > currentExerciseIndex + 1)
            {
                NextExerciseName ="Następne:\n"+ list[currentExerciseIndex + 1].Name;
            }
            else
            {
                NextExerciseName = "Ostatnie!";
            }

            GifPath = list[currentExerciseIndex].GifPath;
            ExercisesCounter = $"{currentExerciseIndex + 1}/{ExercisesList.Count}";

            RestartTimer();
        }


        private void RestartTimer()
        {
            exerciseTimer.Restart(CurrentExercise.AverageTime);
            TimerButtonText = "Start";
        }

        private void ToggleTimer()
        {
            exerciseTimer.Toggle();
            TimerButtonText = exerciseTimer.IsRunning ? "Stop" : "Start";
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
