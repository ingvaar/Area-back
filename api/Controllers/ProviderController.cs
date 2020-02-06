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
	[Route("/provider")]
	public class ProviderController : Controller
	{
		private IProviderRepository provider;
		private IUserRepository user;

		public ProviderController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			provider = new ProviderRepository(context, appSettings);
			user = new UserRepository(context);
		}

		// GET provider
		[AllowAnonymous]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
													[FromQuery] int limit)
		{
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return Ok(provider.GetProviders(offset, limit));
		}

		// GET provider/search
		[AllowAnonymous]
		[HttpGet("search")]
		public ActionResult<IEnumerable<string>> Search([FromQuery] int offset,
														[FromQuery] int limit,
														[FromQuery] string name)
		{
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return Ok(provider.GetProviderByName(name, offset, limit));
		}

		// GET provider/{id}
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<string>> GetById(int id)
		{
			return Ok(provider.GetProviderById(id));
		}

		// DELETE provider/{id}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			int success = provider.DeleteProviderById(id);

			if (success == 1)
				return Ok();
			else if (success == 2)
				return Unauthorized();
			return BadRequest();
		}

		// POST provider
		[HttpPost()]
		public ActionResult<IEnumerable<string>> Post([FromForm] ProviderCreationModel newProv)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			return Ok(provider.AddNewProvider(newProv));
		}

		// PATCH provider/{id}
		[HttpPatch("{id}")]
		public IActionResult Patch(int id, [FromForm] ProviderUpdateModel prov)
		{
			if(!ModelState.IsValid || id < 0)
				return BadRequest();

			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			int success = provider.UpdateProviderById(id, prov);

			if (success == 1)
				return Ok();
			else if (success == 2)
				return NotFound();
			return BadRequest();
		}
	}
}