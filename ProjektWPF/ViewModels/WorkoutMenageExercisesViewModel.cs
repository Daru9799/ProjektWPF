using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.TextFormatting;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class WorkoutMenageExercisesViewModel : ViewModelBase
    {
        MainViewModel mainViewModel;
        private ObservableCollection<WorkoutExercisePreview> workoutExercisesPreviewList;
        private ObservableCollection<Exercise> exercisesList;
        //public ObservableCollection<Exercise> FilteredExercises { get; set; }  // Binding listy ćiwczeń jest do tego teraz !!!!!!!!!!!!!!!!!!!
        private Exercise selectedExercise;
        private WorkoutExercisePreview selectedWorkoutExercise;
        private string planSumUpText;
        private int durationValue = 0;
        public WorkoutPlan SelectedWorkout { set; get; }


        public RelayCommand ReturnToWorkoutPlansCommand { get; set; }
        public RelayCommand SaveExercisesChangesCommand { get; set; }
        public RelayCommand AddExerciseToWorkoutCommand { get; set; }
        public RelayCommand DeleteExerciseFromWorkoutCommand { get; set; }
        public RelayCommand MoveExerciseUpCommand { get; set; }
        public RelayCommand MoveExerciseDownCommand { get; set; }

        public WorkoutMenageExercisesViewModel(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;

            ExercisesList = new ObservableCollection<Exercise>(DbExercises.GetExercises().OrderBy(x => x.Name));
            FilteredExercises = new ObservableCollection<Exercise>(ExercisesList);
            DiffLevelCheckBoxList = new List<CheckBox>();
            BodyPartsCheckBoxList = new List<CheckBox>();

            ReturnToWorkoutPlansCommand = new RelayCommand(execute => { ReturnToWorkoutPlans(); }, canExecute => { return true; });
            SaveExercisesChangesCommand = new RelayCommand(execute => { SaveExerciseChanges(); }, canExecute => { return true; });
            AddExerciseToWorkoutCommand = new RelayCommand(execute => { AddExerciseToWorkout(); }, 
                canExecute => 
                {
                    if (SelectedExercise == null) return false;
                    else return true;
                });
            DeleteExerciseFromWorkoutCommand = new RelayCommand(execute => { DeleteExerciseFromWorkout(); },
                canExecute =>
                {
                    if (SelectedWorkoutExercise == null) return false;
                    else return true;
                });
            MoveExerciseUpCommand = new RelayCommand(execute => { MoveUp(); }, 
                canExecute => 
                {
                    if (SelectedWorkoutExercise == null) return false;
                    else return true;
                });
            MoveExerciseDownCommand = new RelayCommand(execute => { MoveDown(); },
                canExecute =>
                {
                    if (SelectedWorkoutExercise == null) return false;
                    else return true;
                });
        }

        public void ReturnToWorkoutPlans()
        {

            mainViewModel.WorkoutsVm.Update();
            mainViewModel.CheangeViewToWorkoutsPanel();
        }

        public void LoadWorkoutExercises(WorkoutPlan wp)
        {

            SelectedWorkout = wp;
            SelectedExercise = null;
            SelectedWorkoutExercise = null;
            DurationValue = 0;
            var tmpList = DbPlanExercises.GetWorkoutExercises(wp.PlanId);
            WorkoutExercisesPreviewList = new ObservableCollection<WorkoutExercisePreview>(tmpList);
            FilteredExercises = new ObservableCollection<Exercise>(ExercisesList);
            PopulateComboBox();
            UpdatePlanSumUpText();
        }

        public void MoveUp()
        {
            int currentIndex = WorkoutExercisesPreviewList.IndexOf(SelectedWorkoutExercise);
            if (currentIndex > 0)
            {
                var itemToMoveUp = WorkoutExercisesPreviewList[currentIndex];
                WorkoutExercisesPreviewList.RemoveAt(currentIndex);
                WorkoutExercisesPreviewList.Insert(currentIndex - 1, itemToMoveUp);

                // Aktualizacja właściwości Order dla poprawności wyświetlania
                for (int i = 0; i < WorkoutExercisesPreviewList.Count; i++)
                {
                    WorkoutExercisesPreviewList[i].Order = i+1;
                }
                
                // Ustawienie ponownie zaznaczonego elementu
                SelectedWorkoutExercise = itemToMoveUp;
            }
        }

        public void MoveDown()
        {
            int currentIndex = WorkoutExercisesPreviewList.IndexOf(SelectedWorkoutExercise);
            if (currentIndex < WorkoutExercisesPreviewList.Count - 1 && currentIndex != -1)
            {
                var itemToMoveDown = WorkoutExercisesPreviewList[currentIndex];
                WorkoutExercisesPreviewList.RemoveAt(currentIndex);
                WorkoutExercisesPreviewList.Insert(currentIndex + 1, itemToMoveDown);

                // Aktualizacja właściwości Order dla poprawności wyświetlania
                for (int i = 0; i < WorkoutExercisesPreviewList.Count; i++)
                {
                    WorkoutExercisesPreviewList[i].Order = i + 1;
                }

                // Ustawienie ponownie zaznaczonego elementu
                SelectedWorkoutExercise = itemToMoveDown;
            }

        }

        public void SaveExerciseChanges()
        {
            DbPlanExercises.SaveModifiedPlanExercises(WorkoutExercisesPreviewList);
            DbWorkoutPlans.UpdateWorkoutData(SelectedWorkout.PlanId);
            MessageBox.Show($"Plan '{SelectedWorkout.Name}' został zmieniony.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void AddExerciseToWorkout()
        {
            var tmpOrder = workoutExercisesPreviewList.Count() + 1;
            var tmpWorkoutEP= new WorkoutExercisePreview(SelectedExercise,SelectedWorkout.PlanId,tmpOrder,DurationValue);
            WorkoutExercisesPreviewList.Add(tmpWorkoutEP);
            UpdatePlanSumUpText();
        }

        public void DeleteExerciseFromWorkout()
        {
            WorkoutExercisesPreviewList.RemoveAt(WorkoutExercisesPreviewList.IndexOf(SelectedWorkoutExercise));
            for (int i = 0; i < WorkoutExercisesPreviewList.Count; i++)
            {
                WorkoutExercisesPreviewList[i].Order = i + 1;
            }
            UpdatePlanSumUpText();
        }

        public void UpdatePlanSumUpText()
        {
            string tmpString = "Sumaryczny czas: ";
            int tmpCzas = 0;
            int tmpKcal = 0;
            foreach(var i in WorkoutExercisesPreviewList)
            {
                tmpCzas += i.Duration.Value;
                tmpKcal += i.Duration.Value * i.CaloriesBurnedPerMinute.Value;
            }
            PlanSumUpText = $"Sumaryczny czas: {tmpCzas}min, sumaryczne Kcal: {tmpKcal}";
        }

        public Exercise SelectedExercise
        {
            get { return selectedExercise; }
            set
            {
                selectedExercise = value;
                SelectedWorkoutExercise = null;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<WorkoutExercisePreview> WorkoutExercisesPreviewList
        {
            get { return workoutExercisesPreviewList; }
            set 
            { 
                workoutExercisesPreviewList = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Exercise> ExercisesList
        {
            get { return exercisesList; }
            set 
            { 
                exercisesList = value; 
                OnPropertyChanged();
            }
        }

        public int DurationValue
        {
            get { return durationValue; }
            set 
            {
                durationValue = value;
                if (SelectedWorkoutExercise != null)
                {
                    SelectedWorkoutExercise.Duration = durationValue;
                    UpdatePlanSumUpText(); // <----

                }
                OnPropertyChanged();
            }
        }

        
        public WorkoutExercisePreview SelectedWorkoutExercise
        {
            get { return selectedWorkoutExercise; }
            set
            {
                if (selectedWorkoutExercise != value)
                {
                    selectedWorkoutExercise = value;
                    if (selectedWorkoutExercise != null)
                    {
                        DurationValue = selectedWorkoutExercise.Duration ?? 0;
                    }
                    OnPropertyChanged();
                }
            }
        }

        public string PlanSumUpText
        {
            get { return planSumUpText; }
            set 
            { 
                planSumUpText = value;
                OnPropertyChanged();
            }
        }


        private List<CheckBox> diffLevelCheckBoxList;
        public List<CheckBox> DiffLevelCheckBoxList
        {
            get { return diffLevelCheckBoxList; }
            set 
            { 
                diffLevelCheckBoxList = value; 
                OnPropertyChanged();
            }
        }

        private List<CheckBox> bodyPartsCheckBoxList;
        public List<CheckBox> BodyPartsCheckBoxList
        {
            get { return bodyPartsCheckBoxList; }
            set 
            { 
                bodyPartsCheckBoxList = value; 
                OnPropertyChanged();
            }
        }

        public void PopulateComboBox()
        {
            DiffLevelCheckBoxList.Clear();
            BodyPartsCheckBoxList.Clear();

            string[] diffLevelsText = { "Łatwy", "Średni", "Trudny" };
            foreach (var el in diffLevelsText)
            {
                CheckBox tmpCheckBox = new CheckBox();
                tmpCheckBox.Content = el;
                tmpCheckBox.Checked += (s, e) => FilterExercises();
                tmpCheckBox.Unchecked += (s, e) => FilterExercises();
                DiffLevelCheckBoxList.Add(tmpCheckBox);
            }

            string[] bodyPartsText = { "Całe ciało", "Górna część ciała", "Dolna część ciała", "Ramiona",
                                        "Klatka piersiowa", "Plecy", "Barki", "Nogi", "Pośladki", "Mięśnie brzucha" };
            foreach (var el in bodyPartsText)
            {
                CheckBox tmpCheckBox = new CheckBox();
                tmpCheckBox.Content = el;
                tmpCheckBox.Checked += (s, e) => FilterExercises();
                tmpCheckBox.Unchecked += (s, e) => FilterExercises();
                BodyPartsCheckBoxList.Add(tmpCheckBox);
            }

        }


        private ObservableCollection<Exercise> filteredExercises;

        public ObservableCollection<Exercise> FilteredExercises
        {
            get { return filteredExercises; }
            set 
            { 
                filteredExercises = value; 
                OnPropertyChanged();
            }
        }



        private void FilterExercises()
        {
            var selectedLevels = DiffLevelCheckBoxList
                                .Where(cb => cb.IsChecked == true)
                                .Select(cb => cb.Content.ToString())
                                .ToList();

            var selectedBodyParts =  BodyPartsCheckBoxList
                                    .Where(cb => cb.IsChecked == true)
                                    .Select(cb => cb.Content.ToString())
                                    .ToList();

            int countCheckedLevels = DiffLevelCheckBoxList.Where(cb => cb.IsChecked == true).Count();
            int countCheckedBodyParts= BodyPartsCheckBoxList.Where(cb => cb.IsChecked == true).Count();

            if (countCheckedLevels == 0 && countCheckedBodyParts == 0)
            {
                FilteredExercises = new ObservableCollection<Exercise>(ExercisesList);
            }
            else if(countCheckedLevels > 0 && countCheckedBodyParts == 0)
            {
                var filtered = ExercisesList.Where(e => selectedLevels.Contains(e.DifficultyLevel)).ToList();

                FilteredExercises.Clear();
                foreach (var exercise in filtered)
                {
                    FilteredExercises.Add(exercise);
                }
            }
            else if(countCheckedLevels == 0 && countCheckedBodyParts > 0)
            {
                var filtered = ExercisesList.Where(e => selectedBodyParts.Contains(e.BodyPart)).ToList();

                FilteredExercises.Clear();
                foreach (var exercise in filtered)
                {
                    FilteredExercises.Add(exercise);
                }
            }
            else
            {
                var filtered = ExercisesList.Where(e => (selectedLevels.Contains(e.DifficultyLevel) && selectedBodyParts.Contains(e.BodyPart)) ).ToList();

                FilteredExercises.Clear();
                foreach (var exercise in filtered)
                {
                    FilteredExercises.Add(exercise);
                }
            }
        }
    }
}
