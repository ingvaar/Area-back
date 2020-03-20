using System.Linq;
using area.Contexts;
using area.Models.WidgetParam;
using area.Repositories.Widget;
using Microsoft.EntityFrameworkCore;

namespace area.Repositories.WidgetParam
{
    public class WidgetParamRepository : IWidgetParamRepository
    {
        private readonly DbSet<WidgetParamModel> _repository;
        private readonly WidgetRepository _widgetRepository;
        
        public WidgetParamRepository(AreaContext context)
        {
            _repository = context.WidgetParam;
            _widgetRepository = new WidgetRepository(context);
        }
        
        public WidgetParamModel[] GetWidgetParams(int offset, int limit)
        {
			return _repository.OrderBy(p => p.Id)
				.Skip(offset)
				.Take(limit)
				.ToArray();
        }

        public WidgetParamModel GetWidgetParamByWidgetId(uint widgetId)
        {
            var id = _widgetRepository.GetWidgetById(widgetId).ParamId;
            return _repository.SingleOrDefault(p => p.Id == id);
        }

        public WidgetParamModel GetWidgetParamById(int id)
        {
            return _repository.SingleOrDefault(p => p.Id == id);
        }
    }
}