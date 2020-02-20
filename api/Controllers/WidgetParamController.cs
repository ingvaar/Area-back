using System.Collections.Generic;
using area.Business.WidgetParam;
using area.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace area.Controllers
{
	[Authorize]
	[Route("/widget/param")]
    public class WidgetParamController : Controller
    {
		private readonly IWidgetParamBusinessLogic _widgetParamBusiness;
		
		public WidgetParamController(AreaContext context)
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

		// GET widget/param/[id]
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<string> GetWidgetParam(int id)
		{
			return Ok(_widgetParamBusiness.GetWidgetParamById(id));
		}
    }
}