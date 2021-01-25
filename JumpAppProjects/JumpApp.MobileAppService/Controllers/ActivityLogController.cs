using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Models;
namespace JumpApp.MobileAppService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private readonly IRepositoryWrapper repoWrapper;

        public ActivityLogController(IRepositoryWrapper RepoWrapper)
        {
            repoWrapper = RepoWrapper;
        }

        [HttpGet]
        public IEnumerable<ActivityLog> GetActivityLogsById(string LoginId)
        {
            var activityLog = repoWrapper.ActivityLog.GetActivityLogsById(LoginId);
            return activityLog; 
        }
        [HttpPost]
        IActionResult Create([FromBody] ActivityLog activityLog)
        {
            try
            {
                if(activityLog == null || !ModelState.IsValid)
                {
                    return BadRequest("InvalidState");
                }
                repoWrapper.ActivityLog.CreateActivityLog(activityLog);
            }
            catch (Exception)
            {

                return BadRequest("Error while creating");
            }
            return Ok(activityLog);
        }
    }
}