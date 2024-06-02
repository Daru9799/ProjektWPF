using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjektWPF.Core;

namespace ProjektWPF.ViewModels
{
    //ViewModel powiązany z panelem rejestracji
    public class RegisterViewModel : ViewModelBase
    {
        private ObservableCollection<string> _gender;
        public ObservableCollection<string> Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender));
            }
        }

        private string _selectedGender;
        public string SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                if (_selectedGender != value)
                {
                    _selectedGender = value;
                    OnPropertyChanged(nameof(SelectedGender));
                }
            }
        }
        public RegisterViewModel()
        {
            Gender = ["Mężczyzna", "Kobieta"];
            SelectedGender = "Mężczyzna";
        }
    }
}
