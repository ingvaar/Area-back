using System.Collections.Generic;
using area.Business.User;
using area.Business.Widget;
using area.Business.WidgetParam;
using area.Configuration;
using area.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace area.Controllers
{
	[Authorize]
	[Route("/widget/param")]
    public class WidgetParamController : Controller
    {
		private readonly IWidgetParamBusinessLogic _widgetParamBusiness;
		
		public WidgetParamController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_widgetParamBusiness = new WidgetParamBusinessLogic(context);
		}
		
		// GET widget/param
		[AllowAnonymous]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
			[FromQuery] int limit)
		{
			return Ok(_widgetParamBusiness.GetWidgetParams(offset, limit));
		}

		// GET widget/param/{id}
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<string> GetWidgetParam(int id)
		{
			return Ok(_widgetParamBusiness.GetWidgetParamById(id));
		}
    }
}