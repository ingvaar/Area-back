using area.Models.User;
using area.Models.WidgetConf;

namespace area.Business.WidgetConf
{
    public interface IWidgetConfBusinessLogic
    {
        public WidgetConfModel[] GetWidgetConfsByUserId(uint userId, int offset, int limit);
        public WidgetConfModel GetWidgetConf(int widgetConfId);
        public WidgetConfModel UpdateWidgetConf(int id, UserPublicModel currentUser, WidgetConfUpdateModel updateModel);
        public int DeleteWidgetConf(int id, UserPublicModel currentUser);
    }
}