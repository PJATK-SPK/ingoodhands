﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using Core.Setup.WebApi;
using System.Linq.Dynamic.Core;

namespace WebApi.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("")]
    public class MaintenanceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MaintenanceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetDocs()
        {
            _context.Products.PageResult(1, 30);
            return new ContentResult
            {
                Content = MaintenanceService.LoadHelloHtml(),
                ContentType = "text/html"
            };
        }

        [HttpGet("health")]
        public async Task<ActionResult> CheckHealth()
        {
            try
            {
                using var connection = _context.Database.GetDbConnection();
                await connection.OpenAsync();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Database is unavailable.");
            }

            try
            {
                MaintenanceService.LoadHelloHtml();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "File system is unavailable.");
            }

            return Ok("OK");
        }
    }
}
