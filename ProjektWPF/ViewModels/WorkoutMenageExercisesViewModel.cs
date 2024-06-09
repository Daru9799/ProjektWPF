using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class WorkoutMenageExercisesViewModel : ViewModelBase
    {
        MainViewModel mainViewModel;

        public RelayCommand ReturnToWorkoutPlansCommand { get; set; }

        public WorkoutMenageExercisesViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;

            ReturnToWorkoutPlansCommand = new RelayCommand(execute => { ReturnToWorkoutPlans(); }, canExecute => { return true; });

        }

        public void ReturnToWorkoutPlans()
        {
            mainViewModel.WorkoutsVm.Update();
            mainViewModel.CheangeViewToWorkoutsPanel();
        }
    }
}
