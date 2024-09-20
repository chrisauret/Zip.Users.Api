using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zip.Domain.Entities;
using Zip.Application.Contracts;

namespace Zip.WebAPI.Controllers
{
    /// <summary>
    /// Endpoints for user management.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Creates a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">Implementation of user service.</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieve all users.
        /// </summary>
        /// <returns>A list of user objects.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetAll().ConfigureAwait(false);

            // TODO: Instead of BadRequest, return a ProblemDetails result.
            return result.IsSuccessful ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Retrieve a user by ID.
        /// </summary>
        /// <param name="id">The user ID to search for.</param>
        /// <returns>A user object.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _userService.GetById(id).ConfigureAwait(false);

            return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
        }

        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="user">The user to be created.</param>
        /// <returns>A user object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            var result = await _userService.Create(user).ConfigureAwait(false);

            return result.IsSuccessful
                ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                : BadRequest(result.Error);
        }
    }
}
