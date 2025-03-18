﻿using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("LoginUser")]
        public IActionResult Login([FromBody] RequestUserLoginModel userLogin)
        {
            var user = new UserDto
            {
                Username = userLogin.Username,
                Email = userLogin.Email,
                Password = userLogin.Password
            };

            var result = _userService.LoginUser(user);

            if (result)
                return Ok(new { Success = result, Message = "Login successful" });   
            else
                return StatusCode(500, new { Success = result, Message = "An error occurred while logging in." });
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] UserDto user)
        {
            _userService.SendChangePasswordMail(user);
            return Ok();
        }
    }
}
