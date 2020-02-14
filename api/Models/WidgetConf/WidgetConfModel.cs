using System;
using area.Models.User;
using area.Models.Widget;

namespace area.Models.WidgetConf
{
	public class WidgetConfModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public UserModel User { get; set; }
		public int WidgetId { get; set; }
		public WidgetModel Widget { get; set; }
		public string Conf { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
