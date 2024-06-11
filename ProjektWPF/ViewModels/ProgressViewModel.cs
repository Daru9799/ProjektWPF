using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class ProgressViewModel : ViewModelBase
    {
        private string? _weightText { get; set; }
        public string? WeightText
        {
            get => _weightText;
            set
            {
                if (_weightText != value)
                {
                    _weightText = value;
                    OnPropertyChanged(nameof(WeightText));
                }
            }
        }
        private string? _lastMeasurementText { get; set; }
        public string? LastMeasurementText
        {
            get => _lastMeasurementText;
            set
            {
                if (_lastMeasurementText != value)
                {
                    _lastMeasurementText = value;
                    OnPropertyChanged(nameof(LastMeasurementText));
                }
            }
        }
        private string? _bmiText { get; set; }
        public string? BmiText
        {
            get => _bmiText;
            set
            {
                if (_bmiText != value)
                {
                    _bmiText = value;
                    OnPropertyChanged(nameof(BmiText));
                }
            }
        }
        private string? _bodyFatText { get; set; }
        public string? BodyFatText
        {
            get => _bodyFatText;
            set
            {
                if (_bodyFatText != value)
                {
                    _bodyFatText = value;
                    OnPropertyChanged(nameof(BodyFatText));
                }
            }
        }
        private string? _weightDifferenceText { get; set; }
        public string? WeightDifferenceText
        {
            get => _weightDifferenceText;
            set
            {
                if (_weightDifferenceText != value)
                {
                    _weightDifferenceText = value;
                    OnPropertyChanged(nameof(WeightDifferenceText));
                }
            }
        }

        private MainViewModel _mainViewModel;
        public ProgressViewModel(MainViewModel mainViewModel)
        {
            UserSession.UserIdChanged += OnUserIdChanged;
            UserSession.UserWeightChanged += OnUserWeightChanged;
            _mainViewModel = mainViewModel;
        }

        private ICommand _newClick = null;
        public ICommand NewClick
        {
            get
            {
                if (_newClick == null)
                {
                    _newClick = new RelayCommand(
                        arg => { NewMeasurement(); }, null);
                }

                return _newClick;
            }
        }
        private void NewMeasurement()
        {
            _mainViewModel.ChangeViewToNewMeasurement();
        }

        private void OnUserWeightChanged(bool? x)
        {
            UserProgress currentProgress = DbUserProgress.GetLatestProgressForUser(UserSession.CurrentUserId);
            if(currentProgress != null)
            {
                this.WeightText = "Aktualna waga: " + currentProgress.Weight + " kg";
                this.LastMeasurementText = currentProgress.Date.Value.ToString("d");
                this.BmiText = "BMI: " + currentProgress.Bmi;
                this.BodyFatText = "Tkanka Tłuszcz: " + currentProgress.BodyFatPercentage + "%";

                float? oldWeight = DbUserProgress.GetSecondLatestWeightForUser(UserSession.CurrentUserId);
                if (oldWeight != null)
                {
                    float? difference = Calculator.CalculateWeightDifference(oldWeight, currentProgress.Weight);
                    if (difference >0) 
                    {
                        this.WeightDifferenceText = $"Zmiana wagi: +{difference:F1} kg";
                    }
                    else
                    {
                        this.WeightDifferenceText = $"Zmiana wagi: {difference:F1} kg";
                    }
                }
                else
                {
                    this.WeightDifferenceText = "Zmiana wagi: 0 kg";
                }
            }
        }

        private void OnUserIdChanged(int? id)
        {
            //Na razie tu ale potem przerzuci sie do aktualizacji po odbytym treningu
            if (id != null)
            {
                this.WeekStatsText = DbWorkoutSessions.CountTrainingsLastWeek(UserSession.CurrentUserId).ToString();
                this.MonthStatsText = DbWorkoutSessions.CountTrainingsLastMonth(UserSession.CurrentUserId).ToString();
                this.YearStatsText = DbWorkoutSessions.CountTrainingsLastYear(UserSession.CurrentUserId).ToString();

                int bestPlan = DbWorkoutSessions.GetMostFrequentPlanIdForUser(UserSession.CurrentUserId);
                this.BestPlanText = DbWorkoutPlans.GetWorkoutName(bestPlan);
            }
        }

        //OSIAGNIECIA
        private string? _weekStatsText { get; set; }
        public string? WeekStatsText
        {
            get => _weekStatsText;
            set
            {
                if (_weekStatsText != value)
                {
                    _weekStatsText = value;
                    OnPropertyChanged(nameof(WeekStatsText));
                }
            }
        }
        private string? _monthStatsText { get; set; }
        public string? MonthStatsText
        {
            get => _monthStatsText;
            set
            {
                if (_monthStatsText != value)
                {
                    _monthStatsText = value;
                    OnPropertyChanged(nameof(MonthStatsText));
                }
            }
        }
        private string? _yearStatsText { get; set; }
        public string? YearStatsText
        {
            get => _yearStatsText;
            set
            {
                if (_yearStatsText != value)
                {
                    _yearStatsText = value;
                    OnPropertyChanged(nameof(YearStatsText));
                }
            }
        }
        private string? _bestPlanText { get; set; }
        public string? BestPlanText
        {
            get => _bestPlanText;
            set
            {
                if (_bestPlanText != value)
                {
                    _bestPlanText = value;
                    OnPropertyChanged(nameof(BestPlanText));
                }
            }
        }
    }
}
