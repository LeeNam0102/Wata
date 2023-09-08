using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Wata.Commerce.Common.Objects;
using Wata.Commerce.Common.Controllers;
using Wata.Commerce.Sample.Dtos;
using Wata.Commerce.Sample.Business.Services;

namespace Wata.Commerce.Sample.Controllers
{
	[Route("api/Sample/[controller]")]
	[ApiController]
    public class AbcController : ApiControllerBase
    {
		#region Fields
		private readonly IConfiguration _config;
		private readonly ILogger _logger;
		private readonly IAbcService _abcService;
		#endregion

		#region Constructors
		public AbcController(
			ILogger<AbcController> logger,
			IConfiguration configuration,
			IAbcService abcService)
        {
			_logger = logger;
            _config = configuration;
			_abcService = abcService;
        }
		#endregion

		#region Insert Abc
		[HttpPost]
        public async Task<ActionResult<AbcDto?>> Insert([FromBody]AbcRequestDto abcRequestDto)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest();
			}

			AbcDto? abcDto = await _abcService.InsertAbcAsync(abcRequestDto);
			abcRequestDto.SetUserID(await GetUserID());

			if(abcDto != null)
			{
				return Ok(abcDto);
			}

			return StatusCode(500);
        }
		#endregion

		#region Update Abc
        [HttpPut("{abcID}")]
        public async Task<ActionResult<int>> Update([FromBody]AbcRequestDto abcRequestDto, int abcID)
        {
			if(!ModelState.IsValid)
			{
				return BadRequest();
			}

			abcRequestDto.SetUserID(await GetUserID());

				abcRequestDto.AbcID = abcID;

			int total = await _abcService.UpdateAbcAsync(abcRequestDto);
			if (total > 0)
			{
				return Ok(total);
			}

			return StatusCode(500);
        }
        #endregion

		#region Delete Abc
        [HttpDelete("{abcID}")]
        public async Task<ActionResult<int>> Delete(int abcID)
        {
			AbcDto? abcDto = await _abcService.GetAbcAsync(abcID, false);
			if(abcDto == null)
			{
				return NotFound();
			}

			int total = await _abcService.DeleteAbcAsync(abcID);
			if (total > 0)
			{
				return Ok(total);
			}

			return StatusCode(500);
        }
        #endregion

		#region Get Abc
        [HttpGet("{abcID}")]
        public async Task<ActionResult<AbcDto?>> Get(int abcID, bool? isDeep)
        {
			AbcDto? abcDto = await _abcService.GetAbcAsync(abcID, isDeep ?? false);
			if(abcDto == null)
			{
				return NotFound();
			}

			return Ok(abcDto);
        }
        #endregion

		#region Get List Abcs
        [HttpGet]
        public async Task<ActionResult<PagedDto<AbcDto>>> GetList([FromQuery]AbcFilterDto filterDto)
        {
			return Ok(await _abcService.GetListAbcsAsync(filterDto));
        }
        #endregion

    }
}