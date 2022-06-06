using DoctorCase.API.Controllers;
using DoctorCase.Entity.Dtos;
using DoctorCase.Entity.Models;
using DoctorCase.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
                SONUC_MESAJI = "İşlem Başarılı",
                DoctorList = new List<Doctor>
                {
                    new Doctor
                    {
                        gender = "Male",
                        branchId = 45234.4,
                        createdAt = DateTime.Now,
                        doctorId = "1",
                        hospitalId = 2,
                        name = "Sinancan Sevinc",
                        nationality = "TUR",
                        specialtyId = 1,
                        hospitalName = "Medicana"

                    },
                    new Doctor
                    {
                        gender = "Male",
                        branchId = 43234.4,
                        createdAt = DateTime.Now,
                        doctorId = "2",
                        hospitalId = 2,
                        name = "Kobe Bryant ",
                        nationality = "USA",
                        specialtyId = 1,
                        hospitalName = "International"

                    },
                },
                DoctorDto=new List<DoctorDto>
                {
                    new DoctorDto
                    {
                        gender = "Male",
                        branchId = 452345,
                        createdAt = DateTime.Now,
                        doctorId = "1",
                        hospitalId = 2,
                        name = "Sinancan Sevinc",
                        nationality = "TUR",
                        specialtyId = 1,
                        hospitalName = "Medicana"

                    },
                    new DoctorDto
                    {
                        gender = "Male",
                        branchId = 43235,
                        createdAt = DateTime.Now,
                        doctorId = "2",
                        hospitalId = 2,
                        name = "Kobe Bryant ",
                        nationality = "USA",
                        specialtyId = 1,
                        hospitalName = "International"

                    },
                }
            };
        }

        [Fact]
        public async void GetTurkishDoctorsAsync_ActionRun_ReturnOkResultWithDoctors()
        {
            _mockRepo.Setup(x => x.GetTurkishDoctorsAsync()).ReturnsAsync(doctorRoot);

            var result = await _controller.GetDoctors();

            var okResult = Assert.IsType<OkObjectResult>(result);

            var returnDoctors = Assert.IsType<DoctorRoot>(okResult.Value);

            Assert.Equal<int>(2, returnDoctors.DoctorDto.Count);
        }
    }
}
