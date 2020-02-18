using System;
using System.ComponentModel.DataAnnotations.Schema;
using area.Models.User;
using area.Models.Widget;

namespace area.Models.WidgetConf
{
	public class WidgetConfModel
	{
		public uint Id { get; set; }
		public uint UserId { get; set; }
		[ForeignKey("UserForeignKey")]
		public UserModel User { get; set; }
		public uint WidgetId { get; set; }
		[ForeignKey("WidgetForeignKey")]
		public WidgetModel Widget { get; set; }
		public string Conf { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
