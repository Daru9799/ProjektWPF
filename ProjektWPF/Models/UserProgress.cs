using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektWPF.Models
{
    public class UserProgress
    {
        [Column("progress_id")]
        public int ProgressId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public float? Weight { get; set; }
        [Column("body_fat_percentage")]
        public float? BodyFatPercentage { get; set; }
        public UserProgress(int progressId, int userId, DateTime? date, float? weight, float? bodyFatPercentage) 
        {
            this.ProgressId = progressId;
            this.UserId = userId;
            this.Date = date;
            this.Weight = weight;
            this.BodyFatPercentage = bodyFatPercentage;
        }
        public override string ToString()
        {
            return $"{ProgressId}, {UserId}, {Date}, {Weight}, {BodyFatPercentage}";
        }
    }
}
