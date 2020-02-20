using System.ComponentModel.DataAnnotations;

namespace area.Models.WidgetConf
{
    public class WidgetConfUpdateModel
    {
        [Required]
		public string Conf { get; set; }
    }
}