using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interface;
using Entities.Models;
using Entities.Extensions;

namespace JumpApp.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutSessionController : Controller
    {
        private readonly IRepositoryWrapper repoWrapper;

        public WorkoutSessionController(IRepositoryWrapper RepoWrapper)
        {
            repoWrapper = RepoWrapper;
        }

        [HttpGet]
        public IActionResult List(string loginId)
        {
            return Ok(repoWrapper.WorkoutSession.GetWorkoutSessions(loginId));
        }
        [HttpGet("Id")]
        public WorkoutSession GetWorkoutSessionById(int Id)
        {
            return repoWrapper.WorkoutSession.GetWorkoutSessionById(Id);
        }
        //[HttpGet("{Id}")]
        //public IEnumerable<WorkoutSession> GetWorkoutSessions(string loginId)
        //{
        //    var workoutSessions = repoWrapper.WorkoutSession.GetWorkoutSessions(loginId);
        //    return workoutSessions;
        //}
        [HttpPost]
        public IActionResult Create([FromBody]IEnumerable<WorkoutSession> workoutSessions)
        {
            try
            {
                if (workoutSessions == null || workoutSessions.Count() == 0 || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                repoWrapper.WorkoutSession.CreateMultipleWorkoutSessions(workoutSessions);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(workoutSessions);
        }
    }
}