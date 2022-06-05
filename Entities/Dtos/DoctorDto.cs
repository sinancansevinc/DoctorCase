using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Entites.Dtos
{
    public class DoctorDto
    {
        public DateTime createdAt { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string hospitalName { get; set; }
        public int hospitalId { get; set; }
        public int specialtyId { get; set; }
        public int branchId { get; set; }
        public string nationality { get; set; }
        public string doctorId { get; set; }
    }
}
