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
    public  class TrainingHistoryViewModel : ViewModelBase
    {
        private int? _userId;
        private List<WorkoutSessionDetails> sessionsList;

        public TrainingHistoryViewModel() {}

        public int? UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    SessionsList=DbWorkoutSessions.GetUserSessions(_userId);
                    OnPropertyChanged(nameof(UserId));
                }
            }
        }
        public List<WorkoutSessionDetails> SessionsList
        {
            get { return sessionsList; }
            set
            {
                sessionsList = value;
                OnPropertyChanged();
            }
        }


    }
}
