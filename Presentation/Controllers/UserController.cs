using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicpayChallenge.Application.Interfaces;
using PicpayChallenge.Domain.Entities;
using PicpayChallenge.Domain.Interfaces;
using PicpayChallenge.Exceptions;
using PicpayChallenge.Infra.Data;

namespace PicpayChallenge.Presentation.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("api/users/createuser")]
        public async Task<IActionResult> CreateUser(string name,
            string email,
            string password,
            string document,
            double initialAmount)
        {
            try
            {
                var newUser = await _userService.CreateUser(name, email, password, document, initialAmount);
                return Ok(newUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("api/users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("api/users/{id}")]

        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest($"User not found {e.Message}");
            }
        }

        [HttpGet("api/users/getbydocument/{doc}")]
        public async Task<IActionResult> GetUserByDocument(string doc)
        {
            try
            {
                var user = await _userService.GetUserByDocument(doc);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest($"User not found {e.Message}");
            }
        }

        [HttpGet("api/users/transactions")]
        public async Task<IActionResult> GetUserTransactions(string userDocument)
        {
            try
            {
                var user = await _userService.GetUserTransactions(userDocument);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest($"User not found {e.Message}");
            }
        }

        [HttpPatch("api/users/updateuser")]

        public async Task<IActionResult> UpdateUserName(string document, string newName, string pwd)
        {
            try
            {
                var user = await _userService.GetUserByDocument(document);
                
                if (user is null)
                    throw new UserDataException($"Any user found with document: {document}");

                await _userService.UpdateUser(user.Id, document, newName, pwd);

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest($"User not found {e.Message}");
            }
        }

        [HttpDelete("api/users/deleteusers/{id}")]

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                await _userService.DeleteUser(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest($"User not found {e.Message}");
            }
        }
    }
}
