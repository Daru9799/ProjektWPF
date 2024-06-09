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
    public class WorkoutModifyViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel;

        private WorkoutPlan workoutPlan;
        public RelayCommand ReturnToWorkoutPlansCommand { get; set; }
        public RelayCommand ModifyWorkoutCommand { get; set; }

        public WorkoutModifyViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            ModifyWorkoutCommand = new RelayCommand(execute => { ModifyWorkoutPlan(); },
                canExecute => {
                    if (this.Name != null) return true;
                    else return false;
                });
            ReturnToWorkoutPlansCommand = new RelayCommand(execute => { ReturnToWorkoutPlans(); }, canExecute => { return true; });
        }

        public void ReturnToWorkoutPlans()
        {
            this.Name = "";
            this.Description = "";
            mainViewModel.WorkoutsVm.Update();
            mainViewModel.CheangeViewToWorkoutsPanel();
        }

        public void LoadDataToModify(WorkoutPlan wp)
        {
            workoutPlan = wp;
        }

        public void ModifyWorkoutPlan()
        {
            DbWorkoutPlans.ModifyWorkoutPlan(workoutPlan);
            MessageBox.Show($"Plan {this.Name} został zmieniony.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public string? Name
        {
            get { return workoutPlan.Name; }
            set
            {
                workoutPlan.Name = value;
                OnPropertyChanged();
            }
        }

        public string? Description
        {
            get { return workoutPlan.Description; }
            set
            {
                workoutPlan.Description = value;
                OnPropertyChanged();
            }
        }

    }
}
