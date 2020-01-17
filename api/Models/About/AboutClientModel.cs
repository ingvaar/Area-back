using System.Text.Json.Serialization;

namespace area.Models
{
	public partial class AboutClientModel
	{
		[JsonPropertyName("host")]
		public string Host { get; set; }
	}
}
