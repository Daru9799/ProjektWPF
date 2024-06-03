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
    }
}
