using System;
using area.Contexts;
using area.Helpers;
using area.Models.User;
using area.Models.WidgetConf;
using area.Repositories.WidgetConf;

namespace area.Business.WidgetConf
{
    public class WidgetConfBusinessLogic : IWidgetConfBusinessLogic
    {
		private readonly IWidgetConfRepository _widgetConfRepository;

		public WidgetConfBusinessLogic(AreaContext context)
        {
            _widgetConfRepository = new WidgetConfRepository(context);
        }
        public WidgetConfModel[] GetWidgetConfs(int offset, int limit)
        {
            (offset, limit) = RangeHelper.CheckRange(offset, limit, 20);
            
            return _widgetConfRepository.GetWidgetConfs(offset, limit);
        }

        public WidgetConfModel GetWidgetConf(int widgetConfId)
        {
            return _widgetConfRepository.GetWidgetConfById(widgetConfId);
        }

        public WidgetConfModel UpdateWidgetConf(int id, UserPublicModel currentUser, WidgetConfUpdateModel updateModel)
        {
            var target = _widgetConfRepository.GetWidgetConfById(id);
            var toPatch = target;

            if (toPatch.UserId != currentUser.Id)
                return null;
            toPatch.Conf = updateModel.Conf;
            toPatch.UpdatedAt = DateTimeOffset.Now;

            return _widgetConfRepository.UpdateWidgetConf(target, toPatch) == 1 ? toPatch : null;
        }

        public int DeleteWidgetConf(int id, UserPublicModel currentUser)
        {
            var target = _widgetConfRepository.GetWidgetConfById(id);

            if (target.UserId != currentUser.Id)
                return 0;

            return _widgetConfRepository.DeleteWidgetConf(target) == 1 ? 1 : 0;
        }
    }
}