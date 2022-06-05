using DoctorCase.Entity.Dtos;
using DoctorCase.Entity.Models;
using DoctorCase.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorCase
{
    public class Program
    {
        static void Main(string[] args)
        {
            
            var serviceProvider = new ServiceCollection().AddSingleton<IService, DoctorCase.Service.Services.Service>().BuildServiceProvider();

            var service = serviceProvider.GetService<IService>();

            var doctorListResponse = service.GetDoctorsAsync();
            doctorListResponse.Wait();

            if (doctorListResponse.IsCompletedSuccessfully)
            {
                Console.WriteLine("DOCTORS LIST");
                foreach (var item in doctorListResponse.Result.DoctorList)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));

                }
            }

            Console.WriteLine("Please write an Id of doctor do you want to book");
            string doctorId = Console.ReadLine();

            var scheduleListResponse = service.GetSchedulesAsync(doctorId);
            scheduleListResponse.Wait();

            if (scheduleListResponse.IsCompletedSuccessfully)
            {
                if (!(scheduleListResponse.Result != null))
                {
                    return;
                }

                Console.WriteLine("Schedule List");

                foreach (var item in scheduleListResponse.Result)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine(JsonConvert.SerializeObject(item, Formatting.Indented));

                }
            }

            Console.WriteLine("You can select a schedule id for booking");
            string scheduleId = Console.ReadLine();

            BookDto bookDto = new BookDto
            {
                BranchId = doctorListResponse.Result.DoctorDto.FirstOrDefault(x => x.doctorId == doctorId).branchId,
                Date = scheduleListResponse.Result.FirstOrDefault(x => x.id == scheduleId).startTime.ToString("dd'/'MM'/'yyyy"),
                StartTime = scheduleListResponse.Result.FirstOrDefault(x => x.id == scheduleId).startTime.ToShortTimeString(),
                EndTime = scheduleListResponse.Result.FirstOrDefault(x => x.id == scheduleId).endTime.ToShortTimeString(),
                DoctorId = doctorId,
                PatientName = "Kamil",
                PatientSurname = "Oz",
                HospitalId = doctorListResponse.Result.DoctorDto.FirstOrDefault(x => x.doctorId == doctorId).hospitalId,
                VisitId = scheduleListResponse.Result.FirstOrDefault(x => x.id == scheduleId).VisitId
            };

            var bookRoot = service.PostBooking(bookDto);
            bookRoot.Wait();

            if (bookRoot.Result.ReturnStatus)
            {
                Console.WriteLine(" Your booking id is: " + bookRoot.Result.BookingId);
            }

            Console.ReadLine();

        }

        
    }
}
