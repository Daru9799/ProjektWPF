using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;

namespace ProjektWPF.ViewModels
{
    public class ProgressViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public ProgressViewModel() { }
        public ProgressViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
    }
}
