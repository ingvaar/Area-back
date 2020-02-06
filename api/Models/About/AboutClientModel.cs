using System.Text.Json.Serialization;

namespace area.Models.About
{
	public class AboutClientModel
	{
		[JsonPropertyName("host")]
		public string Host { get; set; }
	}
}
