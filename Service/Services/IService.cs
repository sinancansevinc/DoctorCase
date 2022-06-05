using DoctorCase.Entity.Dtos;
using DoctorCase.Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoctorCase.Services.Services
{
    public interface IService
    {
        Task<List<DoctorDto>> GetDoctorsAsync();
        Task<List<Schedule>> GetSchedulesAsync(string id);
        Task<BookRoot> PostBooking(BookDto bookDto);
    }
}
