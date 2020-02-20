using area.Models.WidgetParam;

namespace area.Repositories.WidgetParam
{
    public interface IWidgetParamRepository
    {
        WidgetParamModel[] GetWidgetParams(int offset, int limit);
        WidgetParamModel GetWidgetParamByWidgetId(int widgetId);
        WidgetParamModel GetWidgetParamById(int id);
    }
}