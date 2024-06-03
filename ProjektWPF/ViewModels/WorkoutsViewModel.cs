using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;

namespace ProjektWPF.ViewModels
{
    public class WorkoutsViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public WorkoutsViewModel() { }
        public WorkoutsViewModel(MainViewModel mainViewModel) 
        {
            _mainViewModel = mainViewModel;
        }
    }
}
