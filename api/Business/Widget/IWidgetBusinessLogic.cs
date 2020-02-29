using area.Models.User;
using area.Models.Widget;
using area.Models.WidgetConf;
using area.Models.WidgetParam;

namespace area.Business.Widget
{
    public interface IWidgetBusinessLogic
    {
        WidgetModel[] GetWidgets(int offset, int limit);
        WidgetModel GetWidgetById(uint id);
        WidgetParamModel GetWidgetParam(uint widgetId);
        WidgetConfModel[] GetWidgetConf(uint widgetId, UserPublicModel user, int offset, int limit);
        WidgetConfModel AddWidgetConf(int widgetId, UserPublicModel user, WidgetConfCreationModel newConf);
    }
}