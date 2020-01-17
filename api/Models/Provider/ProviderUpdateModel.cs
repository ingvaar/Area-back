using System.ComponentModel.DataAnnotations;

namespace area.Models
{
	public partial class ProviderUpdateModel
	{
		[StringLength(100)]
		public string Name { get; set; }

		[StringLength(100)]
		public string Url { get; set; }
	}
}
