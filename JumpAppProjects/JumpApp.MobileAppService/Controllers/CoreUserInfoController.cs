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
    public class CoreUserInfoController : Controller
    {
        private readonly IRepositoryWrapper repoWrapper;

        public CoreUserInfoController(IRepositoryWrapper RepoWrapper)
        {
            repoWrapper = RepoWrapper;
        }

        [HttpGet("{Id}")]
        public CoreUserInformation GetCoreUserInfo(int Id)
        {
            //Guid result = Guid.Parse(Id);
            var coreUserInfo = repoWrapper.CoreUserInfo.GetCoreUserById(Id);
            return coreUserInfo;
        }
        [Route("[action]/{LoginId}")]
        [HttpGet]
        public CoreUserInformation GetCoreUserInfoLogin(string LoginId)
        {
            //Guid result = Guid.Parse(Id);
            var coreUserInfo = repoWrapper.CoreUserInfo.GetCoreUserByLoginId(LoginId);
            return coreUserInfo;
        }
        [HttpPost]
        public IActionResult Create([FromBody]CoreUserInformation coreUserInfo)
        {
            try
            {
                if (coreUserInfo == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                repoWrapper.CoreUserInfo.CreateCoreUser(coreUserInfo);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(coreUserInfo);
        }

        [HttpPut("{Id}")]
        public IActionResult Edit(int Id, [FromBody] CoreUserInformation coreUserInformation)
        { 
            
            try
            {
                if (coreUserInformation == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                var dbcoreUserInfo = repoWrapper.CoreUserInfo.GetCoreUserById(Id);

                repoWrapper.CoreUserInfo.UpdateCoreUser(dbcoreUserInfo, coreUserInformation);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public void Delete(int id)
        {
            repoWrapper.CoreUserInfo.DeleteCoreUser(GetCoreUserInfo(id));
        }
    }
}