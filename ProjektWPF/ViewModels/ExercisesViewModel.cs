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
        private MainViewModel _mainViewModel;
        private List<Exercise> exercisesList;
        private Exercise currentExercise;

        public string? CurrentExerciseName
        {
            get => currentExercise.Name;
            set
            {
                if (currentExercise.Name != value)
                {
                    currentExercise.Name = value;
                    OnPropertyChanged(nameof(CurrentExerciseName));
                }
            }
        }

        public ExercisesViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ExercisesList = DbExercises.GetExercises();

        }

        public List<Exercise> ExercisesList
        {
            get => exercisesList;
            set
            {
                exercisesList = value;
                OnPropertyChanged(); }
        }

        private void ChangeViewToSelected()
        {
            _mainViewModel.ChangerViewToSelectedExercise();
        }


        private ICommand? _detailsClikc = null;


        public ICommand? DetailClikc
        {
            get
            {
                if(_detailsClikc == null)
                {
                    _detailsClikc=new RelayCommand(arg => { ChangeViewToSelected(); },null);
                }
                return _detailsClikc;
            }
        }
    }
}
