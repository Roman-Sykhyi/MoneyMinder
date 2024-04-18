using FinanceManagerBack.Data.Repositories;
using FinanceManagerBack.Models;
using FinanceManagerBack.Services;
using FinanceManagerBack.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FinanceManagerBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly UserRepository _userRepository;
        private IConfiguration _config;
        private const string ROLE = "user";

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, UserRepository userRepository, IConfiguration config)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._userRepository = userRepository;
            _config = config;
        }

        [HttpGet("user")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                string EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;

                var user = await _userRepository.GetByEmail(EmailAddress);


                if (user != null)
                {
                    return new ObjectResult(user);
                }
                return NoContent();
            }
            return NoContent();
        }


        [HttpGet("users")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            if (users != null)
            {
                return new ObjectResult(users);
            }
           return NoContent();
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Register([FromBody] RegisterRequestDto model)
        {

            if (ModelState.IsValid)
            {
                var existingUser = await _userRepository.GetByName(model.Name);

                if (existingUser == null)
                {

                        var newUser = new User { Email = model.Email, UserName = model.Email, Name = model.Name };

                        try
                        {
                            var result = await _userManager.CreateAsync(newUser, model.Password);

                            if (result.Succeeded)
                            {
                                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                                //User Get Role
                                var roleIdentity = await _userRepository.GetRole(ROLE);

                                //Add Role to User
                                var userRole = new IdentityUserRole<string>() { UserId = newUser.Id, RoleId = roleIdentity.Id };

                                await _userRepository.AddRole(userRole);
                                await _userRepository.Save();

                                newUser = await _userManager.FindByIdAsync(newUser.Id);

                                await _userManager.ConfirmEmailAsync(newUser, code);

                                return Ok("register done!");

                            }
                        }
                        catch (Exception)
                        {
                            await _userRepository.Delete(newUser.Id);
                            await _userRepository.Save();

                            return StatusCode(500);
                        }
                }
                else
                {
                    return StatusCode(500);
                }
            }

            return ValidationProblem("Bad Form");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetByEmail(model.Email);

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    var token = JwtService.Generate(user, _config);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Wrong login or password");
                }
            }
            return ValidationProblem("Bad Form");
        }
    }
}