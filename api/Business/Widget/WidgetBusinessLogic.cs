using area.Contexts;
using area.Helpers;
using area.Models.User;
using area.Models.Widget;
using area.Models.WidgetConf;
using area.Models.WidgetParam;
using area.Repositories.Widget;
using area.Repositories.WidgetConf;
using area.Repositories.WidgetParam;

namespace area.Business.Widget
{
    public class WidgetBusinessLogic : IWidgetBusinessLogic
    {
        private readonly IWidgetRepository _widgetRepository;
        private readonly IWidgetParamRepository _widgetParamRepository;
        private readonly IWidgetConfRepository _widgetConfRepository;

        public WidgetBusinessLogic(AreaContext context)
        {
            _widgetRepository = new WidgetRepository(context);
            _widgetParamRepository = new WidgetParamRepository(context);
            _widgetConfRepository = new WidgetConfRepository(context);
        }
        
        public WidgetModel[] GetWidgets(int offset, int limit)
        {
            (offset, limit) = RangeHelper.CheckRange(offset, limit, 20);

            return _widgetRepository.GetWidgets(offset, limit);
        }

        public WidgetModel GetWidgetById(uint id)
        {
            return _widgetRepository.GetWidgetById(id);
        }

        public WidgetParamModel GetWidgetParam(uint widgetId)
        {
            var widget = _widgetRepository.GetWidgetById(widgetId);

            return widget != null ? _widgetParamRepository.GetWidgetParamByWidgetId(widgetId) : null;
        }

        public WidgetConfModel[] GetWidgetConf(uint widgetId, UserPublicModel user, int offset, int limit)
        {
            (offset, limit) = RangeHelper.CheckRange(offset, limit, 20);
            var widget = _widgetRepository.GetWidgetById(widgetId);
            
            return widget != null ? _widgetConfRepository.GetWidgetConfByWidgetUserId(widgetId, user.Id, offset, limit) : null;
        }

        public WidgetConfModel AddWidgetConf(int widgetId, UserPublicModel user, WidgetConfCreationModel newConf)
        {
            var widgetConf = new WidgetConfModel {Conf = newConf.Conf};
            
	        return _widgetConfRepository.AddNewWidgetConf(widgetConf) == 1 ? _widgetConfRepository.GetLastWidgetConfByUserId(user.Id) : null;
        }
    }
}