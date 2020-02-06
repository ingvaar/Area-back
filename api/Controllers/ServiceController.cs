using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Helpers;
using area.Models;
using area.Repositories;
using area.Repositories.User;

namespace area.Controllers
{
	[Authorize]
	[Route("/service")]
	public class ServiceController : Controller
	{
		private readonly IServiceRepository service;
		private readonly IUserRepository user;

		public ServiceController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			service = new ServiceRepository(context, appSettings);
			user = new UserRepository(context);
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
	}
}
