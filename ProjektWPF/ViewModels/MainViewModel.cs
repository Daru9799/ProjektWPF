using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProjektWPF.Core;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    //Klasa główna odpowiedzialna za spięcie wszystkich innych viewmodeli i zarządzanie nimi
    public class MainViewModel : ViewModelBase
    {
        //ViewModele do poszczególnych podstron (tutaj trzeba kazdy nowy dodac zeby go obsluzyc razem z RelayCommand)
        public LoginViewModel LoginVm { get; set; }
        public RegisterViewModel RegisterVm { get; set; }
        public RelayCommand LoginViewCommand { get; set; }
        public RelayCommand RegisterViewCommand { get; set; }

        //Aktualny widok
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public MainViewModel() 
        {
            LoginVm = new LoginViewModel(this); //Przekazuje referencje na potrzeby zmiany zmiennej id
            RegisterVm = new RegisterViewModel();
            LoginViewCommand = new RelayCommand(arg => { CurrentView = LoginVm; }, null);
            RegisterViewCommand = new RelayCommand(arg => { CurrentView = RegisterVm; }, null);
            //Ustawienie poczatkowego widoku na ekran logowania
            CurrentView = LoginVm;

            //przed zalogowaniem ustawiam zmienną CurrentUserId na null (przy testach zmiana tego jest rownowazna z byciem zalogowanym)
            CurrentUserId = null;
        }

        //Zmienna CurrentUserId przechowuje id aktualnie zalogowanego uzytkownika
        private int? _currentUserId;
        public int? CurrentUserId
        {
            get => _currentUserId;
            set
            {
                if (_currentUserId != value)
                {
                    _currentUserId = value;
                    OnPropertyChanged(nameof(CurrentUserId));
                    //Zmiana widocznosci jesli mamy przechwycone id zalogowanego uzytkownika
                    if(CurrentUserId != null) 
                    {
                        LogRegVisibility = Visibility.Collapsed;
                        LogOutVisibility = Visibility.Visible;
                    }
                    else
                    {
                        LogRegVisibility = Visibility.Visible;
                        LogOutVisibility = Visibility.Collapsed;
                    }
                }
            }
        }

        //Zmiany panelu bocznego przy zalogowaniu i po wylogowaniu 
        private Visibility _logRegVisibility = Visibility.Visible;
        public Visibility LogRegVisibility
        {
            get { return _logRegVisibility; }
            set
            {
                _logRegVisibility = value;
                OnPropertyChanged(nameof(LogRegVisibility));
            }
        }
        private Visibility _logOutVisibility = Visibility.Collapsed;
        public Visibility LogOutVisibility
        {
            get { return _logOutVisibility; }
            set
            {
                _logOutVisibility = value;
                OnPropertyChanged(nameof(LogOutVisibility));
            }
        }

        //Wylogowywanie sie
        private ICommand _logOutClick;
        public ICommand LogOutClick
        {
            get
            {
                if (_logOutClick == null)
                {
                    _logOutClick = new RelayCommand(
                        arg => { LogOut(); }, null);
                }

                return _logOutClick;
            }
        }
        private void LogOut()
        {
            CurrentUserId = null; //Po wylogowaniu id aktualnego uzytkownika ustawiamy na null
            CurrentView = LoginVm; //Wracamy rowniez do widoku logowania
        }
    }
}
