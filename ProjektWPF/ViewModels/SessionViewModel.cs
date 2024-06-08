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
    public class SessionViewModel : ViewModelBase
    {
        private WorkoutPlan workoutPlan;
        private List<WorkoutExercisePreview> exercisesList;

        public SessionViewModel(WorkoutPlan wp)
        {
            this.workoutPlan = wp;
            ExercisesList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
        }
        

        public List<WorkoutExercisePreview> ExercisesList
        {
            get { return exercisesList; }
            set { 
                exercisesList = value; 
                OnPropertyChanged();
            }
        }



    }
}
