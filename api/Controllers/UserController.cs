using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using area.Configuration;
using area.Contexts;
using area.Helpers;
using area.Models;
using area.Repositories;

namespace area.Controllers
{
	[Authorize]
	[Route("/user")]
	public class UserController : Controller
	{
		private IUserRepository user;

		public UserController(AreaContext context, IOptions<AppSettings> appSettings)
		{
			user = new UserRepository(context, appSettings);
		}

		// GET user
		[AllowAnonymous]
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get([FromQuery] int offset,
													[FromQuery] int limit)
		{
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			return Ok(user.GetUsers(offset, limit));
		}

		// GET user/5
		[AllowAnonymous]
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			UserPublicModel userGet = user.GetUserById(id);

			if (userGet != null)
				return Ok(userGet);
			else if (id < 0)
				return BadRequest();
			else
				return NotFound("User not found.");
		}

		// POST user
		[AllowAnonymous]
		[HttpPost]
		public IActionResult Post([FromForm] UserCreationModel newUser)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			UserPublicModel success = user.AddNewUser(newUser);

			if (success != null)
				return Created("users", success);
			return BadRequest();
		}

		// PATCH user/5
		[HttpPatch("{id}")]
		public IActionResult Put(int id, [FromForm] UserUpdateModel newUser)
		{
			if(!ModelState.IsValid || id < 0)
				return BadRequest();

			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized();

			if (currentUser.Id != id)
				return Unauthorized();

			int success = user.UpdateUserById(id, newUser, currentUser.Id);

			if (success == 1)
				return Ok();
			else if (success == 2)
				return NotFound();

			return BadRequest();
		}

		// DELETE user/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var currentUser = user.GetCurrentUser(User);
			if (currentUser == null)
				return Unauthorized("Bad token.");

			int success = user.DeleteUserById(id, currentUser.Id);

			if (success == 1)
				return Ok();
			else if (success == 2)
				return Unauthorized();

			return BadRequest();
		}

		// POST user/authenticate
		[AllowAnonymous]
		[HttpPost("authenticate")]
		public IActionResult Authenticate([FromForm]UserAuthModel authUser)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var userEntity = user.Authenticate(authUser);

			if (userEntity == null)
				return BadRequest(new { message = "Username or password is incorrect" });

			return Ok(userEntity);
		}

		// GET user/current
		[HttpGet("current")]
		public ActionResult<string> GetCurrentUser()
		{
			return Ok(user.GetCurrentUser(User));
		}

		// GET user/search
		[AllowAnonymous]
		[HttpGet("search")]
		public ActionResult<string> Search([Required][FromQuery] string username,
											[FromQuery] int offset,
											[FromQuery] int limit)
		{
			(offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

			if (username == null || (username != null && username.Length < 1))
				return BadRequest("Invalid username.");

			return Ok(user.GetUserByUsername(username, offset, limit));
		}
	}
}
