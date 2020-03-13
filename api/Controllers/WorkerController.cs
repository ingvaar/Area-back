using System.Collections.Generic;
using area.Business.User;
using area.Business.Worker;
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
        private readonly IWorkerBusinessLogic _workerBusiness;
        
        public WorkerController(AreaContext context, IOptions<AppSettings> appSettings)
        {
           _userBusiness = new UserBusinessLogic(context, appSettings); 
           _workerBusiness = new WorkerBusinessLogic(context);
        }
        
        // GET worker/[id]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> Worker(int id)
        {
            var currentUser = _userBusiness.GetCurrentUser(User);
            if (currentUser == null)
                return Unauthorized();

            var data = _workerBusiness.GetData(id, currentUser);
            return data == null ? (ActionResult<IEnumerable<string>>) BadRequest("{\"error\": \"configuration ill-formed\"}") : Ok(data);
        }
    }
}