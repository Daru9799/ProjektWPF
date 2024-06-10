using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private string? _userNameText { get; set; }
        private string? _emailText { get; set; }
        private string? _ageText { get; set; }
        private string? _genderText { get; set; }
        private string? _weightText { get; set; }
        private string? _heightText { get; set; }
        private string? _totalWorkoutsText { get; set; }
        private string? _totalCaloriesBurnedText { get; set; }
        private string? _totalTimeSpentText { get; set; }
        private string? _joinDateText { get; set; }
        private string? _lastLoginText { get; set; }

        public string? UserNameText
        {
            get => _userNameText;
            set
            {
                if (_userNameText != value)
                {
                    _userNameText = value;
                    OnPropertyChanged(nameof(UserNameText));
                }
            }
        }

        public string? EmailText
        {
            get => _emailText;
            set
            {
                if (_emailText != value)
                {
                    _emailText = value;
                    OnPropertyChanged(nameof(EmailText));
                }
            }
        }

        public string? AgeText
        {
            get => _ageText;
            set
            {
                if (_ageText != value)
                {
                    _ageText = value;
                    OnPropertyChanged(nameof(AgeText));
                }
            }
        }

        public string? GenderText
        {
            get => _genderText;
            set
            {
                if (_genderText != value)
                {
                    _genderText = value;
                    OnPropertyChanged(nameof(GenderText));
                }
            }
        }

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

        public string? HeightText
        {
            get => _heightText;
            set
            {
                if (_heightText != value)
                {
                    _heightText = value;
                    OnPropertyChanged(nameof(HeightText));
                }
            }
        }

        public string? TotalWorkoutsText
        {
            get => _totalWorkoutsText;
            set
            {
                if (_totalWorkoutsText != value)
                {
                    _totalWorkoutsText = value;
                    OnPropertyChanged(nameof(TotalWorkoutsText));
                }
            }
        }

        public string? TotalCaloriesBurnedText
        {
            get => _totalCaloriesBurnedText;
            set
            {
                if (_totalCaloriesBurnedText != value)
                {
                    _totalCaloriesBurnedText = value;
                    OnPropertyChanged(nameof(TotalCaloriesBurnedText));
                }
            }
        }

        public string? TotalTimeSpent
        {
            get => _totalTimeSpentText;
            set
            {
                if (_totalTimeSpentText != value)
                {
                    _totalTimeSpentText = value;
                    OnPropertyChanged(nameof(TotalTimeSpent));
                }
            }
        }
        public string? JoinDateText
        {
            get => _joinDateText;
            set
            {
                if (_joinDateText != value)
                {
                    _joinDateText = value;
                    OnPropertyChanged(nameof(JoinDateText));
                }
            }
        }
        public string? LastLoginText
        {
            get => _lastLoginText;
            set
            {
                if (_lastLoginText != value)
                {
                    _lastLoginText = value;
                    OnPropertyChanged(nameof(LastLoginText));
                }
            }
        }

        public ProfileViewModel()
        {
            UserSession.UserIdChanged += OnUserIdChanged;
        }

        private void OnUserIdChanged(int? newUserId)
        {

            if (newUserId != null)
            {
                User user = DbUsers.GetUserFromDb(newUserId);
                this.UserNameText = "Witaj " + user.Username + "!";
                this.EmailText = user.Email;
                this.AgeText = user.Age.ToString();
                this.WeightText = user.Weight + " kg";
                this.GenderText = ConvertGender(user.Gender);
                this.HeightText = user.Height + " cm";

                //Te dane bedzie trzeba jakos inaczej wydobyc gdyz beda ulegac ciaglej aktualizacji ale na razie wstawiam tak
                this.TotalWorkoutsText = user.TotalWorkouts.ToString();
                this.TotalCaloriesBurnedText = user.TotalCaloriesBurned + " kcal";
                this.TotalTimeSpent = user.TotalTimeSpent + " min";

                this.JoinDateText = user.JoinDate.ToString("d");
                this.LastLoginText = user.LastLogin.ToString();
            }
        }

        public string ConvertGender(string sex)
        {
            if(sex == "M")
            {
                return "Mężczyzna";
            } else
            {
                return "Kobieta";
            }
        }
    }
}
