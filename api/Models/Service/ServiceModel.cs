using System.ComponentModel.DataAnnotations.Schema;
using area.Models.Provider;

namespace area.Models.Service
{
	public class ServiceModel
	{
		public uint Id { get; set; }
		public string Name { get; set; }
		public uint ProvId { get; set; }
		[ForeignKey("ProviderForeignKey")]
		public ProviderModel Provider { get; set; }
	}
}
