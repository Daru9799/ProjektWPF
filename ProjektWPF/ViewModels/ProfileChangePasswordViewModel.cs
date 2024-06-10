using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektWPF.Core;

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
    }
}
