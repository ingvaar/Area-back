using System.ComponentModel.DataAnnotations;

namespace area.Models.WidgetConf
{
	public partial class WidgetConfCreationModel
	{
		[Required]
		public string Conf { get; set; }
	}
}
