using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Entites.Dtos
{
    public class BookDto
    {
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string StartTime { get; set; }
        public string Date { get; set; }
        public string EndTime { get; set; }
        public int HospitalId { get; set; }
        public string DoctorId { get; set; }
        public int VisitId { get; set; }
        public int BranchId { get; set; }

    }
}
