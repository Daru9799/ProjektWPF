using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        #region Zmienne prywatne
        private int? _user_id { get; set; }
        private string? _userName { get; set; }
        private string? _email { get; set; }
        private int? _age { get; set; }
        private string? _gender { get; set; }
        private int? _weight { get; set; }
        private int? _height { get; set; }
        private int _total_workouts { get; set; }
        private int? _total_calories_burned { get; set; }
        private int? _total_time_spent { get; set; }
        #endregion

        #region Zmienne publiczne
        public int? User_id
        {
            get => _user_id;
            set
            {
                if (_user_id != value)
                {
                    _user_id = value;
                    OnPropertyChanged(nameof(User_id));
                }
            }
        }

        public string? UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        public string? Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public int? Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }

        public string? Gender
        {
            get => _gender;
            set
            {
                if (_gender != value)
                {
                    _gender = value;
                    OnPropertyChanged(nameof(Gender));
                }
            }
        }

        public int? Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }

        public int? Height
        {
            get => _height;
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        public int Total_workouts
        {
            get => _total_workouts;
            set
            {
                if (_total_workouts != value)
                {
                    _total_workouts = value;
                    OnPropertyChanged(nameof(Total_workouts));
                }
            }
        }

        public int? Total_calories_burned
        {
            get => _total_calories_burned;
            set
            {
                if (_total_calories_burned != value)
                {
                    _total_calories_burned = value;
                    OnPropertyChanged(nameof(Total_calories_burned));
                }
            }
        }

        public int? Total_time_spent
        {
            get => _total_time_spent;
            set
            {
                if (_total_time_spent != value)
                {
                    _total_time_spent = value;
                    OnPropertyChanged(nameof(Total_time_spent));
                }
            }
        }
        #endregion

        public ProfileViewModel()
        {

        }
    }
}
