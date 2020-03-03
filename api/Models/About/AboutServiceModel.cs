using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace area.Models.About
{
	public class AboutServiceModel
	{
		[JsonPropertyName("name")]
		public string Name { get; set; }
		[JsonPropertyName("widgets")]
		public IEnumerable<AboutWidgetModel> Widgets { get; set ;}
	}
}
