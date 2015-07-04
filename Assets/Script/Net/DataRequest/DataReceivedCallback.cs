using System;

namespace NetManager
{
	public interface DataReceivedCallback<T>
	{
		void Result(T  data);
	}
}

