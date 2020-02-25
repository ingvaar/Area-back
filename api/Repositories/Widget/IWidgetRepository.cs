using area.Models.Widget;

namespace area.Repositories.Widget
{
    public interface IWidgetRepository
    {
        WidgetModel[] GetWidgets(int offset, int limit);
        WidgetModel GetWidgetById(uint id);
        WidgetModel[] SearchWidgetByName(string name, int offset, int limit);
        WidgetModel[] GetWidgetsByServiceId(int id, int offset, int limit);
    }
}