using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;

namespace ProjektWPF.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;
        public SessionViewModel() { }
        public SessionViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
    }
}
