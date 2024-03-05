using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Models;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParameterController : ControllerBase
    {

        private readonly ILogger<ParameterController> _logger;
        private readonly EluminiDbContext _eluminiDbContext;

        public ParameterController(ILogger<ParameterController> logger)
        {
            _logger = logger;
            _eluminiDbContext = new EluminiDbContext();
        }

        [HttpGet(Name = "GetParametres")]
        public IEnumerable<Parameters> Get()
        {
            try
            {
                return _eluminiDbContext.Parameters.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Parameters: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return (IEnumerable<Parameters>)StatusCode(500, "Internal Server Error");
            }
        }
    }
}
