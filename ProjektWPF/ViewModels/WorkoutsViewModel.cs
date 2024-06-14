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
    public class WorkoutsViewModel : ViewModelBase
    {
        private MainViewModel mainViewModel;

        private List<WorkoutPlan> workoutPlansItemSource;
        private List<WorkoutExercisePreview> selectedWorkoutPlanExercises;
        private WorkoutPlan selectedWorkoutPlan = null;
        private WorkoutExercisePreview selectedExercise = null;

        public RelayCommand SessionViewCommand { get; private set; }
        //public RelayCommand DeleteWorkoutExerciseCommand { get; set; }
        public RelayCommand DeleteWorkoutCommand { get; set; }
        public RelayCommand ChangeViewToAddPanelCommand { get; set; }
        public RelayCommand ChangeViewToModifyWorkoutCommand { get;set; }
        public RelayCommand ChangeViewToMenageExercisesCommand { get; set; }


        public WorkoutsViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            ChangeViewToAddPanelCommand = new RelayCommand(execute => { ChangeToAddPanel(); }, canExecute => { return true; });

            SessionViewCommand = new RelayCommand(execute => { mainViewModel.CheangeViewToSessionView(selectedWorkoutPlan); }, 
                canExecute => 
                {
                    if ((SelectedWorkoutPlan == null) || (SelectedWorkoutPlanExercises.Count == 0)) return false;
                    else return true;
                });

            /*DeleteWorkoutExerciseCommand = new RelayCommand(execute => { DeleteWorkoutExercise(); },
                canExecute =>
                {
                    if (SelectedExercise == null) return false;
                    else return true;
                });*/

            DeleteWorkoutCommand = new RelayCommand(execute => { DeleteWorkout(); },
                canExecute =>
                {
                    if (SelectedWorkoutPlan == null) return false;
                    else return true;
                });

            ChangeViewToModifyWorkoutCommand = new RelayCommand(execute => { ChangeToModifyWorkoutPanel(); },
                canExecute =>
                {
                    if (SelectedWorkoutPlan == null) return false;
                    else return true;
                });

            ChangeViewToMenageExercisesCommand = new RelayCommand(execute => { ChangeToMenageExercisesPanel(); },
                canExecute =>
                {
                    if (SelectedWorkoutPlan == null) return false;
                    else return true;
                });
        }

        public void Update()
		{
            WorkoutPlansItemSource = DbWorkoutPlans.GetCurrentUserWorkouts();
            SelectedWorkoutPlan = null;
            SelectedExercise = null;
            OnPropertyChanged();
        }

        /*public void DeleteWorkoutExercise()
        {
            var result = MessageBox.Show($"Jesteś pewny że chcesz usunąć ćwiczenie {SelectedExercise.Name}?", "Ostrzeżenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) 
            {
                DbPlanExercises.DeleteWorkoutExercise(SelectedExercise);
                SelectedWorkoutPlanExercises = DbPlanExercises.GetWorkoutExercises(selectedWorkoutPlan.PlanId); // odświerzenie widoku ExercisePreview
            }
        }*/

        public void DeleteWorkout()
        {
            var result = MessageBox.Show($"Jesteś pewny że chcesz usunąć plan {selectedWorkoutPlan.Name}?", "Ostrzeżenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DbWorkoutPlans.DeleteWorkoutPlan(SelectedWorkoutPlan);
                    DbPlanExercises.DeleteWorkoutExercises(SelectedWorkoutPlan.PlanId);
                    Update(); // odświerzenie widoku Planów
                }
                catch (Exception ex)
                {
                    var result2 = MessageBox.Show($"Usunięcie tego planu usunie Sesje Treningowe z nim związane. Czy jesteś pewien, że chcesz usunąć ten plan?", "Ostrzeżenie", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                    if(result2 == MessageBoxResult.Yes)
                    {
                        DbWorkoutSessions.DeleteWorkoutSessions(SelectedWorkoutPlan.PlanId);
                        DbPlanExercises.DeleteWorkoutExercises(SelectedWorkoutPlan.PlanId);
                        DbWorkoutPlans.DeleteWorkoutPlan(SelectedWorkoutPlan);
                        Update(); // odświerzenie widoku Planów
                        DbUsers.UpdateStats(UserSession.CurrentUserId);
                        UserSession.CurrentUserTrainingAdded += 1;
                    }
                }
            }
        }

        public void ChangeToAddPanel()
        {
            mainViewModel.CheangeViewToWorkoutAddPanel();
        }

        public void ChangeToModifyWorkoutPanel()
        {
            mainViewModel.CheangeViewToWorkoutModifyPanel(SelectedWorkoutPlan);
        }

        public void ChangeToMenageExercisesPanel()
        {
            mainViewModel.ChangeViewToWorkoutMenageExercisesPanel(SelectedWorkoutPlan);
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
                if(selectedWorkoutPlan== null) SelectedWorkoutPlanExercises = new List<WorkoutExercisePreview>();
                else SelectedWorkoutPlanExercises = DbPlanExercises.GetWorkoutExercises(selectedWorkoutPlan.PlanId);
                OnPropertyChanged();
			}
		}

		
		public List<WorkoutExercisePreview> SelectedWorkoutPlanExercises
        {
			get { return selectedWorkoutPlanExercises; }
			set 
            { 
                selectedWorkoutPlanExercises = value;
                OnPropertyChanged();
            }
		}


        public WorkoutExercisePreview SelectedExercise
        {
            get { return selectedExercise; }
            set 
            { 
                selectedExercise = value;
                OnPropertyChanged();
            }
        }
    }
}
