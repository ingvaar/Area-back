using System.ComponentModel.DataAnnotations;

namespace area.Models.User
{
	public class UserCreationModel
	{
		[Required]
		[StringLength(100)]
		public string Username { get; set; }

		[Required]
		[StringLength(100)]
		public string Password { get; set; }

		[Required]
		[EmailAddress]	
		[StringLength(100)]
		public string Email { get; set; }
	}
}
