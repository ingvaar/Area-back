using System.Text.Json.Serialization;

namespace area.Models
{
	public partial class AboutWidgetModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("params")]
		public string Params { get; set; }
	}
}
