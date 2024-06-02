using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProjektWPF.ViewModels
{
    //Konwersja pola tekstowego stringu w przypadku gdy jest puste tak aby zwracalo wtedy null (domyslnie zwraca pusty ciag)
    public class EmptyStringToNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Konwersja z UI do ViewModel wiec tu bez zmian
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Jesli jest to pusty ciag to zwraca nulla, inaczej normalna wartosc
            if (value is string stringValue)
            {
                return string.IsNullOrEmpty(stringValue) ? null : stringValue;
            }
            return value;
        }
    }
}
