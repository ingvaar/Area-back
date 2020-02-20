using System.ComponentModel.DataAnnotations;

namespace area.Models.WidgetConf
{
	public class WidgetConfCreationModel
	{
		[Required]
		public string Conf { get; set; }
	}
}
