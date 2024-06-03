﻿using System;
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
        //ViewModele do poszczególnych podstron
        public LoginViewModel LoginVm { get; set; }
        public RegisterViewModel RegisterVm { get; set; }
        public ProfileViewModel ProfileVm { get; set; }
        public ExercisesViewModel ExercisesVm { get; set; }
        public WorkoutsViewModel WorkoutsVm { get; set; }
        public SessionViewModel SessionVm { get; set; }
        public ProgressViewModel ProgressVm { get; set; }
        public RelayCommand LoginViewCommand { get; set; }
        public RelayCommand RegisterViewCommand { get; set; }
        public RelayCommand ProfileViewCommand { get; set; }
        public RelayCommand ExercisesViewCommand { get; set; }
        public RelayCommand WorkoutsViewCommand { get; set; }
        public RelayCommand SessionViewCommand { get; set; }
        public RelayCommand ProgressViewCommand { get; set; }

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
            //Przekazuje referencje do MainView aby poszczególne widoki miały dostęp do zmiennej CurrentUserId 
            LoginVm = new LoginViewModel(this);
            RegisterVm = new RegisterViewModel();
            ProfileVm = new ProfileViewModel(this);
            ExercisesVm = new ExercisesViewModel();
            WorkoutsVm = new WorkoutsViewModel(this);
            SessionVm = new SessionViewModel(this);
            ProgressVm = new ProgressViewModel(this);

            LoginViewCommand = new RelayCommand(arg => { CurrentView = LoginVm; }, null);
            RegisterViewCommand = new RelayCommand(arg => { CurrentView = RegisterVm; }, null);
            ProfileViewCommand = new RelayCommand(arg => { CurrentView = ProfileVm; }, null);
            ExercisesViewCommand = new RelayCommand(arg => { CurrentView = ExercisesVm; }, null);
            WorkoutsViewCommand = new RelayCommand(arg => { CurrentView = WorkoutsVm; }, null);
            SessionViewCommand = new RelayCommand(arg => { CurrentView = SessionVm; }, null);
            ProgressViewCommand = new RelayCommand(arg => { CurrentView = ProgressVm; }, null);

            //Ustawienie poczatkowego widoku na ekran logowania
            CurrentView = LoginVm;

            //przed zalogowaniem ustawiam zmienną CurrentUserId na null
            //POLECAM DO TESTOW USTAWIAC NA DOWOLNE ID WTEDY TRAKTUJE JAK ZALOGOWANEGO
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
                        CurrentView = ProfileVm;
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
