using System;
using System.Collections.Generic;
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
    public class ProfileChangePasswordViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;

        public ProfileChangePasswordViewModel(MainViewModel mainViewModel)
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

        private string? _oldPassword { get; set; }

        public string? OldPassword
        {
            get => _oldPassword;
            set
            {
                if (_oldPassword != value)
                {
                    _oldPassword = value;
                    OnPropertyChanged(nameof(OldPassword));
                }
            }
        }

        private string? _newPassword { get; set; }

        public string? NewPassword
        {
            get => _newPassword;
            set
            {
                if (value != null)
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

                if (_newPassword != value)
                {
                    _newPassword = value;
                    OnPropertyChanged(nameof(NewPassword));
                }
            }
        }

        private ICommand _passwordClick = null;
        public ICommand PasswordClick
        {
            get
            {
                if (_passwordClick == null)
                {
                    _passwordClick = new RelayCommand(
                        arg => { ChangePassword(); }, arg => CanChangePassword()); ;
                }

                return _passwordClick;
            }
        }

        private void ChangePassword()
        {
            int? id = UserSession.CurrentUserId;
            string hPassword = DbUsers.GetHashById(id);
            if (PasswordEncryption.VerifyPassword(this.OldPassword, hPassword))
            {
                string hashedNewPassword = PasswordEncryption.HashPassword(this.NewPassword);
                DbUsers.UpdatePassword(UserSession.CurrentUserId, hashedNewPassword); //aktualizacja hasła
                this.OldPassword = null;
                this.NewPassword = null;
                UserSession.CurrentUserId = null;
                UserSession.CurrentUserId = id;
            }
            else
            {
                this.ErrorText = "Podano złe hasło!";
            }
        }

        private bool CanChangePassword()
        {
            if (this.NewPassword != null)
            {
                if (PasswordWarningText == "")
                {
                    return true;
                }
            }
            return false;
        }



        //Widocznosc
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
    }
}
