using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Repositories;

namespace area.Controllers
{
	[Route("/about.json")]
	public class AboutController : Controller
	{
		private IAboutRepository about;

		public AboutController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			about = new AboutRepository(context, appSettings);
		}

		// GET provider
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return Ok(about.About());
		}
	}
}