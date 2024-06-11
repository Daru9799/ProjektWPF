using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class NewMeasurementViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public NewMeasurementViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
        private ICommand _cancelClick = null;
        public ICommand CancelClick
        {
            get
            {
                if (_cancelClick == null)
                {
                    _cancelClick = new RelayCommand(
                        arg => { Cancel(); }, null); ;
                }

                return _cancelClick;
            }
        }
        private void Cancel()
        {
            _mainViewModel.ChangeViewToProgress();
        }

        private string? _weight { get; set; }
        public string? Weight
        {
            get => _weight;
            set
            {
                if (value != null)
                {
                    string pattern = @"^([3-9]{1}[0-9]|1[0-9]{2}|2[0-9]{2}|300)(?:,\d)?$";
                    if (!Regex.IsMatch(value, pattern))
                    {
                        WeightWarningVisibility = Visibility.Visible;
                        WeightWarningText = "Niepoprawna waga (dokladność prosimy podać do jednego miejsca po przecinku)";
                    }
                    else
                    {
                        WeightWarningVisibility = Visibility.Hidden;
                        WeightWarningText = "";
                    }

                }
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        private ICommand _newWeightClick = null;
        public ICommand NewWeightClick
        {
            get
            {
                if (_newWeightClick == null)
                {
                    _newWeightClick = new RelayCommand(
                        arg => { AddNewWeight(); }, arg => CanAddNewWeight()); ;
                }

                return _newWeightClick;
            }
        }
        
        public void AddNewWeight()
        {
            //Aktualizacja wagi usera 
            DbUsers.UpdateWeight(UserSession.CurrentUserId, float.Parse(this.Weight, NumberStyles.Float, CultureInfo.GetCultureInfo("pl-PL")));
            //Dodanie nowego wpisu
            CreateNewMeasurement(UserSession.CurrentUserId);

            //Aktualizacja stanu okien XD
            if(UserSession.CurrentUserWeight == true)
            {
                UserSession.CurrentUserWeight = false;
            } 
            else
            {
                UserSession.CurrentUserWeight = true;
            }
            
            Cancel();
        }

        private bool CanAddNewWeight()
        {
            if (this.Weight != null)
            {
                if (WeightWarningText == "")
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateNewMeasurement(int? id)
        {
            User user1 = DbUsers.GetUserFromDb(UserSession.CurrentUserId);

            float? bmi = Calculator.CalculateBmi(float.Parse(this.Weight, NumberStyles.Float, CultureInfo.GetCultureInfo("pl-PL")), user1.Height);
            float? bodyFat = Calculator.CalculateBodyFat(float.Parse(this.Weight, NumberStyles.Float, CultureInfo.GetCultureInfo("pl-PL")), bmi, Calculator.CalculateAge(user1.BirthDate), Calculator.IsMale(user1.Gender));
            float roundedBmi = (float)Math.Round(bmi.Value, 1);
            float roundedBodyFat = (float)Math.Round(bodyFat.Value, 1);
            UserProgress newMeasurement = new UserProgress(0, (int)id, DateTime.Now, float.Parse(this.Weight, NumberStyles.Float, CultureInfo.GetCultureInfo("pl-PL")), roundedBodyFat, roundedBmi);
            DbUserProgress.AddMeasurementToDb(newMeasurement);
        }


        //Reakcja na błędy
        private Visibility _weightWarningVisibility = Visibility.Collapsed;
        public Visibility WeightWarningVisibility
        {
            get { return _weightWarningVisibility; }
            set
            {
                _weightWarningVisibility = value;
                OnPropertyChanged(nameof(WeightWarningVisibility));
            }
        }
        private string _weightWarningText;
        public string WeightWarningText
        {
            get { return _weightWarningText; }
            set
            {
                _weightWarningText = value;
                OnPropertyChanged(nameof(WeightWarningText));
            }
        }
    }
}
