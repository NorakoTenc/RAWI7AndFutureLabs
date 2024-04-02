using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using RAWI7AndFutureLabs.Services.Data;

namespace RAWI7AndFutureLabs.Controllers
{
    [ApiController]
    [Route("api/data")]
    [Authorize]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{version}")]
        [MapToApiVersion("1.0")]
        [Obsolete("This version is obsolete.")]
        public IActionResult GetData(string version)
        {
            switch (version)
            {
                case "1.0":
                    int dataV1 = _dataService.GetIntegerData();
                    return Ok(dataV1);
                case "2.0":
                    string dataV2 = _dataService.GetTextData();
                    return Ok(dataV2);
                case "3.0":
                    byte[] excelData = _dataService.GenerateExcelData();
                    return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "data.xlsx");
                default:
                    return BadRequest("Invalid version specified.");
            }
        }
    }
}