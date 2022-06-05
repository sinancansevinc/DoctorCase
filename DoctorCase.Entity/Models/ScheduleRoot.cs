using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Entity.Models
{
    public class ScheduleRoot:BaseRoot
    {
        public List<Schedule> ScheduleList { get; set; }
    }
}
