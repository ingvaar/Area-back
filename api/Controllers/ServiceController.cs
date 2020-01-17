using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Helpers;
using area.Models;
using area.Repositories;

namespace area.Controllers
{
	[Authorize]
	[Route("/service")]
	public class ServiceController : Controller
	{
		private IServiceRepository service;
		private IUserRepository user;

		public ServiceController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			service = new ServiceRepository(context, appSettings);
			user = new UserRepository(context, appSettings);
		}

		// GET service
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
													[FromQuery] int limit)
		{
			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return Ok(service.GetServices(offset, limit, currentUser.Id));
		}

		// GET service/search
		[AllowAnonymous]
		[HttpGet("search")]
		public ActionResult<IEnumerable<string>> Search([FromQuery] int offset,
														[FromQuery] int limit,
														[FromQuery] int id)
		{
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return Ok(service.GetServices(offset, limit, id));
		}

		// GET service/{id}
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<string>> GetById(int id)
		{
			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			return Ok(service.GetServiceById(id));
		}

		// DELETE service/{id}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			int success = service.DeleteServiceById(id, currentUser.Id);

			if (success == 1)
				return Ok();
			else if (success == 2)
				return Unauthorized();
			return BadRequest();
		}

		// POST service
		[HttpPost()]
		public ActionResult<IEnumerable<string>> Post([FromForm] AccountCreationModel newProv)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			return Ok(service.AddNewService(newProv, currentUser.Id));
		}

		// PATCH service/{id}
		[HttpPatch("{id}")]
		public IActionResult Patch(int id, [FromForm] AccountUpdateModel serv)
		{
			if(!ModelState.IsValid || id < 0)
				return BadRequest();

			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			int success = service.UpdateServiceById(id, serv, currentUser.Id);

			if (success == 1)
				return Ok();
			else if (success == 2)
				return NotFound();
			return BadRequest();
		}
	}
}
