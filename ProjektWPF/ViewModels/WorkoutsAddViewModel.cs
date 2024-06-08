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
    public class WorkoutsAddViewModel : ViewModelBase
    {
        MainViewModel mainViewModel;
        public RelayCommand CreateWorkoutCommand { get; set; }

        public WorkoutsAddViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            CreateWorkoutCommand = new RelayCommand(execute => { CreateWorkout(); },
                canExecute =>{
                    if (this.Name != null) return true;
                    else return false; });
        }


        public void CreateWorkout()
        {
            var newWorkoutPlan = new WorkoutPlan(0, this.Name, UserSession.CurrentUserId.Value, 0, 0, this.Description, DateTime.Now);
            DbWorkoutPlans.AddWorkoutPlan(newWorkoutPlan);
            MessageBox.Show($"Plan {this.Name} został dodany.","Informacja",MessageBoxButton.OK, MessageBoxImage.Information);
            this.Name = "";
            this.Description = "";
        }

        
        private string? name; 
        public string? Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged();
            }
        }

        private string? description;

        public string? Description
        {
            get { return description; }
            set 
            { 
                description = value;
                OnPropertyChanged();
            }
        }
    }
}
