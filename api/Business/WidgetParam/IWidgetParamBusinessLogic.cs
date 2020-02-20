using area.Models.WidgetParam;

namespace area.Business.WidgetParam
{
    public interface IWidgetParamBusinessLogic
    {
        public WidgetParamModel[] GetWidgetParams(int offset, int limit);
        public WidgetParamModel GetWidgetParamById(int id);
    }
}