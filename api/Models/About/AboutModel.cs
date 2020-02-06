using System.Text.Json.Serialization;

namespace area.Models.About
{
	public class AboutModel
	{
		[JsonPropertyName("client")]
		public AboutClientModel Client { get; set; }
		[JsonPropertyName("server")]
		public AboutServerModel Server { get; set; }
	}
}
