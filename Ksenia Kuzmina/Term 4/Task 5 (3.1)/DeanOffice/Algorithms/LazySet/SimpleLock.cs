using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOffice.Algorithms.LazySet
{
	public class SimpleLock
	{
		private object obj = new object();

		public void Lock()
		{
			Monitor.Enter(obj);
		}

		public void Unlock()
		{
			Monitor.Exit(obj);
		}
	}
}
