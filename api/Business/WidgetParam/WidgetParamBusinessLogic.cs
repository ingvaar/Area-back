using area.Contexts;
using area.Helpers;
using area.Models.WidgetParam;
using area.Repositories.WidgetParam;

namespace area.Business.WidgetParam
{
    public class WidgetParamBusinessLogic : IWidgetParamBusinessLogic
    {
        private readonly IWidgetParamRepository _widgetParamRepository;
        
        public WidgetParamBusinessLogic(AreaContext context)
        {
           _widgetParamRepository = new WidgetParamRepository(context); 
        }
                
        public WidgetParamModel[] GetWidgetParams(int offset, int limit)
        {
            (offset, limit) = RangeHelper.CheckRange(offset, limit, 20);
            return _widgetParamRepository.GetWidgetParams(offset, limit);
        }

        public WidgetParamModel GetWidgetParamById(int id)
        {
            return _widgetParamRepository.GetWidgetParamById(id);
        }
    }
}