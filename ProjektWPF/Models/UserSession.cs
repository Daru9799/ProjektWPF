using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    //Klasa która przechowuje ID aktualnie zalogowanego Usera i przesyła informacje o jego zmianie do MainViewModel
    public static class UserSession
    {
        private static int? _currentUserId;
        public static event Action<int?> UserIdChanged;

        public static int? CurrentUserId
        {
            get => _currentUserId;
            set
            {
                if (_currentUserId != value)
                {
                    _currentUserId = value;
                    UserIdChanged?.Invoke(_currentUserId);
                }
            }
        }

        private static bool? _currentUserWeight;
        public static event Action<bool?> UserWeightChanged;
        public static bool? CurrentUserWeight
        {
            get => _currentUserWeight;
            set
            {
                if (_currentUserWeight != value)
                {
                    _currentUserWeight = value;
                    UserWeightChanged?.Invoke(_currentUserWeight);
                }
            }
        }

        private static int? _currentUserTrainingAdded;
        public static event Action<int?> UserTrainingAdded;
        public static int? CurrentUserTrainingAdded
        {
            get => _currentUserTrainingAdded;
            set
            {
                if (_currentUserTrainingAdded != value)
                {
                    _currentUserTrainingAdded = value;
                    UserTrainingAdded?.Invoke(CurrentUserTrainingAdded);
                }
            }
        }
    }
}
