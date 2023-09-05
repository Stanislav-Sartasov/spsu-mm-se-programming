using System;

namespace BashLib.IO
{
	public class EnvironmentalExiter : IExiter
	{
		public void Exit()
		{
			Environment.Exit(0);
		}
	}
}