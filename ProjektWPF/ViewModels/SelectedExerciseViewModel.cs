using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class SelectedExerciseViewModel:ViewModelBase
    {
        #region zmienne
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
#endregion

        public SelectedExerciseViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public SelectedExerciseViewModel(int id)
        {
            _exerciseId = id;
            SetValues(id);
        }

        private void SetValues(int? workoutId)
        {
            if(workoutId!=null)
            {
                Exercise exercise = DbExercises.GetOneExcercise(workoutId);
                this.ExerciseTitle = exercise.Name;
                this.ExerciseBodyPart = exercise.BodyPart;
                this.ExerciseDescription = exercise.Description;
                this.ExerciseCalories= exercise.CaloriesBurnedPerMinute.ToString()+" kcal/min";
                this.ExerciseDifficultyLevel=exercise.DifficultyLevel;
                this.ExerciseGifPath = exercise.GifPath;
            }
        }








    }
}
