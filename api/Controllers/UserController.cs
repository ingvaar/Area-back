using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Business.User;
using area.Models.User;

namespace area.Controllers
{
	[Authorize]
	[Route("/user")]
	public class UserController : Controller
	{
		private readonly IUserBusinessLogic _business;

		public UserController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			_business = new UserBusinessLogic(context, appSettings);
		}

		// GET user
		[AllowAnonymous]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
													[FromQuery] int limit)
		{
			return Ok(_business.GetUsers(offset, limit));
		}

		// GET user/5
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			var userGet = _business.GetUserById(id);

			if (userGet != null)
				return Ok(userGet);
			if (id < 0)
				return BadRequest();
			return NotFound("User not found.");
		}

		// POST user
		[AllowAnonymous]
		[HttpPost]
		public IActionResult Post([FromForm] UserCreationModel newUser)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var success = _business.AddNewUser(newUser);

			if (success != null)
				return Created("users", success);
			return Conflict(new { message = "Account already exists" });
		}

		// PATCH user/5
		[HttpPatch("{id}")]
		public IActionResult Put(int id, [FromForm] UserUpdateModel newUser)
		{
			if(!ModelState.IsValid || id < 0)
				return BadRequest();

			var currentUser = _business.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized();

			if (currentUser.Id != id)
				return Unauthorized();

			var success = _business.UpdateUserById(id, newUser, currentUser.Id);

			return success switch
			{
				1 => (IActionResult) Ok(),
				2 => NotFound(),
				_ => BadRequest()
			};
		}

		// DELETE user/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = _business.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			var success = _business.DeleteUserById(id, currentUser.Id);

			return success switch
			{
				1 => (IActionResult) Ok(),
				2 => Unauthorized(),
				_ => BadRequest()
			};
		}

		// POST user/authenticate
		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromForm]UserAuthModel authUser)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var userEntity = _business.Authenticate(authUser);

			if (userEntity == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(userEntity);
		}

		// GET user/current
		[HttpGet("current")]
		public ActionResult<string> GetCurrentUser()
		{
			return Ok(_business.GetCurrentUser(User));
		}

		// GET user/search
		[AllowAnonymous]
		[HttpGet("search")]
		public ActionResult<string> Search([FromQuery] string username,
											[FromQuery] string email,
											[FromQuery] int offset,
											[FromQuery] int limit)
		{
			if (!string.IsNullOrEmpty(username))
			{
				return Ok(_business.SearchUserByUsername(username, offset, limit));
			}
			if (!string.IsNullOrEmpty(email))
			{
				return Ok(_business.SearchUserByEmail(email, offset, limit));
			}

			return BadRequest("No username or email specified");
		}
	}
}
