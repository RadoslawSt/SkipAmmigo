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
    public class UserInfoController : Controller
    {
        private readonly IRepositoryWrapper repoWrapper;

        public UserInfoController(IRepositoryWrapper RepoWrapper)
        {
            repoWrapper = RepoWrapper;
        }
        [HttpGet("{Id}")]
        public UserInformation GetPublicUserInfo(string loginId)
        {
            //Guid result = Guid.Parse(Id);
            var userInfo = repoWrapper.UserInfo.GetUserInformation(loginId);
            return userInfo;
        }
        [Route("[action]/{LoginId}")]
        [HttpGet]
        public UserInformation GetPublicUserInfoLogin(string LoginId)
        {
            //Guid result = Guid.Parse(Id);
            var UserInfo = repoWrapper.UserInfo.GetUserInformation(LoginId);
            return UserInfo;
        }
        [HttpPost]
        public IActionResult Create([FromBody] UserInformation userInfo)
        {
            try
            {
                if(userInfo == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                repoWrapper.UserInfo.CreateUserInformation(userInfo);
            }
            catch (Exception)
            {

                return BadRequest("Error while creating");
            }
            return Ok(userInfo);
        }
        [HttpPut("{Id}")]
        public IActionResult Edit(string Id, [FromBody] UserInformation userInformation)
        {

            try
            {
                if (userInformation == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                var dbUserInfo = repoWrapper.UserInfo.GetUserInformation(Id);

                repoWrapper.UserInfo.UpdateUserInformation(dbUserInfo, userInformation);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

    }
}