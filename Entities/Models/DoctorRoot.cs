﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Entites.Models
{
    public class DoctorRoot:BaseRoot
    {
        public List<Doctor> DoctorList { get; set; }
    }
}
