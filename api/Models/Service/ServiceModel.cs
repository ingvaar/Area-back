using System.ComponentModel.DataAnnotations.Schema;
using area.Models.Provider;

namespace area.Models.Service
{
	public partial class ServiceModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ProvId { get; set; }
		[ForeignKey("ProviderForeignKey")]
		public ProviderModel Provider { get; set; }
	}
}
