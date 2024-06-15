using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
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
        private Exercise? currentExercise = null;

        public ExercisesViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            ExercisesList = DbExercises.GetExercises();
            FilteredExercises = new ObservableCollection<Exercise>(ExercisesList);
            //CurrentExercise = ExercisesList[0];

            DiffLevelCheckBoxList = new List<CheckBox>();
            BodyPartsCheckBoxList = new List<CheckBox>();
            PopulateComboBox();
        }

        public List<Exercise> ExercisesList
        {
            get => exercisesList;
            set
            {
                exercisesList = value;
                OnPropertyChanged();
            }
        }

        public Exercise? CurrentExercise
        {
            get { return currentExercise; }
            set
            {
                if (currentExercise != value)
                {
                    currentExercise = value;
                    OnPropertyChanged();
                    ChangeViewToSelected();
                }
            }
        }

        private void ChangeViewToSelected()
        {
            try
            {
                if (CurrentExercise != null)
                {
                    _mainViewModel.ChangerViewToSelectedExercise();
                    _mainViewModel.SelectedExerciseVm.Update(CurrentExercise.ExerciseId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error changing view: {ex.Message}");
            }
        }

        //private ICommand? _detailsClick = null;
        //public ICommand? DetailClick
        //{
        //    get
        //    {
        //        if (_detailsClick == null)
        //        {
        //            _detailsClick = new RelayCommand(arg => { ChangeViewToSelected(); }, null);
        //        }
        //        return _detailsClick;
        //    }
        //}

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
                CheckBox tmpCheckBox = new CheckBox
                {
                    Content = el
                };
                tmpCheckBox.Checked += (s, e) => FilterExercises();
                tmpCheckBox.Unchecked += (s, e) => FilterExercises();
                DiffLevelCheckBoxList.Add(tmpCheckBox);
            }

            string[] bodyPartsText = { "Całe ciało", "Górna część ciała", "Dolna część ciała", "Ramiona",
                                        "Klatka piersiowa", "Plecy", "Barki", "Nogi", "Pośladki", "Mięśnie brzucha" };
            foreach (var el in bodyPartsText)
            {
                CheckBox tmpCheckBox = new CheckBox
                {
                    Content = el
                };
                tmpCheckBox.Checked += (s, e) => FilterExercises();
                tmpCheckBox.Unchecked += (s, e) => FilterExercises();
                BodyPartsCheckBoxList.Add(tmpCheckBox);
            }
        }

        private void FilterExercises()
        {
            var selectedLevels = DiffLevelCheckBoxList
                                .Where(cb => cb.IsChecked == true)
                                .Select(cb => cb.Content.ToString())
                                .ToList();

            var selectedBodyParts = BodyPartsCheckBoxList
                                    .Where(cb => cb.IsChecked == true)
                                    .Select(cb => cb.Content.ToString())
                                    .ToList();

            int countCheckedLevels = DiffLevelCheckBoxList.Count(cb => cb.IsChecked == true);
            int countCheckedBodyParts = BodyPartsCheckBoxList.Count(cb => cb.IsChecked == true);

            if (countCheckedLevels == 0 && countCheckedBodyParts == 0)
            {
                FilteredExercises = new ObservableCollection<Exercise>(ExercisesList);
            }
            else if (countCheckedLevels > 0 && countCheckedBodyParts == 0)
            {
                var filtered = ExercisesList.Where(e => selectedLevels.Contains(e.DifficultyLevel)).ToList();

                FilteredExercises.Clear();
                foreach (var exercise in filtered)
                {
                    FilteredExercises.Add(exercise);
                }
            }
            else if (countCheckedLevels == 0 && countCheckedBodyParts > 0)
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
                var filtered = ExercisesList.Where(e => selectedLevels.Contains(e.DifficultyLevel) && selectedBodyParts.Contains(e.BodyPart)).ToList();

                FilteredExercises.Clear();
                foreach (var exercise in filtered)
                {
                    FilteredExercises.Add(exercise);
                }
            }
        }



        private void ClearFilters()
        {
            foreach (var checkBox in DiffLevelCheckBoxList.Concat(BodyPartsCheckBoxList))
            {
                checkBox.IsChecked = false;
            }
            FilterExercises();
        }
    }
}




