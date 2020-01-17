using System.Text.Json.Serialization;

namespace area.Models
{
	public partial class AboutServerModel
	{
		[JsonPropertyName("current_time")]
		public int CurrentTime { get; set; }
		[JsonPropertyName("services")]
		public AboutServiceModel[] Services { get; set ;}
	}
}
