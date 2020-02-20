using System.Collections.Generic;
using area.Business.Provider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using area.Contexts;

namespace area.Controllers
{
	[Authorize]
	[Route("/provider")]
	public class ProviderController : Controller
	{
		private readonly IProviderBusinessLogic _provider;

		public ProviderController(AreaContext context)
		{
			_provider = new ProviderBusinessLogic(context);
		}

		// GET provider
		[AllowAnonymous]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
													[FromQuery] int limit)
		{
			return Ok(_provider.GetProviders(offset, limit));
		}

		// GET provider/search
		[AllowAnonymous]
		[HttpGet("search")]
		public ActionResult<IEnumerable<string>> Search([FromQuery] int offset,
														[FromQuery] int limit,
														[FromQuery] string name)
		{
			return Ok(_provider.SearchProviderByName(name, offset, limit));
		}

		// GET provider/[id]
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<string>> GetById(int id)
		{
			return Ok(_provider.GetProviderById(id));
		}
	}
}