using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using area.Business.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using area.Contexts;

namespace area.Controllers
{
	[Authorize]
	[Route("/service")]
	public class ServiceController : Controller
	{
		private readonly IServiceBusinessLogic _service;

		public ServiceController(AreaContext context)
		{
			_service = new ServiceBusinessLogic(context);
		}

		// GET service
		[AllowAnonymous]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
													[FromQuery] int limit)
		{
			return Ok(_service.GetServices(offset, limit));
		}

		// GET service/search
		[AllowAnonymous]
		[HttpGet("search")]
		public ActionResult<IEnumerable<string>> Search([FromQuery] int offset,
														[FromQuery] int limit,
														[Required][FromQuery] string name)
		{
			return Ok(_service.SearchServiceByName(name, offset, limit));
		}

		// GET service/[id]
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<IEnumerable<string>> GetById(uint id)
		{
			return Ok(_service.GetServiceById(id));
		}
	}
}
