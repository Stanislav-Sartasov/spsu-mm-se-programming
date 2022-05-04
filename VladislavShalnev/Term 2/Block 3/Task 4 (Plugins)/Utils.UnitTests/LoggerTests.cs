using System;
using NUnit.Framework;

namespace Utils.UnitTests;

public class LoggerTests
{
	[Test]
	public void LogTest()
	{
		Logger.Log("Log", ConsoleColor.Magenta, " :|");
		Assert.Pass();
	}
	
	[Test]
	public void SuccessTest()
	{
		Logger.Success("Success"," :)");
		Assert.Pass();
	}
	
	[Test]
	public void ErrorTest()
	{
		Logger.Error("Error"," :(");
		Assert.Pass();
	}
}