using System.Collections.Generic;
using System.Linq;
using area.Contexts;
using area.Models.Widget;

namespace area.Repositories.Widget
{
    public class WidgetRepository : IWidgetRepository
    {
        private readonly AreaContext _context;

        public WidgetRepository(AreaContext context)
        {
            _context = context;
        }
        
        public WidgetModel[] GetWidgets(int offset, int limit)
        {
			return _context.Widget.OrderBy(p => p.Id)
				.Skip(offset)
				.Take(limit)
				.ToArray();
        }

        public WidgetModel GetWidgetById(uint id)
        {
			return _context.Widget.FirstOrDefault(a => a.Id == id);
        }

        public WidgetModel[] SearchWidgetByName(string name, int offset, int limit)
        {
			return _context.Widget.OrderBy(p => p.Id)
                .Where(w => w.Name.Contains(name))
				.Skip(offset)
				.Take(limit)
				.ToArray();
        }

        public IEnumerable<WidgetModel> GetWidgetsByServiceId(uint id, int offset, int limit)
        {
	        return _context.Widget.OrderBy(p => p.Id)
		        .Where(w => w.ServiceId == id)
		        .Skip(offset)
		        .Take(limit);
        }
    }
}