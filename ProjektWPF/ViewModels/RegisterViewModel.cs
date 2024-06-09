using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ProjektWPF.Core;
using ProjektWPF.Models;
using ProjektWPF.Data;
using System.Security.Cryptography;
using ProjektWPF.ViewModels.Converters;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace ProjektWPF.ViewModels
{
    //ViewModel powiązany z panelem rejestracji
    public class RegisterViewModel : ViewModelBase
    {
        private ObservableCollection<string> _gender;
        public ObservableCollection<string> Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string _selectedGender;
        public string SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                if (_selectedGender != value)
                {
                    _selectedGender = value;
                    OnPropertyChanged(nameof(SelectedGender));
                }
            }
        }
        private string? _errorText { get; set; }
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                if (_errorText != value)
                {
                    _errorText = value;
                    OnPropertyChanged(nameof(ErrorText));
                }
            }
        }
        private string? _username { get; set; }
        private string? _password { get; set; }
        private string? _email { get; set; }
        private string? _age { get; set; }
        private string? _weight { get; set; }
        private string? _height { get; set; }
        public string? Username
        {
            get => _username;
            set
            {
                if (value != null)
                {
                    string pattern = "[^a-zA-Z0-9]";
                    if (Regex.Matches(value, pattern).Count > 0)
                    {
                        LoginWarningVisibility = Visibility.Visible;
                        LoginWarningText = "Login zawiera niedozwolone znaki";
                    }
                    else if(value.Length <3)
                    {
                        LoginWarningVisibility = Visibility.Visible;
                        LoginWarningText = "Login jest za krótki. Musi mieć przynajmniej 3 znaki";
                    }
                    else if(value.Length > 20)
                    {
                        LoginWarningVisibility = Visibility.Visible;
                        LoginWarningText = "Login jest za długi. Musi mieć maksymalnie 20 znaków";
                    }
                    else
                    {
                        LoginWarningVisibility = Visibility.Hidden;
                        LoginWarningText = "";
                    }
                    
                }

                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        public string? Password
        {
            get => _password;
            set
            {
                if(value != null)
                {
                    string pattern = @"^(?=.*[A-Z])(?=.*\d).{8,}$";
                    if (!Regex.IsMatch(value, pattern))
                    {
                        PasswordWarningVisibility = Visibility.Visible;
                        PasswordWarningText = "Hasło musi mieć przynajmniej 8 znaków w tym przynajmniej jedną wielką literę i cyfrę";
                    }
                    else
                    {
                        PasswordWarningVisibility = Visibility.Hidden;
                        PasswordWarningText = "";
                    }
                }

                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        public string? Age
        {
            get => _age;
            set
            {
                if (value != null)
                {
                    string pattern = @"^(1[3-9]|[2-9][0-9])$";
                    if (!Regex.IsMatch(value, pattern))
                    {
                        AgeWarningVisibility = Visibility.Visible;
                        AgeWarningText = "Podano zły wiek. Musi to być liczba w przedziale 13-99";
                    }
                    else
                    {
                        AgeWarningVisibility = Visibility.Hidden;
                        AgeWarningText = "";
                    }
                }
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged(nameof(Age));
                }
            }
        }
        public string? Email
        {
            get => _email;
            set
            {
                if (value != null)
                {
                    string pattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
                    if (!Regex.IsMatch(value, pattern))
                    {
                        EmailWarningVisibility = Visibility.Visible;
                        EmailWarningText = "Wpisano niepoprawny adres";
                    }
                    else
                    {
                        EmailWarningVisibility = Visibility.Hidden;
                        EmailWarningText = "";
                    }
                }

                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
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
        public string? Height
        {
            get => _height;
            set
            {
                if (value != null)
                {
                    string pattern = @"^(?:12\d|1[3-9]\d|2[01]\d|22[0-9]|230)(?:,\d)?$";
                    if (!Regex.IsMatch(value, pattern))
                    {
                        HeightWarningVisibility = Visibility.Visible;
                        HeightWarningText = "Niepoprawny wzrost (dokladność prosimy podać do jednego miejsca po przecinku)";
                    }
                    else
                    {
                        HeightWarningVisibility = Visibility.Hidden;
                        HeightWarningText = "";
                    }

                }
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }
        public RegisterViewModel()
        {
            Gender = ["Mężczyzna", "Kobieta"];
            SelectedGender = "Mężczyzna";
        }

        private ICommand _registerClick = null;
        public ICommand RegisterClick
        {
            get
            {
                if (_registerClick == null)
                {
                    _registerClick = new RelayCommand(
                        arg => { Register(); }, arg => CanRegister()); ;
                }

                return _registerClick;
            }
        }
        private void Register()
        {
            if(DbUsers.GetIdByName(this.Username) == 0)
            {
                string sex = GenderToEnum();
                string hPassword = PasswordEncryption.HashPassword(this.Password);
                User user1 = new User(0, this.Username, hPassword, this.Email, int.Parse(this.Age), sex, float.Parse(this.Weight, NumberStyles.Float, CultureInfo.GetCultureInfo("pl-PL")), float.Parse(this.Height, NumberStyles.Float, CultureInfo.GetCultureInfo("pl-PL")), 0, 0, 0, DateTime.Now, DateTime.Now);
                DbUsers.AddUserToDb(user1);
                UserSession.CurrentUserId = DbUsers.GetIdByName(this.Username); //Tworze sesje dla zarejestrowanego
            }
            else
            { 
                this.ErrorText = "Użytkownik o podanej nazwie istnieje!";
            }
        }
        //Funkcja bedzie sprawdzac czy wszystkie pola są poprawne (regexy) i wypelnione jesli nie beda wyswietlane bledy po prawej stronie
        private bool CanRegister()
        {
            if((this.Username != null) && (this.Password != null) && (this.Age != null) && (this.Email != null) && (this.Weight != null) && (this.Height != null))
            {
                if ((LoginWarningText == "") && (PasswordWarningText == "") && (EmailWarningText == "") && (AgeWarningText == "") && (WeightWarningText == "") && (HeightWarningText == ""))
                {
                    return true;
                }
            }
            return false;
        }
        private string GenderToEnum()
        {
            if(this.SelectedGender == "Mężczyzna")
            {
                return "M";
            } 
            else 
            {
                return "F";
            }
        }

        //Reakcja na blędy
        private Visibility _loginWarningVisibility = Visibility.Collapsed;
        public Visibility LoginWarningVisibility
        {
            get { return _loginWarningVisibility; }
            set
            {
                _loginWarningVisibility = value;
                OnPropertyChanged(nameof(LoginWarningVisibility));
            }
        }
        private string _loginWarningText;
        public string LoginWarningText
        {
            get { return _loginWarningText; }
            set
            {
                _loginWarningText = value;
                OnPropertyChanged(nameof(LoginWarningText));
            }
        }

        private Visibility _passwordWarningVisibility = Visibility.Collapsed;
        public Visibility PasswordWarningVisibility
        {
            get { return _passwordWarningVisibility; }
            set
            {
                _passwordWarningVisibility = value;
                OnPropertyChanged(nameof(PasswordWarningVisibility));
            }
        }
        private string _passwordWarningText;
        public string PasswordWarningText
        {
            get { return _passwordWarningText; }
            set
            {
                _passwordWarningText = value;
                OnPropertyChanged(nameof(PasswordWarningText));
            }
        }

        private Visibility _emailWarningVisibility = Visibility.Collapsed;
        public Visibility EmailWarningVisibility
        {
            get { return _emailWarningVisibility; }
            set
            {
                _emailWarningVisibility = value;
                OnPropertyChanged(nameof(EmailWarningVisibility));
            }
        }
        private string _emailWarningText;
        public string EmailWarningText
        {
            get { return _emailWarningText; }
            set
            {
                _emailWarningText = value;
                OnPropertyChanged(nameof(EmailWarningText));
            }
        }
        private Visibility _ageWarningVisibility = Visibility.Collapsed;
        public Visibility AgeWarningVisibility
        {
            get { return _ageWarningVisibility; }
            set
            {
                _ageWarningVisibility = value;
                OnPropertyChanged(nameof(AgeWarningVisibility));
            }
        }
        private string _ageWarningText;
        public string AgeWarningText
        {
            get { return _ageWarningText; }
            set
            {
                _ageWarningText = value;
                OnPropertyChanged(nameof(AgeWarningText));
            }
        }

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

        private Visibility _heightWarningVisibility = Visibility.Collapsed;
        public Visibility HeightWarningVisibility
        {
            get { return _heightWarningVisibility; }
            set
            {
                _heightWarningVisibility = value;
                OnPropertyChanged(nameof(HeightWarningVisibility));
            }
        }
        private string _heightWarningText;
        public string HeightWarningText
        {
            get { return _heightWarningText; }
            set
            {
                _heightWarningText = value;
                OnPropertyChanged(nameof(HeightWarningText));
            }
        }
    }
}
