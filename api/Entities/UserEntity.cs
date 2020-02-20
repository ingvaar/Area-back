using System;

namespace area.Entities
{
	public class UserEntity
	{
		public uint Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
		public DateTimeOffset Date { get; set; }
	}
}
