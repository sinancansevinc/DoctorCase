﻿using DoctorCase.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IService _service;

        public DoctorsController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var doctorRoot = await _service.GetTurkishDoctorsAsync();

            _service.ExportCSV(doctorRoot);

            return Ok(doctorRoot);
        }

        
    }
}
