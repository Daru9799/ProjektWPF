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
        private List<WorkoutExercisePreview> workoutExercisesPreviewList;
        private List<Exercise> exercisesList;
        public RelayCommand ReturnToWorkoutPlansCommand { get; set; }

        public WorkoutMenageExercisesViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            ExercisesList = DbExercises.GetExercises();
            ReturnToWorkoutPlansCommand = new RelayCommand(execute => { ReturnToWorkoutPlans(); }, canExecute => { return true; });
        }

        public void ReturnToWorkoutPlans()
        {
            mainViewModel.WorkoutsVm.Update();
            mainViewModel.CheangeViewToWorkoutsPanel();
        }

        public void LoadWorkoutExercises(WorkoutPlan wp)
        {
            WorkoutExercisesPreviewList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
        }

        public List<WorkoutExercisePreview> WorkoutExercisesPreviewList
        {
            get { return workoutExercisesPreviewList; }
            set 
            { 
                workoutExercisesPreviewList = value; 
                OnPropertyChanged();
            }
        }

        public List<Exercise> ExercisesList
        {
            get { return exercisesList; }
            set 
            { 
                exercisesList = value; 
                OnPropertyChanged();
            }
        }


    }
}
