﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;

namespace ProjektWPF.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public ProfileViewModel() { }
        public ProfileViewModel(MainViewModel mainViewModel) 
        {  
            _mainViewModel = mainViewModel; 
        }
    }
}
