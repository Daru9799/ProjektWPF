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
    public class ExercisesViewModel : ViewModelBase
    {
        public event EventHandler<ExerciseChangedEventArgs> CurrentExerciseChanged;

        private MainViewModel _mainViewModel;
        private List<Exercise> exercisesList;
        private Exercise? currentExercise = null;

        public ExercisesViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ExercisesList = DbExercises.GetExercises();
            CurrentExercise = ExercisesList[0];
        }

        public List<Exercise> ExercisesList
        {
            get => exercisesList;
            set
            {
                exercisesList = value;
                OnPropertyChanged();
            }
        }

        public Exercise? CurrentExercise
        {
            get { return currentExercise; }
            set
            {
                if (currentExercise != value)
                {
                    currentExercise = value;
                    OnPropertyChanged();
                    OnCurrentExerciseChanged(new ExerciseChangedEventArgs(value));
                }
            }
        }

        protected virtual void OnCurrentExerciseChanged(ExerciseChangedEventArgs e)
        {
            CurrentExerciseChanged?.Invoke(this, e);
        }

        private void ChangeViewToSelected()
        {
            _mainViewModel.ChangerViewToSelectedExercise();
            _mainViewModel.SelectedExerciseVm.Update(CurrentExercise?.ExerciseId);
        }


        private ICommand? _detailsClikc = null;

        public ICommand? DetailClikc
        {
            get
            {
                if (_detailsClikc == null)
                {
                    _detailsClikc = new RelayCommand(arg => { ChangeViewToSelected(); }, null);
                }
                return _detailsClikc;
            }
        }
    }

    public class ExerciseChangedEventArgs : EventArgs
    {
        public Exercise? NewExercise { get; }

        public ExerciseChangedEventArgs(Exercise? newExercise)
        {
            NewExercise = newExercise;
        }
    }
}
