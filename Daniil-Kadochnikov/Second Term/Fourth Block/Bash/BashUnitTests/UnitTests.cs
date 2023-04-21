using Bash;
using Commands;
using NUnit.Framework;
using System;
using System.IO;

namespace BashUnitTests
{
	public class Tests
	{
		[Test]
		public void EchoTest()
		{
			ICommand command = new EchoCommand();
			Assert.AreEqual(command.Name, "echo");
			string[] arguments = new string[] { "Hi", "there!" };
			string[] result = new string[] { "Hi there!" };
			Assert.AreEqual(command.Execute(arguments), result);
		}

		[Test]
		public void CatTest()
		{
			ICommand command = new CatCommand();
			Assert.AreEqual(command.Name, "cat");
			string[] arguments = new string[] { "../../../TestFiles/TestFile1.txt", "../../../TestFiles/TestFile2.txt" };
			string[] result = new string[] { "File name: ../../../TestFiles/TestFile1.txt", "Hello!", "My name is Daniil!", "TestFile1.txt", "../../../../BashUnitTests/TestFiles/TestFile2.txt", "File name: ../../../TestFiles/TestFile2.txt", "some text that makes no sence" };
			Assert.AreEqual(command.Execute(arguments), result);
			try
			{
				arguments = new string[] { "SomeFileThatDoesNorExist.txt" };
				command.Execute(arguments);
			}
			catch (Exception ex)
			{
				Assert.AreEqual(ex.Message, "The file \"SomeFileThatDoesNorExist.txt\" was not found.");
			}
		}

		[Test]
		public void PwdTest()
		{
			Directory.SetCurrentDirectory("../../../TestFiles");
			var returnValue = new PwdCommand().Execute(new string[0]);
			Directory.SetCurrentDirectory("../bin/Debug/net6.0");
			Assert.AreEqual(returnValue[1], "TestFile1.txt");
			Assert.AreEqual(returnValue[2], "TestFile2.txt");
			Assert.Pass();
		}

		[Test]
		public void WcTest()
		{
			ICommand command = new WcCommand();
			Assert.AreEqual(command.Name, "wc");
			string[] arguments = new string[] { "../../../TestFiles/TestFile1.txt", "../../../TestFiles/TestFile2.txt" };
			string[] result = new string[] { "File name: ../../../TestFiles/TestFile1.txt", "Lines: 4, Words: 4, Bytes: 92", "File name: ../../../TestFiles/TestFile2.txt", "Lines: 1, Words: 6, Bytes: 29" };
			Assert.AreEqual(command.Execute(arguments), result);
		}

		[Test]
		public void VariableManagerTest()
		{
			VariableManager variableManager = new VariableManager();
			variableManager.AssignVariable("$a=5");
			string[] command = new string[] { "someCommand", "$a" };
			variableManager.ReplaceVariables(ref command);
			Assert.AreEqual(command[1], "5");

			variableManager.AssignVariable("$a=sdsf");
			command = new string[] { "someCommand", "$a" };
			variableManager.ReplaceVariables(ref command);
			Assert.AreEqual(command[1], "sdsf");

			try
			{
				variableManager.AssignVariable("$a= ");
			}
			catch (Exception ex)
			{
				Assert.AreEqual(ex.Message, "Mistake in syntax. Variable \"a\" was not defined.\nOne or both of the argumnets were not provided.");
			}

			try
			{
				variableManager.AssignVariable("$octopus$=8");
			}
			catch (Exception ex)
			{
				Assert.AreEqual(ex.Message, "Mistake in syntax. Variable \"octopus$\" was not defined.\nThe name of the variable can't contain \"$\".");
			}

			try
			{
				command = new string[] { "someCommand", "$b" };
				variableManager.ReplaceVariables(ref command);
			}
			catch (Exception ex)
			{
				Assert.AreEqual(ex.Message, "Unknown variable \"b\".");
			}
		}

		[Test]
		public void ÑonveyorTest()
		{
			MyBash bash = new MyBash();
			string command = "cat ../../../TestFiles/TestFile1.txt | wc";
			string output = bash.ParceInput(command);
			Assert.AreEqual(output, "File name: ../../../../BashUnitTests/TestFiles/TestFile2.txt\nLines: 1, Words: 6, Bytes: 29");
			command = "../../../TestFiles/TestFile1.txt";
		}
	}
}