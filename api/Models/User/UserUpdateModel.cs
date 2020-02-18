using System.ComponentModel.DataAnnotations;

namespace area.Models.User
{
	public class UserUpdateModel
	{
		[StringLength(100)]
		public string Username { get; set; }

		[StringLength(100)]
		public string Password { get; set; }

		[StringLength(100)]
		public string Email { get; set; }
	}
}
