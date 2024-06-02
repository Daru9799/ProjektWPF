﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektWPF.Core;

namespace ProjektWPF.ViewModels
{
    //ViewModel powiązany z panelem logowania
    public class LoginViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public LoginViewModel() { }
        public LoginViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }


        private ICommand _loginClick = null;
        public ICommand LoginClick
        {
            get
            {
                if (_loginClick == null)
                {
                    _loginClick = new RelayCommand(
                        arg => { Login(); }, null);
                }

                return _loginClick;
            }
        }
        //Potem będzie tutaj logika logowania się i przypisywania Id, aktualnie jest to robione na sztywno
        private void Login()
        {
            _mainViewModel.CurrentUserId = 1;
        }
    }
}
