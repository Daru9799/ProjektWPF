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
        public RelayCommand ReturnToWorkoutPlansCommand { get; set; }


        public WorkoutsAddViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            CreateWorkoutCommand = new RelayCommand(execute => { CreateWorkout(); },
                canExecute =>{
                    if (this.Name != null && this.Name.Count()<100 && ((this.Description?.Count() ?? 0)<(Math.Pow(2,16))-1 )) return true; // zabezpieczenie
                    else return false; });
            ReturnToWorkoutPlansCommand = new RelayCommand(execute => { ReturnToWorkoutPlans(); },canExecute => { return true; });
        }


        public void CreateWorkout()
        {
            if (this.Description == null) this.Description = "";
            var newWorkoutPlan = new WorkoutPlan(0, this.Name, UserSession.CurrentUserId.Value, 0, 0, this.Description, DateTime.Now);
            try
            {
                DbWorkoutPlans.AddWorkoutPlan(newWorkoutPlan);
                MessageBox.Show($"Plan {this.Name} został dodany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Name = "";
                this.Description = "";
            }
            catch (InvalidOperationException ex)
            {
                UserSession.CurrentSqlError += 1; //Jesli serwer nie dziala to przelacza na okno z informacją o tym
            }
        }

        public void ReturnToWorkoutPlans()
        {
            this.Name = "";
            this.Description = "";
            mainViewModel.WorkoutsVm.Update();
            mainViewModel.CheangeViewToWorkoutsPanel();
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
