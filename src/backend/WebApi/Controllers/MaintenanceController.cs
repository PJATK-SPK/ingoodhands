using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Core.Database;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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
            return new ContentResult
            {
                Content = LoadHelloHtml(),
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
                LoadHelloHtml();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "File system is unavailable.");
            }

            return Ok("OK");
        }

        private string LoadHelloHtml()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var result = new Uri(location).LocalPath;
            location = result.ToString();

            var path =
               Path.Combine(
                   Path.GetDirectoryName(location)!,
                   Path.Join("WebApi/Hello.html")
               );

            return System.IO.File.ReadAllText(path);
        }
    }
}
