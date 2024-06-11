﻿using System;
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

        private void OnUserIdChanged(int? newUserId)
        {
            if (newUserId != null)
            {
                UserProgress currentProgress = DbUserProgress.GetLatestProgressForUser(UserSession.CurrentUserId);
                this.WeightText = "Aktualna waga: " + currentProgress.Weight + " kg";
                this.LastMeasurementText = currentProgress.Date.Value.ToString("d");
                this.BmiText = "BMI: " + currentProgress.Bmi;
                this.BodyFatText = "Tkanka Tłuszcz: " + currentProgress.BodyFatPercentage + "%";

                float? oldWeight = DbUserProgress.GetSecondLatestWeightForUser(UserSession.CurrentUserId);
                if(oldWeight != null)
                {
                    float? difference = Calculator.CalculateWeightDifference(oldWeight, currentProgress.Weight);
                    this.WeightDifferenceText = $"Zmiana wagi: {difference:F1} kg";
                }
                else
                {
                    this.WeightDifferenceText = "Zmiana wagi: 0 kg";
                }
            }
        }
    }
}
