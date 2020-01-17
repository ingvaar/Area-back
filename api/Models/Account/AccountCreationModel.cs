using System.ComponentModel.DataAnnotations;

namespace area.Models
{
	public partial class AccountCreationModel
	{
		[Required]
		[StringLength(100)]
		public string Token { get; set; }

		[Required]
		public int ProvId { get; set; }

		[Required]
		public int UserId { get; set; }
	}
}
