using System.Collections.Generic;
using area.Business.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;

namespace area.Controllers
{
    [Authorize]
    [Route("/worker")]
    public class WorkerController : Controller
    {
        private readonly IUserBusinessLogic _userBusiness;
        
        public WorkerController(AreaContext context, IOptions<AppSettings> appSettings)
        {
           _userBusiness = new UserBusinessLogic(context, appSettings); 
        }
        
        // GET worker/[id]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> Worker(int id)
        {
            var currentUser = _userBusiness.GetCurrentUser(User);
            if (currentUser == null)
                return Unauthorized();

            return NotFound();
        }
    }
}