using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjektWPF.Core;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class ProfileEditViewModel : ViewModelBase
    {
        private MainViewModel _mainViewModel;

        public ProfileEditViewModel(MainViewModel mainViewModel)
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
