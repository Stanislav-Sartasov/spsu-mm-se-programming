using NUnit.Framework;
using Core;
using System.Collections.Generic;
using System.IO;
using CommandLib;
using CommandResolverLib;
using Moq;
using System;

namespace Task8.UnitTests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\TestCatalog");
        }

        [Test]
        public void MySplitTest()
        {
            // echo asd asd"a sd" "a sd"asd asd"as d"asd asd"as d"asd"asd"asd
            string[] test1 = Analyser.MySplit("echo asd asd\"a sd\" \"a sd\"asd asd\"as d\"asd asd\"as d\"asd\"asd\"asd", ' ');
            Assert.AreEqual(new string[] { "echo", "asd", "asd\"a sd\"", "\"a sd\"asd", "asd\"as d\"asd", "asd\"as d\"asd\"asd\"asd" }, test1);
            //echo | "asd |asd" | cat "asd| "ASD | sad
            string[] test2 = Analyser.MySplit("echo | \"asd |asd\" | cat \"asd| \"ASD | sad", '|');
            Assert.AreEqual(new string[] { "echo ", " \"asd |asd\" ", " cat \"asd| \"ASD ", " sad" }, test2);
            //echo > asd"a sd>asd" > "sda"
            string[] test3 = Analyser.MySplit("echo > asd\"a sd>asd\" > \"sda\"", '>');
        }

        [Test]
        public void EXPORTTest()
        {
            string[] args = new string[] { "test1=asda sd", "test2=as dasd", "test3====", "test4=", "test5" };
            EXPORTCommand export;
            foreach (string arg in args)
            {
                export = new EXPORTCommand(new string[] { arg });
                export.Run();
            }
            export = new EXPORTCommand(new string[] { "test6=ASD", "test7=DSA" });
            export.Run();
            Assert.AreEqual("asda sd", Environ.GetLocalVar("$test1"));
            Assert.AreEqual("as dasd", Environ.GetLocalVar("$test2"));
            Assert.AreEqual("===", Environ.GetLocalVar("$test3"));
            Assert.AreEqual("", Environ.GetLocalVar("$test4"));
            Assert.AreEqual("", Environ.GetLocalVar("$test5"));
            Assert.AreEqual("ASD", Environ.GetLocalVar("$test6"));
            Assert.AreEqual("DSA", Environ.GetLocalVar("$test7"));
            Environ.DefaultSet();
        }

        [Test]
        public void SubstitutionTest()
        {
            var dict = new Dictionary<string, string>() { { "$test1", "ASD" } };

            Assert.AreEqual("ASD", Analyser.Substitution("$test1", dict));
            Assert.AreEqual("asd\"ASD\"", Analyser.Substitution("asd\"$test1\"", dict));
            Assert.AreEqual("asdASDasd", Analyser.Substitution("asd${test1}asd", dict));
            Assert.AreEqual("asd\"ASD\"asd", Analyser.Substitution("asd\"${test1}\"asd", dict));
            Assert.AreEqual("asd\"\"asd", Analyser.Substitution("asd\"${ASD}\"asd", dict));
        }

        [Test]
        public void StringInfoTest()
        {
            StringInfo info = new StringInfo("this string \r\n has \n 3 strings, \r10 words and 52 bytes");
            Assert.AreEqual("3 10 54", info.GetAllInfo());
        }

        [Test]
        public void ErrorTest()
        {
            Error error = new Error();
            error.StdErr = 0;
            error.Message = "Some error messege";
            Assert.AreEqual(0, error.StdErr);
            Assert.AreEqual("Some error messege", error.Message);
        }

        [Test]
        public void PWDTest()
        {
            string[] args = new string[] { "" };
            PWDCommand pwd = new PWDCommand(args);
            Environ.SetCurrentDirectory(@"C:\catalog1\catalog2\catalog3");
            pwd.Run();
            Assert.AreEqual(@"C:\catalog1\catalog2\catalog3", pwd.GetStdOut());
            Environ.DefaultSet();
        }

        [Test]
        public void ExitTest()
        {
            string[] args = new string[] { "" };
            EXITCommand exit = new EXITCommand(args);
            exit.Run();
            Assert.AreEqual(null, exit.GetStdOut());
            Environ.DefaultSet();
        }
        [Test]
        public void ECHOTest()
        {
            string[] args = new string[] { "asd", " 231   __123", "ЧЯМИТЧЯИТЮЧСЮМИТМЧСЮБИЧИСТЮ" };
            ECHOCommand echo = new ECHOCommand(args);
            echo.Run();
            Assert.AreEqual("asd  231   __123 ЧЯМИТЧЯИТЮЧСЮМИТМЧСЮБИЧИСТЮ", echo.GetStdOut());
        }

        [Test]
        public void HELPTest()
        {
            string[] args = new string[] { "echo", "cat", "cd" };
            HELPCommand help = new HELPCommand(args);
            help.Run();
            Assert.AreEqual("echo [arg1] [arg2] ... - Display a line of text\n" + 
                            "cat [arg1] [arg2] ... - Concatenate files and print on the standard output\n" +
                            "cd [directory] - Change to directory by absolute or relative path ", help.GetStdOut());
            help = new HELPCommand(new string[] { });
            help.Run();
            Assert.AreEqual("echo [arg1] [arg2] ... - Display a line of text\n" +
                            "cat [arg1] [arg2] ... - Concatenate files and print on the standard output\n" +
                           "export [var1=value1] [var2=value2] ... - Variable declaration\n" +
                                "\t\tcall variable: $var; \"${var}\"\n" +
                            "cd [directory] - Change to directory by absolute or relative path \n" +
                            "pwd - Shows current working directory\n" +
                            "ls - [dir1] [dir2] ... - Shows all files and catalogs in directories by path\n" +
                                 "\t\talternative : ls - shows files and catalog in current directory\n" +
                            "exit [errCode] - Exit the shell with status errCode\n" +
                            "touch [file1path] [file2path] ... - Creates files if they don't exists \n" +
                            "mkdir [dir1path] [dir2path] ... - Creates directories if they don't exists\n" +
                            "rm [file1path] [file2path] ... - Removes existing files\n" +
                            "rmdir [dir1path] [dir2path] ... - Removes existing directories\n" +
                            "wc [file1path] [file2path] ... - Print newline, word, and byte counts for each file\n" +
                            "help [commandName1] [commandName2]... - Shows info for commands\n" +
                                "\t\talternative : help - shows info for all commands", help.GetStdOut());
            help = new HELPCommand(new string[] {"some not existing command"});
            help.Run();
            Assert.AreEqual("-bash: help: no help topics match `some not existing command'\n", help.GetErrorMessage());
        }

        [Test]
        public void RMTest()
        {
            string[] args = new string[]
            {
               "TestCatalog\\file1.txt",
               "TestCatalog\\file2.txt",
               "TestCatalog\\directory"
            };
            string directoryPath = Environ.GetCurrentDirectory();
            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory");
            RMCommand rm;
            rm = new RMCommand(args);
            rm.Run();
            Assert.AreEqual("rm: cannot remove 'TestCatalog\\file2.txt': No such file or directory" +
                "\nrm: cannot remove 'TestCatalog\\directory': Is a directory\n", rm.GetErrorMessage());
            Assert.AreEqual(false, File.Exists(directoryPath + @"\TestCatalog\file1.txt"));
            Assert.AreEqual(true, Directory.Exists(directoryPath + @"\TestCatalog\directory"));
            Directory.Delete(directoryPath + @"\TestCatalog\directory");
            Environ.DefaultSet();
        }

        [Test]
        public void RMDIRTest()
        {
            string[] args = new string[]
            {
               "TestCatalog\\directory1",
               "TestCatalog\\directory2",
               "TestCatalog\\directory3",
               "TestCatalog\\file1.txt"
            };
            string directoryPath = Environ.GetCurrentDirectory();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory1");
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory3");
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory3\additionalDirectory");

            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();
            RMDIRCommand rmdir;
            rmdir = new RMDIRCommand(args);
            rmdir.Run();

            Assert.AreEqual("rmdir: failed to remove 'TestCatalog\\directory2': No such file or directory"
                + "\nrmdir: failed to remove 'TestCatalog\\directory3': Directory not empty"
                + "\nrmdir: failed to remove 'TestCatalog\\file1.txt': Not a directory\n", rmdir.GetErrorMessage());
            Assert.AreEqual(false, Directory.Exists(directoryPath + @"\TestCatalog\directory1"));
            Assert.AreEqual(true, Directory.Exists(directoryPath + @"\TestCatalog\directory3"));
            Assert.AreEqual(true, File.Exists(directoryPath + @"\TestCatalog\file1.txt"));
            File.Delete(directoryPath + @"\TestCatalog\file1.txt");
            Directory.Delete(directoryPath + @"\TestCatalog\directory3\additionalDirectory");
            Directory.Delete(directoryPath + @"\TestCatalog\directory3");
            Environ.DefaultSet();
        }

        [Test]
        public void MKDIRTest()
        {
            string[] args = new string[]
            {
               "TestCatalog\\directory1",
               "TestCatalog\\direc:tory2",
               "TestCatalog\\directory3"
            };
            string directoryPath = Environ.GetCurrentDirectory();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory1");

            MKDIRCommand mkdir;
            mkdir = new MKDIRCommand(args);
            mkdir.Run();

            Assert.AreEqual("mkdir: cannot create directory ‘TestCatalog\\directory1’: File exists" +
                "\nmkdir: cannot create directory ‘TestCatalog\\direc:tory2’: Not supported name\n", mkdir.GetErrorMessage());
            Assert.AreEqual(true, Directory.Exists(directoryPath + @"\TestCatalog\directory3"));
            Directory.Delete(directoryPath + @"\TestCatalog\directory1");
            Directory.Delete(directoryPath + @"\TestCatalog\directory3");
            Environ.DefaultSet();
        }
        [Test]
        public void TOUCHTest()
        {
            string[] args = new string[]
            {
               "TestCatalog\\file1.txt",
               "TestCatalog\\notExistingFolder\\file2.txt",
               "TestCatalog\\file3.txt"
            };
            string directoryPath = Environ.GetCurrentDirectory();

            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();

            TOUCHCommand touch;
            touch = new TOUCHCommand(args);
            touch.Run();

            Assert.AreEqual("touch: cannot touch TestCatalog\\notExistingFolder\\file2.txt: No such file or directory\n", touch.GetErrorMessage());
            Assert.AreEqual(true, File.Exists(directoryPath + @"\TestCatalog\file1.txt"));
            Assert.AreEqual(true, File.Exists(directoryPath + @"\TestCatalog\file3.txt"));


            File.Delete(directoryPath + @"\TestCatalog\file1.txt");
            File.Delete(directoryPath + @"\TestCatalog\file3.txt");
            Environ.DefaultSet();
        }

        [Test]
        public void LSTest()
        {
            string[] args = new string[]
            {
               "TestCatalog",
               "TestCatalog\\IamNotExist",
               "TestCatalog\\file1.txt"
            };
            string directoryPath = Environ.GetCurrentDirectory();

            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory1");

            LSCommand ls;
            ls = new LSCommand(args);
            ls.Run();

            Assert.AreEqual("TestCatalog:\n" +
                            "directory1 file1.txt \n" +
                            "\nTestCatalog\\file1.txt ", ls.GetStdOut());

            Assert.AreEqual("ls: cannot access 'TestCatalog\\IamNotExist': No such directory\n", ls.GetErrorMessage());
            ls = new LSCommand(new string[] { });
            ls.Run();

            File.Delete(directoryPath + @"\TestCatalog\file1.txt");
            Directory.Delete(directoryPath + @"\TestCatalog\directory1");
            Environ.DefaultSet();
        }

        [Test]
        public void CDTest()
        {
            CDCommand cd;

            string[] args = new string[]
            {
               "..",
               "TestCatalog\\file.txt",
               "C:\\"
            };

            string current = Directory.GetCurrentDirectory();
            File.Create(current + @"\TestCatalog\file.txt").Close();

            cd = new CDCommand(new string[] { args[0] });
            cd.Run();
            Assert.AreEqual(current[..^7], Environ.GetCurrentDirectory());

            cd = new CDCommand(new string[] { args[1] });
            cd.Run();
            Assert.AreEqual($"cd: \'{args[1]}\': No such directory\n", cd.GetErrorMessage());

            cd = new CDCommand(new string[] { args[1], args[2] });
            cd.Run();
            Assert.AreEqual("cd: too many arguments\n", cd.GetErrorMessage());

            cd = new CDCommand(new string[] { args[2] });
            cd.Run();
            Assert.AreEqual(@"C:\", Environ.GetCurrentDirectory());

            cd = new CDCommand(new string[] { });
            cd.Run();
            Assert.AreEqual(current, Environ.GetCurrentDirectory());

            cd = new CDCommand(new string[] { });
            cd.Run();
            Assert.AreEqual(current, Environ.GetCurrentDirectory());

            cd = new CDCommand(new string[] { args[1] });
            cd.Run();
            Assert.AreEqual($"cd: \'{args[1]}\': Not a directory\n", cd.GetErrorMessage());

            File.Delete(current + @"\TestCatalog\file.txt");
            Environ.DefaultSet();
        }

        [Test]
        public void STDTest()
        {
            STDCommand std;
            CommandCreator cc = new CommandCreator();
            string current = Directory.GetCurrentDirectory();
            List<string[]> args = new List<string[]>()
            {
                new string[] {"echo ASD", "file.txt"},
                new string[] {"echo ASD", "file1.txt"},
                new string[] {"echo ASD", "file.txt", "file2.txt asd"},
                new string[] {"echo ASD", "directory1" },
                new string[] {"echo ASD", "\\someDirectory\\file.txt" },
            };

            File.Create(current + @"\TestCatalog\file2.txt").Close();
            Directory.CreateDirectory(current + @"\TestCatalog\directory1");

            new CDCommand(new string[] { "TestCatalog" }).Run();

            std = new STDCommand(args[0], cc);
            std.Run();
            Assert.AreEqual("ASD", File.ReadAllText(current + @"\TestCatalog\file.txt"));

            std = new STDCommand(args[1], cc);
            std.Run();
            Assert.That(File.Exists(current + @"\TestCatalog\file1.txt"));
            Assert.AreEqual("ASD", File.ReadAllText(current + @"\TestCatalog\file1.txt"));

            std = new STDCommand(args[2], cc);
            std.Run();
            Assert.That(File.Exists(current + @"\TestCatalog\file2.txt"));
            Assert.AreEqual("", File.ReadAllText(current + @"\TestCatalog\file.txt"));
            Assert.AreEqual("ASDasd ", File.ReadAllText(current + @"\TestCatalog\file2.txt"));

            std = new STDCommand(args[3], cc);
            std.Run();
            Assert.AreEqual("-mybash: directory1: Is a directory\n", std.GetErrorMessage());

            std = new STDCommand(args[4], cc);
            std.Run();
            Assert.AreEqual("-mybash: \\someDirectory\\file.txt: No sush file or directory\n", std.GetErrorMessage());

            File.Delete(current + @"\TestCatalog\file.txt");
            File.Delete(current + @"\TestCatalog\file1.txt");
            File.Delete(current + @"\TestCatalog\file2.txt");
            Directory.Delete(current + @"\TestCatalog\directory1");
            Environ.DefaultSet();
        }

        [Test]
        public void WCTest()
        {
            string current = Directory.GetCurrentDirectory();
            List<string[]> args = new List<string[]>()
            {
                new string[] {"TestCatalog\\file.txt"},
                new string[] {"TestCatalog\\file.txt", "TestCatalog\\file1.txt"},
                new string[] {"TestCatalog\\directory"},
                new string[] {"TestCatalog\\notRealfile.txt"},
                new string[] { },
            };
            File.Create(current + @"\TestCatalog\file.txt").Close();
            File.Create(current + @"\TestCatalog\file1.txt").Close();
            File.Create(current + @"\TestCatalog\file2.txt").Close();
            Directory.CreateDirectory(current + @"\TestCatalog\directory");
            File.WriteAllLines(current + @"\TestCatalog\file.txt", new string[] { "asd as" });
            File.WriteAllLines(current + @"\TestCatalog\file1.txt", new string[] { "asad", "assadd asd" });
            WCCommand wc;

            wc = new WCCommand(args[0]);
            wc.Run();
            Assert.AreEqual("2 2 8", wc.GetStdOut());

            wc = new WCCommand(args[1]);
            wc.Run();
            Assert.AreEqual("2 2 8 TestCatalog\\file.txt\n" +
                            "3 3 18 TestCatalog\\file1.txt\n" +
                            "5 5 26 total", wc.GetStdOut());

            wc = new WCCommand(args[2]);
            wc.Run();
            Assert.AreEqual("wc: TestCatalog\\directory: Is a directory\n", wc.GetErrorMessage());

            wc = new WCCommand(args[3]);
            wc.Run();
            Assert.AreEqual("wc: TestCatalog\\notRealfile.txt: No such file or directory\n", wc.GetErrorMessage());

            wc = new WCCommand(args[4]);
            wc.SetStdIn("\r std in");
            wc.Run();
            Assert.AreEqual("2 2 8", wc.GetStdOut());



            File.Delete(current + @"\TestCatalog\file.txt");
            File.Delete(current + @"\TestCatalog\file1.txt");
            File.Delete(current + @"\TestCatalog\file2.txt");
            Directory.Delete(current + @"\TestCatalog\directory");

            Environ.DefaultSet();
        }
        [Test]
        public void UNKNOWNTest()
        {
            UNKNOWNCommand unknown = new UNKNOWNCommand("notExisting");
            unknown.Run();
            Assert.AreEqual("notExisting: command not found\n",unknown.GetErrorMessage());
        }
            
        
        [Test]
        public void CATTest()
        {
            string current = Directory.GetCurrentDirectory();
            List<string[]> args = new List<string[]>()
            {
                new string[] {"TestCatalog\\file.txt"},
                new string[] {"TestCatalog\\file.txt", "TestCatalog\\file1.txt"},
                new string[] {"TestCatalog\\directory"},
                new string[] {"TestCatalog\\notRealfile.txt"},
                new string[] { },
            };
            File.Create(current + @"\TestCatalog\file.txt").Close();
            File.Create(current + @"\TestCatalog\file1.txt").Close();
            File.Create(current + @"\TestCatalog\file2.txt").Close();
            Directory.CreateDirectory(current + @"\TestCatalog\directory");
            File.WriteAllLines(current + @"\TestCatalog\file.txt", new string[] { "asd as" });
            File.WriteAllLines(current + @"\TestCatalog\file1.txt", new string[] { "asad", "assadd asd" });
            CATCommand cat;

            cat = new CATCommand(args[0]);
            cat.Run();
            Assert.AreEqual("asd as\r\n", cat.GetStdOut());

            cat = new CATCommand(args[1]);
            cat.Run();
            Assert.AreEqual("asd as\r\nasad\r\nassadd asd\r\n", cat.GetStdOut());

            cat = new CATCommand(args[2]);
            cat.Run();
            Assert.AreEqual("cat: TestCatalog\\directory: Is a directory\n", cat.GetErrorMessage());

            cat = new CATCommand(args[3]);
            cat.Run();
            Assert.AreEqual("cat: TestCatalog\\notRealfile.txt: No such file or directory\n", cat.GetErrorMessage());

            cat = new CATCommand(args[4]);
            cat.SetStdIn("some stdin");
            cat.Run();
            Assert.AreEqual("some stdin", cat.GetStdOut());

            File.Delete(current + @"\TestCatalog\file.txt");
            File.Delete(current + @"\TestCatalog\file1.txt");
            File.Delete(current + @"\TestCatalog\file2.txt");
            Directory.Delete(current + @"\TestCatalog\directory");

            Environ.DefaultSet();
        }

        [Test]
        public void SessionTest()
        {
            string[] commands = new string[]
            {
                "export a=file",
                "cd TestCatalog",
                "mkdir NewDirectory",
                "cd NewDirectory Misclick",
                "cd NewDirectory",
                "pwd",
                "touch file1.txt",
                "echo NewFileString > \"$a\"2.txt addString",
                "ls > ${a}1.txt",
                "cat \"$a\"1.txt file2.txt",
                "wc file1.txt",
                "cat file1.txt ${a}2.txt | wc",
                "rm file1.txt file2.txt file3.txt",
                "rmdir NewDirectory",
                "rmdir ..\\NewDirectory",
                "help pwd",
                "command",
                "exit",
            };
            string[] expected = new string[]
            {
                "",
                "",
                "",
                "cd: too many arguments\n",
                "",
                Directory.GetCurrentDirectory() + @"\TestCatalog\NewDirectory",
                "",
                "",
                "",
                "file1.txt file2.txt NewFileStringaddString ",
                "1 2 20",
                "1 3 43",
                "rm: cannot remove 'file3.txt': No such file or directory\n",
                "rmdir: failed to remove 'NewDirectory': No such file or directory\n",
                "",
                "pwd - Shows current working directory",
                "command: command not found\n",
                "exit",
            };
            var cc = new CommandCreator();
            var cr = new CommandResolver(cc);
            var session = new Session(cr);
            var testHandler = new TestHandler(commands);
            session.Handler = testHandler;
            session.Start();
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], testHandler.Commands[i]);
            }

            Environ.SetLocalVar("a", "newValue");

            Assert.AreEqual("newValue", Environ.GetLocalVar("$a"));

            Environ.DefaultSet();
        }
    }

}