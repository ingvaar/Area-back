using System.Text.Json.Serialization;

namespace area.Models.About
{
	public class AboutWidgetModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("params")]
		public string Params { get; set; }
	}
}
