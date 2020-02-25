using System.Collections.Generic;
using area.Business.User;
using area.Business.Widget;
using area.Configuration;
using area.Contexts;
using area.Models.WidgetConf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace area.Controllers
{
	[Authorize]
	[Route("/widget")]
	public class WidgetController : Controller
	{
		private readonly IWidgetBusinessLogic _widgetBusiness;
		private readonly IUserBusinessLogic _userBusiness;

		public WidgetController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_widgetBusiness = new WidgetBusinessLogic(context);
			_userBusiness = new UserBusinessLogic(context, appSettings);
		}

		// GET widget
		[AllowAnonymous]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
			[FromQuery] int limit)
		{
			return Ok(_widgetBusiness.GetWidgets(offset, limit));
		}

		// GET widget/[id]
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<string> GetWidget(uint id)
		{
			return Ok(_widgetBusiness.GetWidgetById(id));
		}

		// GET widget/[id]/param
		[AllowAnonymous]
		[HttpGet("{id}/param")]
		public ActionResult<string> GetWidgetParam(uint id)
		{
			return Ok(_widgetBusiness.GetWidgetParam(id));
		}

		// GET widget/[id]/conf
		[HttpGet("{id}/conf")]
		public ActionResult<IEnumerable<string>> GetWidgetConf([FromQuery] int offset, [FromQuery] int limit, uint id)
		{
			var currentUser = _userBusiness.GetCurrentUser(User);

			if (currentUser != null)
				return Ok(_widgetBusiness.GetWidgetConf(id, currentUser, offset, limit));

			return Unauthorized();
		}

		// POST widget/[id]/conf
		[HttpPost("{id}/conf")]
		public ActionResult<string> PostWidgetConf(int id, [FromForm]WidgetConfCreationModel newConf)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var currentUser = _userBusiness.GetCurrentUser(User);

			if (currentUser == null)
				return Unauthorized();
			
			var addedConf = _widgetBusiness.AddWidgetConf(id, currentUser, newConf);

			if (addedConf != null)
				return Ok(addedConf);

			return BadRequest();
		}
	}
}