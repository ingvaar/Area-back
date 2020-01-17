using System.ComponentModel.DataAnnotations;

namespace area.Models
{
	public partial class ProviderCreationModel
	{
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Required]
		[StringLength(100)]
		public string Url { get; set; }
	}
}
