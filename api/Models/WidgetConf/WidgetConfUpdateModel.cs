using System.ComponentModel.DataAnnotations;

namespace area.Models.WidgetConf
{
    public partial class WidgetConfUpdateModel
    {
        [Required]
		public string Conf { get; set; }
    }
}