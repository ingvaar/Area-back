namespace area.Helpers
{
	public class RangeHelper
	{
		public static (int, int) CheckRange(int offset, int limit, int max = 50)
		{
			if (offset < 0)
				offset = 0;
			if (limit <= 0)
				limit = max;
			if (limit > max)
				limit = max;
			return (offset, limit);
		}
	}
}

