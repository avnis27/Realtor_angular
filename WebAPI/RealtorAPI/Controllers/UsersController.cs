using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RealtorAPI.IServices;
using RealtorAPI.Settings;
using RealtorAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtorAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings appSettings;

        public UsersController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            this.appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userParam)
        {
            try
            {
                var user = _userService.Authenticate(userParam.UserName, userParam.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        //[Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [Authorize]
        [HttpGet("{userName}")]
        public async Task<ActionResult<User>> Get(string userName)
        {
            try
            {
                var result = await this._userService.GetUserAsync(userName);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{forgotpassword}/{userName}")]
        public async Task<ActionResult> GetUserDetail(string userName)
        {
            try
            {
                var result = await this._userService.GetUserWithPasswordAsync(userName);

                if (result == null)
                {
                    return NotFound();
                }

                EmailSender emailService = new EmailSender(appSettings);
                emailService.SendUserDetailViaEmail(result);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{id}/{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            try
            {
                var result = await this._userService.GeUserbyIdAsync(userId);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            try
            {
                var users = _userService.GetAll();
                var existUser = users.Where(x => x.UserName == user.UserName).FirstOrDefault();

                if (existUser != null && existUser.UserName == user.UserName)
                {
                    return BadRequest("User already exist");
                }
                user.ActivationCode = Guid.NewGuid().ToString();
                await this._userService.AddUserAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }

                user.Id = id;
                await this._userService.UpdateUserAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        //DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            try
            {
                var result = await this._userService.GeUserbyIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }

                await this._userService.DeleteUserAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}
