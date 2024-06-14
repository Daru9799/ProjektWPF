using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ProjektWPF.Core;
using ProjektWPF.Data;
using ProjektWPF.Models;

namespace ProjektWPF.ViewModels
{
    public class MeasurementViewModel : ViewModelBase
    {
        private List<UserProgress> _progressesList;
        public List<UserProgress> ProgressesList
        {
            get { return _progressesList; }
            set
            {
                _progressesList = value;
                OnPropertyChanged();
            }
        }
        public MeasurementViewModel()
        {
            UserSession.UserIdChanged += OnUserIdChanged;
            UserSession.UserWeightChanged += OnUserWeightChanged;
        }

        private void OnUserIdChanged(int? newUserId)
        {

            if (newUserId != null)
            {
                this.ProgressesList = DbUserProgress.GetAllProgressForUser(newUserId);
                this.Date1 = null;
                this.Date2 = null;
            }
        }
        private DateTime? _date1 { get; set; }
        private DateTime? _date2 { get; set; }
        public DateTime? Date1
        {
            get => _date1;
            set
            {
                if (_date1 != value)
                {
                    _date1 = value;
                    OnPropertyChanged(nameof(Date1));
                }
            }
        }
        public DateTime? Date2
        {
            get => _date2;
            set
            {
                if (_date2 != value)
                {
                    _date2 = value;
                    OnPropertyChanged(nameof(Date2));
                }
            }
        }

        private ICommand _filterClick = null;
        public ICommand FilterClick
        {
            get
            {
                if (_filterClick == null)
                {
                    _filterClick = new RelayCommand(
                        arg => { Filter(); }, arg => CanFilter()); ;
                }

                return _filterClick;
            }
        }
        private ICommand _deleteFilterClick = null;
        public ICommand DeleteFilterClick
        {
            get
            {
                if (_deleteFilterClick == null)
                {
                    _deleteFilterClick = new RelayCommand(
                        arg => { DeleteFilter(); }, null); ;
                }

                return _deleteFilterClick;
            }
        }

        private void Filter()
        {
            this.ProgressesList = DbUserProgress.GetProgressWithDateRange(UserSession.CurrentUserId, this.Date1.Value, this.Date2.Value);
        }

        private bool CanFilter()
        {
            if((this.Date1 != null) && (this.Date2 != null))
            {
                return true;
            }
            return false;
        }

        private void DeleteFilter()
        {
            this.ProgressesList = DbUserProgress.GetAllProgressForUser(UserSession.CurrentUserId);
            this.Date1 = null;
            this.Date2 = null;
        }

        private void OnUserWeightChanged(bool? x)
        {
            if (UserSession.CurrentUserId != null)
            {
                this.ProgressesList = DbUserProgress.GetAllProgressForUser(UserSession.CurrentUserId);
                this.Date1 = null;
                this.Date2 = null;
            }
        }
    }
}
