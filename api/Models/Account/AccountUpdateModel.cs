using System.ComponentModel.DataAnnotations;

namespace area.Models
{
	public partial class AccountUpdateModel
	{
		[StringLength(100)]
		public string Token { get; set; }

		public int? ProvId { get; set; }
	}
}
