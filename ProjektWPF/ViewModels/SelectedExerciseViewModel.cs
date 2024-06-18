using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class SelectedExerciseViewModel : ViewModelBase
    {
        private int _exerciseId;
        private string? _exerciseTitle;
        private string? _exerciseBodyPart;
        private string? _exerciseDescription;
        private string? _exerciseCalories;
        private string? _exerciseDifficultyLevel;
        private string? _exerciseGifPath;
        private MainViewModel _mainViewModel;

        public string? ExerciseDifficultyLevel
        {
            get => _exerciseDifficultyLevel;
            set
            {
                if (_exerciseDifficultyLevel != value)
                {
                    _exerciseDifficultyLevel = value;
                    OnPropertyChanged(nameof(ExerciseDifficultyLevel));
                }
            }
        }

        public string? ExerciseGifPath
        {
            get => _exerciseGifPath;
            set
            {
                if (_exerciseGifPath != value)
                {
                    _exerciseGifPath = value;
                    OnPropertyChanged(nameof(ExerciseGifPath));
                }
            }
        }

        public string? ExerciseTitle
        {
            get => _exerciseTitle;
            set
            {
                if (_exerciseTitle != value)
                {
                    _exerciseTitle = value;
                    OnPropertyChanged(nameof(ExerciseTitle));
                }
            }
        }

        public string? ExerciseBodyPart
        {
            get => _exerciseBodyPart;
            set
            {
                if (_exerciseBodyPart != value)
                {
                    _exerciseBodyPart = value;
                    OnPropertyChanged(nameof(ExerciseBodyPart));
                }
            }
        }

        public string? ExerciseDescription
        {
            get => _exerciseDescription;
            set
            {
                if (_exerciseDescription != value)
                {
                    _exerciseDescription = value;
                    OnPropertyChanged(nameof(ExerciseDescription));
                }
            }
        }

        public string? ExerciseCalories
        {
            get => _exerciseCalories;
            set
            {
                if (_exerciseCalories != value)
                {
                    _exerciseCalories = value;
                    OnPropertyChanged(nameof(ExerciseCalories));
                }
            }
        }

        public SelectedExerciseViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void Update(int? workoutId)
        {
            try
            {
                if (workoutId != null)
                {
                    Exercise exercise = DbExercises.GetOneExcercise(workoutId);
                    this.ExerciseTitle = exercise.Name;
                    this.ExerciseBodyPart = exercise.BodyPart;
                    this.ExerciseDescription = exercise.Description;
                    this.ExerciseCalories = exercise.CaloriesBurnedPerMinute.ToString() + " kcal/min";
                    this.ExerciseDifficultyLevel = exercise.DifficultyLevel;
                    this.ExerciseGifPath = exercise.GifPath;
                }
            }
            catch (InvalidOperationException ex)
            {
                UserSession.CurrentSqlError += 1; //Jesli serwer nie dziala to przelacza na okno z informacją o tym
            }
        }

        public void ChangeViewToExercises()
        {
            _mainViewModel.ChangeViewToExercises();
        }

        private ICommand _goBack = null;
        public ICommand GoBack
        {
            get
            {
                if (_goBack == null)
                {
                    _goBack = new RelayCommand(arg => { ChangeViewToExercises(); }, null);
                }
                return _goBack;
            }
        }
    }
}

