using System.Collections.Generic;
using area.Business.User;
using area.Business.WidgetConf;
using area.Configuration;
using area.Contexts;
using area.Models.WidgetConf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace area.Controllers
{
	[Authorize]
	[Route("/widget/conf")]
    public class WidgetConfController : Controller
    {
		private readonly IWidgetConfBusinessLogic _widgetConfBusiness;
		private readonly IUserBusinessLogic _userBusiness;

		public WidgetConfController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_widgetConfBusiness = new WidgetConfBusinessLogic(context);
			_userBusiness = new UserBusinessLogic(context, appSettings);
		}
		
		// GET widget/conf
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
			[FromQuery] int limit)
		{
			var currentUser = _userBusiness.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized();
			
			return Ok(_widgetConfBusiness.GetWidgetConfsByUserId(currentUser.Id, offset, limit));
		}
		
		// GET widget/conf/[id]
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<string> GetWidgetConf(int id)
		{
			return Ok(_widgetConfBusiness.GetWidgetConf(id));
		}
		
		// PATCH widget/conf/[id]
		[HttpPatch("{id}")]
		public ActionResult<string> PatchWidgetConf(int id, [FromForm] WidgetConfUpdateModel newConf)
		{
			if (!ModelState.IsValid)
				return BadRequest();
			var currentUser = _userBusiness.GetCurrentUser(User);

			if (currentUser == null)
				return Unauthorized();
			var patchedConf = _widgetConfBusiness.UpdateWidgetConf(id, currentUser, newConf);

			if (patchedConf != null)
				return Ok(patchedConf);
			return BadRequest();
		}
		
		// DELETE widget/conf/[id]
		[HttpDelete("{id}")]
		public ActionResult<string> DeleteWidgetConf(int id)
		{
			var currentUser = _userBusiness.GetCurrentUser(User);

			if (currentUser == null)
				return Unauthorized();

			if (_widgetConfBusiness.DeleteWidgetConf(id, currentUser) == 1)
				return Ok();
			return BadRequest();
		}
    }
}