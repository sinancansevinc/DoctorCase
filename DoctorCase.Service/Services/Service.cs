using DoctorCase.Entity.Dtos;
using DoctorCase.Entity.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DoctorCase.Service.Services
{
    public class Service : IService
    {
        public async Task<DoctorRoot> GetDoctorsAsync()
        {

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

                        doctorRoot.DoctorDto = new List<DoctorDto>();

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

                            doctorRoot.DoctorDto.Add(doc);
                        }

                        return doctorRoot;
                    }



                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<DoctorRoot> GetTurkishDoctorsAsync()
        {
            var doctorRoot = await GetDoctorsAsync();
            doctorRoot.DoctorDto = doctorRoot.DoctorDto.Where(x => x.nationality == "TUR").ToList();

            foreach (var doc in doctorRoot.DoctorDto)
            {
                if (doc.gender.Equals("Female"))
                {
                    doc.gender = "Kadın";
                }
                else if (doc.gender.Equals("Male"))
                {
                    doc.gender = "Erkek";
                }
            }

            return doctorRoot;

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

                    if (scheduleRoot.ScheduleList == null)
                    {
                        Console.WriteLine(scheduleRoot.SONUC_MESAJI);

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

        public void ExportCSV(DoctorRoot doctorRoot)
        {
            try
            {
                string[] columnHeaders = new string[] {
                "Created Date",
                "Branch Id",
                "Gender",
                "Hospital Id",
                "Hospital Name",
                "Doctor Name",
                "Specialty Id",
                "Nationality",
                "DoctorId",
                };

                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                ExcelPackage excel = new ExcelPackage();

                // name of the sheet
                var workSheet = excel.Workbook.Worksheets.Add("Doctors");

                // setting the properties
                // of the work sheet 
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                // Setting the properties
                // of the first row
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;

                for (int i = 1; i <=columnHeaders.Length; ++i)
                {
                    
                    workSheet.Cells[1, i].Value = columnHeaders[i-1];
                }

                int recordIndex = 2;
                foreach(var item in doctorRoot.DoctorDto)
                {
                    workSheet.Cells[recordIndex, 1].Value = item.createdAt.ToString();
                    workSheet.Cells[recordIndex, 2].Value = item.branchId;
                    workSheet.Cells[recordIndex, 3].Value = item.gender;
                    workSheet.Cells[recordIndex, 4].Value = item.hospitalId;
                    workSheet.Cells[recordIndex, 5].Value = item.hospitalName;
                    workSheet.Cells[recordIndex, 6].Value = item.name;
                    workSheet.Cells[recordIndex, 7].Value = item.specialtyId;
                    workSheet.Cells[recordIndex, 8].Value = item.nationality;
                    workSheet.Cells[recordIndex, 9].Value = item.doctorId;
                    recordIndex++;
                }

                string p_strPath = "Outputs\\doctors.csv";

                if (File.Exists(p_strPath))
                    File.Delete(p_strPath);

                // Create excel file on physical disk 
                FileStream objFileStrm = File.Create(p_strPath);
                objFileStrm.Close();

                // Write content to excel file 
                File.WriteAllBytes(p_strPath, excel.GetAsByteArray());
                //Close Excel package
                excel.Dispose();



            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
