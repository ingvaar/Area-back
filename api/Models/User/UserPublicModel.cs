﻿using System;

namespace area.Models.User
{
	public partial class UserPublicModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public DateTimeOffset Date { get; set; }
	}
}

