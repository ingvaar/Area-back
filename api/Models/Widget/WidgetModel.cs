namespace area.Models.Widget
{
	public abstract class WidgetModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int ServiceId { get; set; }
		public int ParamId { get; set; }
	}
}
