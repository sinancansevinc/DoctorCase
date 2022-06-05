using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Entites.Models
{
    public class Schedule
    {
        public int doctorId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int VisitId { get; set; }
        public string id { get; set; }
    }
}
