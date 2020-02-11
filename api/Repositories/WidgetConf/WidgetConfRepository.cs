using area.Contexts;
using area.Models.WidgetConf;
using Microsoft.EntityFrameworkCore;

namespace area.Repositories.WidgetConf
{
    public class WidgetConfRepository : IWidgetConfRepository
    {
        private readonly DbSet<WidgetConfModel> _repository;
        
        public WidgetConfRepository(AreaContext context)
        {
            _repository = context.WidgetConf;
        }

        public WidgetConfModel[] GetWidgetConfs(int offset, int limit)
        {
            throw new System.NotImplementedException();
        }

        public WidgetConfModel GetWidgetConfById(int id)
        {
            throw new System.NotImplementedException();
        }

        public WidgetConfModel[] GetWidgetConfByUserId(int userId, int offset, int limit)
        {
            throw new System.NotImplementedException();
        }

        public WidgetConfModel[] GetWidgetConfByWidgetId(int widgetId, int offset, int limit)
        {
            throw new System.NotImplementedException();
        }

        public WidgetConfModel[] GetWidgetConfByWidgetUserId(int widgetId, int userId, int offset, int limit)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteWidgetConf(WidgetConfModel model)
        {
            throw new System.NotImplementedException();
        }

        public int AddNewWidgetConf(WidgetConfModel model)
        {
            throw new System.NotImplementedException();
        }

        public int UpdateWidgetConf(WidgetConfModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}