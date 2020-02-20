using System;

namespace area.Models.User
{
	public class UserModel
	{
		public uint Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public DateTimeOffset Date { get; set; }
	}
}
