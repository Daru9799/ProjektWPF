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
        private int? _age { get; set; }
        private float? _weight { get; set; }
        private float? _height { get; set; }
        public string? Username
        {
            get => _username;
            set
            {
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
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
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
        public float? Weight
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
        public float? Height
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
                User user1 = new User(0, this.Username, hPassword, this.Email, this.Age, sex, this.Weight, this.Height, 0, 0, 0, DateTime.Now, DateTime.Now);
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
                if(this.Username.Length>2 && this.Username.Length < 20)
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
    }
}
