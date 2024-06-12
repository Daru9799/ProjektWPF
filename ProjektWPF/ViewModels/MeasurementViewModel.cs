using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void OnUserIdChanged(int? newUserId)
        {

            if (newUserId != null)
            {
                this.ProgressesList = DbUserProgress.GetAllProgressForUser(newUserId);
            }
        }
    }
}
