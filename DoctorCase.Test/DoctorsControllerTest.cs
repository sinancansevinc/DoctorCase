using DoctorCase.API.Controllers;
using DoctorCase.Entity.Models;
using DoctorCase.Service.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoctorCase.Test
{
    public class DoctorsControllerTest
    {
        private readonly Mock<IService> _mockRepo;
        private readonly DoctorsController _controller;

        private DoctorRoot doctorRoot;

        public DoctorsControllerTest()
        {
            _mockRepo = new Mock<IService>();
            _controller = new DoctorsController(_mockRepo.Object);

            doctorRoot = new DoctorRoot
            {
                SONUC_MESAJI= "İşlem Başarılı",
                DoctorList=new List<Doctor>
                {
                    new Doctor
                    {
                        gender="Male",
                        branchId=45234.4,
                        createdAt=DateTime.Now,
                        doctorId="1",
                        hospitalId=2,
                        name="Sinancan Sevinc",
                        nationality="TUR",
                        specialtyId=1,
                        hospitalName="Medicana"
                        
                    },
                    new Doctor
                    {
                        gender="Male",
                        branchId=43234.4,
                        createdAt=DateTime.Now,
                        doctorId="2",
                        hospitalId=2,
                        name="Kobe Bryant ",
                        nationality="USA",
                        specialtyId=1,
                        hospitalName="International"

                    },

                }
            };
        }
    }
}
