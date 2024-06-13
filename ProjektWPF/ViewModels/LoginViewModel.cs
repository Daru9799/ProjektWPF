using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;
using ProjektWPF.ViewModels.Converters;

namespace ProjektWPF.ViewModels
{
    //ViewModel powiązany z panelem logowania
    public class LoginViewModel : ViewModelBase
    {
        private string? _username { get; set; }
        private string? _password { get; set; }
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
        private ICommand _loginClick = null;
        public ICommand LoginClick
        {
            get
            {
                if (_loginClick == null)
                {
                    _loginClick = new RelayCommand(
                        arg => { Login(); }, arg => (this.Username != null) && (this.Password != null) );
                }

                return _loginClick;
            }
        }
        
        private async void Login()
        {
            int id = await DbUsers.GetIdByName(this.Username);
            if (id != 0)
            {
                string hPassword = await DbUsers.GetHashById(id);
                if(PasswordEncryption.VerifyPassword(this.Password, hPassword))
                {
                    await DbUsers.UpdateLastLogin(id); //Aktualizuje date ostatneigo logowania
                    UserSession.CurrentUserId = id; //Utworzenie sesji 
                    //Czyszczenie pól
                    this.ErrorText = "";
                    this.Username = null;
                    this.Password = null;
                }
                else
                {
                    this.ErrorText = "Błędne hasło!";
                }
            }
            else
            {
                this.ErrorText = "Konto o podanej nazwie nie istnieje!";
            }
        }
    }
}
