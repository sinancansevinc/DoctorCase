using DoctorCase.Entity.Dtos;
using DoctorCase.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorCase.Service.Services
{
    public interface IService
    {
        Task<DoctorRoot> GetDoctorsAsync();
        Task<List<Schedule>> GetSchedulesAsync(string id);
        Task<BookRoot> PostBooking(BookDto bookDto);
        Task<BookRoot> PostCancelBooking(string bookingId);
        Task<DoctorRoot> GetTurkishDoctorsAsync();
        Task ExportCSV();

    }
}
