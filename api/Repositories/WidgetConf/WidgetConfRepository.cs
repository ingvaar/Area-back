using System.Linq;
using area.Contexts;
using area.Models.WidgetConf;
using Microsoft.EntityFrameworkCore;

namespace area.Repositories.WidgetConf
{
    public class WidgetConfRepository : IWidgetConfRepository
    {
        private readonly DbSet<WidgetConfModel> _repository;
        private readonly AreaContext _context;
        
        public WidgetConfRepository(AreaContext context)
        {
	        _context = context;
	        _repository = context.WidgetConf;
        }

        public WidgetConfModel[] GetWidgetConfs(int offset, int limit)
        {
			return _repository.OrderBy(p => p.Id)
				.Skip(offset)
				.Take(limit)
				.ToArray();
        }

        public WidgetConfModel GetWidgetConfById(int id)
        {
			return id < 0 ? null : _repository.SingleOrDefault(a => a.Id == id);
        }

        public WidgetConfModel[] GetWidgetConfByUserId(int userId, int offset, int limit)
        {
			return _repository.OrderBy(p => p.Id)
                .Where(c => c.UserId == userId)
				.Skip(offset)
				.Take(limit)
				.ToArray();
        }

        public WidgetConfModel[] GetWidgetConfByWidgetId(int widgetId, int offset, int limit)
        {
			return _repository.OrderBy(p => p.Id)
                .Where(c => c.WidgetId == widgetId)
				.Skip(offset)
				.Take(limit)
				.ToArray();
        }

        public WidgetConfModel[] GetWidgetConfByWidgetUserId(int widgetId, uint userId, int offset, int limit)
        {
			return _repository.OrderBy(p => p.Id)
                .Where(c => c.WidgetId == widgetId && c.UserId == userId)
				.Skip(offset)
				.Take(limit)
				.ToArray();
        }

        public WidgetConfModel GetLastWidgetConfByUserId(uint userId)
        {
	        return _repository.OrderBy(p => p.Id)
		        .Last(c => c.UserId == userId);
        }

        public int DeleteWidgetConf(WidgetConfModel model)
        {
			_repository.Remove(model);
			return _context.SaveChanges();
        }

        public int AddNewWidgetConf(WidgetConfModel model)
        {
			_repository.Add(model);
			return _context.SaveChanges();
        }

        public int UpdateWidgetConf(WidgetConfModel target, WidgetConfModel model)
        {
			_context.Entry(target).CurrentValues.SetValues(model);
			return _context.SaveChanges();
        }
    }
}