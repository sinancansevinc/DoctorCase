using DoctorCase.Entity.Dtos;
using DoctorCase.Entity.Models;
using DoctorCase.Services.Services;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorCase.Service.Services
{
    public class Service : IService
    {
        public async Task<List<DoctorDto>> GetDoctorsAsync()
        {
            var doctorList = new List<DoctorDto>();

            try
            {
                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync("https://4998a4df-5365-4890-812f-093c4d44f87f.mock.pstmn.io/fetchDoctors");

                    response.EnsureSuccessStatusCode();

                    using (HttpContent content = response.Content)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var doctorRoot = JsonConvert.DeserializeObject<DoctorRoot>(responseBody);

                        if (!(doctorRoot.DoctorList.Count > 0))
                        {
                            throw new Exception(doctorRoot.SONUC_MESAJI);
                        }

                        foreach (var item in doctorRoot.DoctorList)
                        {
                            DoctorDto doc = new DoctorDto
                            {
                                branchId = Convert.ToInt32(item.branchId),
                                doctorId = item.doctorId,
                                createdAt = item.createdAt,
                                gender = item.gender,
                                hospitalId = item.hospitalId,
                                hospitalName = item.hospitalName,
                                name = item.name,
                                nationality = item.nationality,
                                specialtyId = item.specialtyId

                            };

                            doctorList.Add(doc);
                        }

                    }

                    return doctorList;

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<List<Schedule>> GetSchedulesAsync(string id)
        {
            string apiUrl = "https://4998a4df-5365-4890-812f-093c4d44f87f.mock.pstmn.io/fetchSchedules?doctorId=";
            try
            {
                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = await client.GetAsync(apiUrl + id);

                    response.EnsureSuccessStatusCode();

                    HttpContent content = response.Content;

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var scheduleRoot = JsonConvert.DeserializeObject<ScheduleRoot>(responseBody);

                    if ((scheduleRoot.ScheduleList == null))
                    {
                        Console.WriteLine(scheduleRoot.SONUC_MESAJI);
                        throw new Exception(scheduleRoot.SONUC_MESAJI);
                    }

                    return scheduleRoot.ScheduleList;

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<BookRoot> PostBooking(BookDto bookDto)
        {
            var url = "https://4998a4df-5365-4890-812f-093c4d44f87f.mock.pstmn.io/BookVisit";

            try
            {
                using (var client = new HttpClient())
                {
                    var query = new Dictionary<string, string>()
                    {
                        ["VisitId"] = bookDto.VisitId.ToString(),
                        ["startTime"] = bookDto.StartTime.ToString(),
                        ["endTime"] = bookDto.EndTime.ToString(),
                        ["date"] = bookDto.Date.ToString(),
                        ["PatientName"] = bookDto.PatientName.ToString(),
                        ["PatientSurname"] = bookDto.PatientSurname.ToString(),
                        ["hospitalId"] = bookDto.HospitalId.ToString(),
                        ["doctorId"] = bookDto.DoctorId.ToString(),
                        ["branchId"] = bookDto.BranchId.ToString(),
                    };

                    var uri = QueryHelpers.AddQueryString(url, query);
                    var response = await client.PostAsync(uri, null);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var bookRoot = JsonConvert.DeserializeObject<BookRoot>(responseBody);

                    return bookRoot;

                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
