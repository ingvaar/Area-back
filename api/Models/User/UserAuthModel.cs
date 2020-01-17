using System.ComponentModel.DataAnnotations;

namespace area.Models
{
	public partial class UserAuthModel
	{
		[Required]
		[StringLength(100)]
		public string Username { get; set; }

		[Required]
		[StringLength(100)]
		public string Password { get; set; }
	}
}

