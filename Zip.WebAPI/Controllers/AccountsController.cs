using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Zip.Application.Contracts;
using Zip.Domain.Entities;

namespace Zip.WebAPI.Controllers
{
    /// <summary>
    /// Endpoints for account management.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// Creates a new instance of the <see cref="AccountsController"/> class.
        /// </summary>
        /// <param name="accountService">Implementation of account service.</param>
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Retrieve an account by ID.
        /// </summary>
        /// <param name="id">The account ID to search for.</param>
        /// <returns>An account object.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Account))]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _accountService.GetById(id).ConfigureAwait(false);

            return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
        }

        /// <summary>
        /// Create an account.
        /// </summary>
        /// <param name="createAccountRequest">The account to be created.</param>
        /// <returns>An account object.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Account))]
        public async Task<IActionResult> Create([FromBody] Account createAccountRequest)
        {
            var result = await _accountService.Create(createAccountRequest).ConfigureAwait(false);

            return result.IsSuccessful
                ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value)
                : BadRequest(result.Error);
        }
    }
}
