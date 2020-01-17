using System;

namespace area.Models
{
	public partial class AccountModel
	{
		public int Id { get; set; }
		public string Token { get; set; }
		public int ProvId { get; set; }
		public int UserId { get; set; }
		public DateTimeOffset LastUpdated { get; set; }
	}
}
