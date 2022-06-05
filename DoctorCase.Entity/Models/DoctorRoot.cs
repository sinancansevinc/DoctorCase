using DoctorCase.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Entity.Models
{
    public class DoctorRoot:BaseRoot
    {
        public List<Doctor> DoctorList { get; set; }
        public List<DoctorDto> DoctorDto { get; set; }
    }
}
