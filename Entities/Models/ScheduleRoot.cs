using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Entites.Models
{
    public class ScheduleRoot:BaseRoot
    {
        public List<Schedule> ScheduleList { get; set; }
    }
}
