using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class WorkoutsViewModel : ViewModelBase
    {
        private List<WorkoutPlan> workoutPlansItemSource;
        private WorkoutPlan selectedWorkoutPlan = null;
        private MainViewModel mainViewModel;
        public RelayCommand SessionViewCommand { get; private set; }
        public WorkoutsViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            SessionViewCommand = new RelayCommand(execute => { mainViewModel.CheangeViewToSessionView(selectedWorkoutPlan); }, 
                canExecute => 
                {
                    if (SelectedWorkoutPlan == null) return false;
                    else return true;
                });
        }

        public void Update()
		{
            WorkoutPlansItemSource = DbWorkoutPlans.GetCurrentUserWorkouts();
            OnPropertyChanged();
        }

		public List<WorkoutPlan> WorkoutPlansItemSource
        {
			get { return workoutPlansItemSource; }
			set 
			{
				workoutPlansItemSource = value; 
				OnPropertyChanged();
			}
		}


		public WorkoutPlan SelectedWorkoutPlan
        {
			get { return selectedWorkoutPlan; }
			set 
			{ 
				selectedWorkoutPlan = value; 
				OnPropertyChanged();
			}
		}

	}
}
