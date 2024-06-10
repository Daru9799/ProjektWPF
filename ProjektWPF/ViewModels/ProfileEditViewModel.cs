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
using ProjektWPF.ViewModels.Converters;

namespace ProjektWPF.ViewModels
{
    public class ProfileEditViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;

        public ProfileEditViewModel(MainViewModel mainViewModel)
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
            _mainViewModel.ChangeViewToProfile();
        }

        private string? _username { get; set; }
        private string? _email { get; set; }
        private string? _height { get; set; }

        public string? Username
        {
            get => _username;
            set
            {
                if (value != null)
                {
                    // Sprawdzenie czy pierwszy znak jest literą
                    string firstCharPattern = @"^[a-zA-Z]";
                    string pattern = "[^a-zA-Z0-9]";

                    if (!Regex.IsMatch(value, firstCharPattern))
                    {
                        LoginWarningVisibility = Visibility.Visible;
                        LoginWarningText = "Login musi zaczynać się literą";
                    }
                    else if (Regex.Matches(value, pattern).Count > 0)
                    {
                        LoginWarningVisibility = Visibility.Visible;
                        LoginWarningText = "Login zawiera niedozwolone znaki";
                    }
                    else if (value.Length < 3)
                    {
                        LoginWarningVisibility = Visibility.Visible;
                        LoginWarningText = "Login jest za krótki. Musi mieć przynajmniej 3 znaki";
                    }
                    else if (value.Length > 20)
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

        public string? Email
        {
            get => _email;
            set
            {
                if (value != null)
                {
                    string pattern = @"^[a-zA-Z][^\s@]*@[^\s@]+\.[^\s@]+$";

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

        //Przyciski zmiany + funkcje
        private ICommand _loginClick = null;
        public ICommand LoginClick
        {
            get
            {
                if (_loginClick == null)
                {
                    _loginClick = new RelayCommand(
                        arg => { ChangeLogin(); }, arg => CanChangeLogin()); ;
                }

                return _loginClick;
            }
        }

        private void ChangeLogin()
        {
            if (DbUsers.GetIdByName(this.Username) == 0) //Sprawdza czy nie ma takiego usera
            {
                DbUsers.UpdateUsername(UserSession.CurrentUserId, this.Username); //aktualizacja loginu
                int? id = UserSession.CurrentUserId;
                UserSession.CurrentUserId = null;
                UserSession.CurrentUserId = id;
            }
            else
            {
                this.ErrorText = "Użytkownik o podanej nazwie istnieje!";
            }
        }

        private bool CanChangeLogin()
        {
            if (this.Username != null)
            {
                if (LoginWarningText == "")
                {
                    return true;
                }
            }
            return false;
        }

        private ICommand _emailClick = null;
        public ICommand EmailClick
        {
            get
            {
                if (_emailClick == null)
                {
                    _emailClick = new RelayCommand(
                        arg => { ChangeEmail(); }, arg => CanChangeEmail()); ;
                }

                return _emailClick;
            }
        }

        private void ChangeEmail()
        {
            if (DbUsers.GetIdByEmail(this.Email) == 0) //Sprawdza czy nie ma takiego maila
            {
                DbUsers.UpdateEmail(UserSession.CurrentUserId, this.Email); //aktualizacja maila
                int? id = UserSession.CurrentUserId;
                UserSession.CurrentUserId = null;
                UserSession.CurrentUserId = id;
            }
            else
            {
                this.ErrorText = "Użytkownik o podanym adresie email istnieje!";
            }
        }

        private bool CanChangeEmail()
        {
            if (this.Email != null)
            {
                if (EmailWarningText == "")
                {
                    return true;
                }
            }
            return false;
        }

        private ICommand _heightClick = null;
        public ICommand HeightClick
        {
            get
            {
                if (_heightClick == null)
                {
                    _heightClick = new RelayCommand(
                        arg => { ChangeHeight(); }, arg => CanChangeHeight()); ;
                }

                return _heightClick;
            }
        }

        private void ChangeHeight()
        {
            DbUsers.UpdateHeight(UserSession.CurrentUserId, float.Parse(this.Height, NumberStyles.Float, CultureInfo.GetCultureInfo("pl-PL"))); //aktualizacja wzrostu
            int? id = UserSession.CurrentUserId;
            UserSession.CurrentUserId = null;
            UserSession.CurrentUserId = id;
        }

        private bool CanChangeHeight()
        {
            if (this.Height != null)
            {
                if (HeightWarningText == "")
                {
                    return true;
                }
            }
            return false;
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
