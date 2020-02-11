using area.Models.WidgetConf;

namespace area.Repositories.WidgetConf
{
    public interface IWidgetConfRepository
    {
        WidgetConfModel[] GetWidgetConfs(int offset, int limit);
        WidgetConfModel GetWidgetConfById(int id);
        WidgetConfModel[] GetWidgetConfByUserId(int userId, int offset, int limit);
        WidgetConfModel[] GetWidgetConfByWidgetId(int widgetId, int offset, int limit);
        WidgetConfModel[] GetWidgetConfByWidgetUserId(int widgetId, int userId, int offset, int limit);
        int DeleteWidgetConf(WidgetConfModel model);
        int AddNewWidgetConf(WidgetConfModel model);
        int UpdateWidgetConf(WidgetConfModel target, WidgetConfModel model);
    }
}