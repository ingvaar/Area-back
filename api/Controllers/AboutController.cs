using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using area.Business.About;
using area.Configuration;
using area.Contexts;

namespace area.Controllers
{
	[Route("/about.json")]
	public class AboutController : Controller
	{
		private readonly IAboutBusinessLogic _about;

		public AboutController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_about = new AboutBusinessLogic(context);
		}

		// GET provider
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			return Ok(_about.About());
		}
	}
}