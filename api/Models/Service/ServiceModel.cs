using area.Models.Provider;

namespace area.Models.Service
{
	public partial class ServiceModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ProvId { get; set; }
		public ProviderModel Provider { get; set; }
	}
}
