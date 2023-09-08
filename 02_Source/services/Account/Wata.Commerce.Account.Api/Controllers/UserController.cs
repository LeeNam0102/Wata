using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Controllers;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Account.Business.Services;

namespace Wata.Commerce.Account.Controllers
{
	[Route("api/Account/[controller]")]
	[ApiController]
    public class UserController : ApiControllerBase
    {
		#region Fields
		private readonly IConfiguration _config;
		private readonly ILogger _logger;
		private readonly IUserService _userService;
		#endregion

		#region Constructors
		public UserController(
			ILogger<UserController> logger,
			IConfiguration configuration,
			IUserService userService)
        {
			_logger = logger;
            _config = configuration;
			_userService = userService;
        }
		#endregion

		#region Insert User
		[HttpPost]
        public async Task<ActionResult<UserDto?>> Insert([FromBody]UserRequestDto userRequestDto)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest();
			}

			UserDto? userDto = await _userService.InsertUserAsync(userRequestDto);
			userRequestDto.SetUserID(await GetUserID());

			if(userDto != null)
			{
				return Ok(userDto);
			}

			return StatusCode(500);
        }
		#endregion

		#region Update User
        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update([FromBody]UserRequestDto userRequestDto, string id)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest();
			}

			userRequestDto.SetUserID(await GetUserID());

				userRequestDto.Id = id;

			int total = await _userService.UpdateUserAsync(userRequestDto);
			if (total > 0)
			{
				return Ok(total);
			}

			return StatusCode(500);
        }
        #endregion

		#region Delete User
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(string id)
        {
			UserDto? userDto = await _userService.GetUserAsync(id, false);
			if(userDto == null)
			{
				return NotFound();
			}

			int total = await _userService.DeleteUserAsync(id);
			if (total > 0)
			{
				return Ok(total);
			}

			return StatusCode(500);
        }
        #endregion

		#region Get User
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto?>> Get(string id, bool? isDeep)
        {
			UserDto? userDto = await _userService.GetUserAsync(id, isDeep ?? false);
			if(userDto == null)
			{
				return NotFound();
			}

			return Ok(userDto);
        }
        #endregion

		#region Get List Users
        [HttpGet]
        public async Task<ActionResult<PagedDto<UserDto>>> GetList([FromQuery]UserFilterDto filterDto)
        {
			return Ok(await _userService.GetListUsersAsync(filterDto));
        }
        #endregion

    }
}