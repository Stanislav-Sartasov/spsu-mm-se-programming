using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BashCommands;
using BashCommandsUnitTests.ClassesForCommandManagerTests;
using NUnit.Framework;

namespace BashCommandsUnitTests
{
    public class CommandManagerUnitTests
    {
        [Test]
        public void ParameterlessConstructorTest()
        {
            var manager = new CommandManager();
            Assert.IsTrue(manager.GetCommands().Count() == 8);
            Assert.IsTrue(manager.GetCommands().Count() == 0);
        }

        [Test]
        public void FirstParameterConstructorTest()
        {
            var manager = new CommandManager(Assembly.GetExecutingAssembly().Location);
            var commands = manager.GetCommands();
            Assert.That(commands.Count() == 9);
            Assert.That(commands.ToList().Exists(x => x.GetType() == typeof(TestCommand)));
        }

        [Test]
        public void SecondParameterConstructorTest()
        {
            var manager = new CommandManager(new List<string>() { Assembly.GetExecutingAssembly().Location });
            var commands = manager.GetCommands();
            Assert.That(commands.Count() == 9);
            Assert.That(commands.ToList().Exists(x => x.GetType() == typeof(TestCommand)));
        }

        [Test]
        public void RegisterTest()
        {
            var manager = new CommandManager();

            // case of appropriate class
            var result = manager.Register(new List<Type>() {typeof(TestCommand)});
            Assert.That(result is true);

            var commands = manager.GetCommands();
            Assert.That(commands.Count() > 0);

            // case of abstract class
            result = manager.Register(new List<Type>() { typeof(AbstractTestCommand) });
            Assert.That(result is false);

            commands = manager.GetCommands();
            Assert.That(commands.Count() == 0);
        }

        [Test]
        public void RemoveTest()
        {
            var manager = new CommandManager();

            // case of appropriate class
            var result = manager.Register(new List<Type>() { typeof(TestCommand) });
            Assert.That(result is true);
            result = manager.Remove(new List<Type>() { typeof(TestCommand) });
            Assert.That(result is true);

            var commands = manager.GetCommands();
            Assert.That(commands.Count() == 8);
        }

        [Test]
        public void AddCommandsFromAssemblyTest()
        {
            var manager = new CommandManager();
            var assemblyPath = Assembly.GetExecutingAssembly().Location;

            manager.AddCommandsFromAssembly(assemblyPath);

            var commands = manager.GetCommands();
            Assert.That(commands.Count() == 9);
            Assert.That(commands.ToList().Exists(x => x.GetType() == typeof(TestCommand)));
        }

        [Test]
        public void InvalidAssemblyLoadTest()
        {
            var manager = new CommandManager();

            manager.AddCommandsFromAssembly(@"Z:\Something that is not exist\myfile.dll");

            var commands = manager.GetCommands();
            Assert.That(commands.Count() == 8);
        }
    }
}
