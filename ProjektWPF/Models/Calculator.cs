using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektWPF.Models
{
    public static class Calculator
    {
        public static float? CalculateBmi(float? weight, float? height)
        {
            float? heightM = height / 100;

            float bmi = weight.Value / (heightM.Value * heightM.Value);
            return bmi;
        }

        public static float? CalculateBodyFat(float? weight, float? bmi, int? age, bool? isMale)
        {
            if (weight == null || bmi == null || age == null || isMale == null)
            {
                return null;
            }

            float bodyFatPercentage;
            if (isMale.Value)
            {
                // Obliczenia dla mężczyzn
                bodyFatPercentage = 1.20f * bmi.Value + 0.23f * age.Value - 16.2f;
            }
            else
            {
                // Obliczenia dla kobiet
                bodyFatPercentage = 1.20f * bmi.Value + 0.23f * age.Value - 5.4f;
            }
            return bodyFatPercentage;
        }

        public static float? CalculateWeightDifference(float? oldWeight, float? newWeight)
        {
            return newWeight-oldWeight;
        }
        public static int? CalculateAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return null;
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - birthDate.Value.Year;
            if (birthDate.Value.Date > currentDate.AddYears(-age))
            {
                age--;
            }
            return age;
        }

        public static bool IsMale(string sex)
        {
            if (sex == "M")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
