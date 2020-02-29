using System;

namespace area.Models.WidgetConf
{
	public class WidgetConfModel
	{
		public uint Id { get; set; }
		public uint UserId { get; set; }
		public uint WidgetId { get; set; }
		public string Conf { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset UpdatedAt { get; set; }
	}
}
