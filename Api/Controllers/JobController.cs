using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly EluminiDbContext _eluminiDbContext;

        public JobController(ILogger<JobController> logger)
        {
            _logger = logger;
            _eluminiDbContext = new EluminiDbContext();
        }

        [HttpPost(Name = "CreateJob")]
        public IActionResult Create([FromBody] Job job)
        {
            try
            {
                // Logic to create a job goes here
                return CreatedAtRoute("CreateJob", new { id = job.Id }, job);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating the Job: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet(Name = "GetJobs")]
        public IEnumerable<Job> Get()
        {
            try
            {
                return _eluminiDbContext.Jobs.Include(o => o.Text).Include(o => o.ParameterStatus).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving Jobs: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return (IEnumerable<Job>)StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut(Name = "UpdateJob")]
        public IActionResult Update([FromBody] Job job)
        {
            try
            {
                // Logic to update a job goes here
                return CreatedAtRoute("CreateJob", new { id = job.Id }, job);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating the Job: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal Server Error");
            }
        }        
    }
}
