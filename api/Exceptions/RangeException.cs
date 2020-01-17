using System;

namespace area.Exceptions
{
	public class RangeException : Exception
	{
		public RangeException()
		{
		}

		public RangeException(string message)
			: base(message)
		{
		}

		public RangeException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

}
