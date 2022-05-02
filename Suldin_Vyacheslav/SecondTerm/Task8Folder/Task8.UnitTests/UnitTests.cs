using NUnit.Framework;
using BABASH;
using System.Collections.Generic;
using System.IO;

namespace Task8.UnitTests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
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
            Session session = new Session();
            EXPORTCommand export;
            foreach (string arg in args)
            {
                export = new EXPORTCommand(new string[] { arg }, session);
                export.Execute();
            }
            export = new EXPORTCommand(new string[] { "test6=ASD", "test7=DSA" }, session);
            export.Execute();
            Assert.AreEqual("asda sd", session.GetLocalVar("$test1"));
            Assert.AreEqual("as dasd", session.GetLocalVar("$test2"));
            Assert.AreEqual("===", session.GetLocalVar("$test3"));
            Assert.AreEqual("", session.GetLocalVar("$test4"));
            Assert.AreEqual("", session.GetLocalVar("$test5"));
            Assert.AreEqual("ASD", session.GetLocalVar("$test6"));
            Assert.AreEqual("DSA", session.GetLocalVar("$test7"));
        }
        [Test]
        public void SubstitutionTest()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>() {
                {"$test1","ASD"},
            };
            Assert.AreEqual(Analyser.Substitution("$test1", dictionary), "ASD");
            Assert.AreEqual(Analyser.Substitution("asd\"$test1\"", dictionary), "asd\"ASD\"");
            Assert.AreEqual(Analyser.Substitution("asd${test1}asd", dictionary), "asdASDasd");
            Assert.AreEqual(Analyser.Substitution("asd\"${test1}\"asd", dictionary), "asd\"ASD\"asd");
            Assert.AreEqual(Analyser.Substitution("asd\"${ASD}\"asd", dictionary), "asd\"\"asd");
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
            Session session = new Session();
            string[] args = new string[] { "" };
            PWDCommand pwd = new PWDCommand(args, session);
            session.SetCurrentDirectory(@"C:\catalog1\catalog2\catalog3");
            pwd.Execute();
            Assert.AreEqual(@"C:\catalog1\catalog2\catalog3", pwd.GetStdOut());
        }

        [Test]
        public void ExitTest()
        {
            string[] args = new string[] { "" };
            EXITCommand exit = new EXITCommand(args, new Session());
            exit.Execute();
            Assert.AreEqual(null, exit.GetStdOut());
        }
        [Test]
        public void ECHOTest()
        {

            string[] args = new string[] { "" };
            ECHOCommand echo = new ECHOCommand(args, new Session());
            Session session = new Session();
            session.Resolve("export O=ooo");
            Assert.AreEqual("", session.Resolve("echo"));
            Assert.AreEqual("asd", session.Resolve("echo asd"));
            Assert.AreEqual("ooo", session.Resolve("echo $O"));
            Assert.AreEqual("ooo", session.Resolve("echo ${O}"));
            Assert.AreEqual("asd asd asd", session.Resolve("echo asd asd      asd"));
            Assert.AreEqual("asd asd      asd", session.Resolve("echo asd \"asd      asd\""));
            Assert.AreEqual("asdoooasd", session.Resolve("echo asd\"$O\"asd"));
            Assert.AreEqual("asd", session.Resolve("echo asd$Oasd"));
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
            Session session = new Session();
            string directoryPath = session.GetCurrentDirectory();
            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory");
            RMCommand rm;
            rm = new RMCommand(args, session);
            rm.Execute();
            Assert.AreEqual("rm: cannot remove 'TestCatalog\\file2.txt': No such file or directory" +
                "\nrm: cannot remove 'TestCatalog\\directory': Is a directory", rm.GetErrorMessage());
            Assert.AreEqual(false, File.Exists(directoryPath + @"\TestCatalog\file1.txt"));
            Assert.AreEqual(true, Directory.Exists(directoryPath + @"\TestCatalog\directory"));
            Directory.Delete(directoryPath + @"\TestCatalog\directory");
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
            Session session = new Session();
            string directoryPath = session.GetCurrentDirectory();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory1");
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory3");
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory3\additionalDirectory");

            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();
            RMDIRCommand rmdir;
            rmdir = new RMDIRCommand(args, session);
            rmdir.Execute();

            Assert.AreEqual("rmdir: failed to remove 'TestCatalog\\directory2': No such file or directory"
                + "\nrmdir: failed to remove 'TestCatalog\\directory3': Directory not empty"
                + "\nrmdir: failed to remove 'TestCatalog\\file1.txt': Not a directory", rmdir.GetErrorMessage());
            Assert.AreEqual(false, Directory.Exists(directoryPath + @"\TestCatalog\directory1"));
            Assert.AreEqual(true, Directory.Exists(directoryPath + @"\TestCatalog\directory3"));
            Assert.AreEqual(true, File.Exists(directoryPath + @"\TestCatalog\file1.txt"));
            File.Delete(directoryPath + @"\TestCatalog\file1.txt");
            Directory.Delete(directoryPath + @"\TestCatalog\directory3\additionalDirectory");
            Directory.Delete(directoryPath + @"\TestCatalog\directory3");
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
            Session session = new Session();
            string directoryPath = session.GetCurrentDirectory();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory1");

            MKDIRCommand mkdir;
            mkdir = new MKDIRCommand(args, session);
            mkdir.Execute();

            Assert.AreEqual("mkdir: cannot create directory ‘TestCatalog\\directory1’: File exists" +
                "\nmkdir: cannot create directory ‘TestCatalog\\direc:tory2’: Not supported name", mkdir.GetErrorMessage());
            Assert.AreEqual(true, Directory.Exists(directoryPath + @"\TestCatalog\directory3"));
            Directory.Delete(directoryPath + @"\TestCatalog\directory1");
            Directory.Delete(directoryPath + @"\TestCatalog\directory3");
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
            Session session = new Session();
            string directoryPath = session.GetCurrentDirectory();

            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();

            TOUCHCommand touch;
            touch = new TOUCHCommand(args, session);
            touch.Execute();

            Assert.AreEqual("touch: cannot touch TestCatalog\\notExistingFolder\\file2.txt: No such file or directory", touch.GetErrorMessage());
            Assert.AreEqual(true, File.Exists(directoryPath + @"\TestCatalog\file1.txt"));
            Assert.AreEqual(true, File.Exists(directoryPath + @"\TestCatalog\file3.txt"));


            File.Delete(directoryPath + @"\TestCatalog\file1.txt");
            File.Delete(directoryPath + @"\TestCatalog\file3.txt");
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
            Session session = new Session();
            string directoryPath = session.GetCurrentDirectory();

            File.Create(directoryPath + @"\TestCatalog\file1.txt").Close();
            Directory.CreateDirectory(directoryPath + @"\TestCatalog\directory1");

            LSCommand ls;
            ls = new LSCommand(args, session);
            ls.Execute();

            Assert.AreEqual("TestCatalog:\n" +
                            "directory1 file1.txt \n" +
                            "ls: cannot access 'TestCatalog\\IamNotExist': No such directory\n" +
                            "TestCatalog\\file1.txt", ls.GetErrorMessage());

            ls = new LSCommand(new string[] { }, session);
            ls.Execute();

            File.Delete(directoryPath + @"\TestCatalog\file1.txt");
            Directory.Delete(directoryPath + @"\TestCatalog\directory1");
        }

        [Test]
        public void CDTest()
        {
            Session session = new Session();
            CDCommand cd;

            string[] args = new string[]
            {
               "..",
               "TestCatalog\\file.txt",
               "C:\\"
            };

            string current = Directory.GetCurrentDirectory();
            File.Create(current + @"\TestCatalog\file.txt").Close();

            cd = new CDCommand(new string[] { args[0] }, session);
            cd.Execute();
            Assert.AreEqual(current[..^7], session.GetCurrentDirectory());

            cd = new CDCommand(new string[] { args[1] }, session);
            cd.Execute();
            Assert.AreEqual($"cd: \'{args[1]}\': No such directory", cd.GetErrorMessage());

            cd = new CDCommand(new string[] { args[1], args[2] }, session);
            cd.Execute();
            Assert.AreEqual("cd: too many arguments", cd.GetErrorMessage());

            cd = new CDCommand(new string[] { args[2] }, session);
            cd.Execute();
            Assert.AreEqual(@"C:\", session.GetCurrentDirectory());

            cd = new CDCommand(new string[] { }, session);
            cd.Execute();
            Assert.AreEqual(current, session.GetCurrentDirectory());

            cd = new CDCommand(new string[] { }, session);
            cd.Execute();
            Assert.AreEqual(current, session.GetCurrentDirectory());

            cd = new CDCommand(new string[] { args[1] }, session);
            cd.Execute();
            Assert.AreEqual($"cd: \'{args[1]}\': Not a directory", cd.GetErrorMessage());

            File.Delete(current + @"\TestCatalog\file.txt");
        }

        [Test]
        public void STDTest()
        {
            Session session = new Session();
            STDCommand std;
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

            new CDCommand(new string[] { "TestCatalog" }, session).Execute();

            std = new STDCommand(args[0], session);
            std.Execute();
            Assert.AreEqual("ASD", File.ReadAllText(current + @"\TestCatalog\file.txt"));

            std = new STDCommand(args[1], session);
            std.Execute();
            Assert.That(File.Exists(current + @"\TestCatalog\file1.txt"));
            Assert.AreEqual("ASD", File.ReadAllText(current + @"\TestCatalog\file1.txt"));

            std = new STDCommand(args[2], session);
            std.Execute();
            Assert.That(File.Exists(current + @"\TestCatalog\file2.txt"));
            Assert.AreEqual("", File.ReadAllText(current + @"\TestCatalog\file.txt"));
            Assert.AreEqual("ASDasd ", File.ReadAllText(current + @"\TestCatalog\file2.txt"));

            std = new STDCommand(args[3], session);
            std.Execute();
            Assert.AreEqual("-babach: directory1: Is a directory", std.GetErrorMessage());

            std = new STDCommand(args[4], session);
            std.Execute();
            Assert.AreEqual("-babach: \\someDirectory\\file.txt: No sush file or directory", std.GetErrorMessage());

            File.Delete(current + @"\TestCatalog\file.txt");
            File.Delete(current + @"\TestCatalog\file1.txt");
            File.Delete(current + @"\TestCatalog\file2.txt");
            Directory.Delete(current + @"\TestCatalog\directory1");
        }

        [Test]
        public void WCTest()
        {
            Session session = new Session();
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

            wc = new WCCommand(args[0], session);
            wc.Execute();
            Assert.AreEqual("2 2 8", wc.GetStdOut());

            wc = new WCCommand(args[1], session);
            wc.Execute();
            Assert.AreEqual("2 2 8 TestCatalog\\file.txt\n" +
                            "3 3 18 TestCatalog\\file1.txt\n" +
                            "5 5 26 total", wc.GetStdOut());

            wc = new WCCommand(args[2], session);
            wc.Execute();
            Assert.AreEqual("wc: TestCatalog\\directory: Is a directory", wc.GetErrorMessage());

            wc = new WCCommand(args[3], session);
            wc.Execute();
            Assert.AreEqual("wc: TestCatalog\\notRealfile.txt: No such file or directory", wc.GetErrorMessage());

            wc = new WCCommand(args[4], session);
            wc.SetStdIn("\r std in");
            wc.Execute();
            Assert.AreEqual("2 2 8", wc.GetStdOut());



            File.Delete(current + @"\TestCatalog\file.txt");
            File.Delete(current + @"\TestCatalog\file1.txt");
            File.Delete(current + @"\TestCatalog\file2.txt");
            Directory.Delete(current + @"\TestCatalog\directory");
        }

        [Test]
        public void CATTest()
        {
            Session session = new Session();
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

            cat = new CATCommand(args[0], session);
            cat.Execute();
            Assert.AreEqual("asd as\r\n", cat.GetStdOut());

            cat = new CATCommand(args[1], session);
            cat.Execute();
            Assert.AreEqual("asd as\r\nasad\r\nassadd asd\r\n", cat.GetStdOut());

            cat = new CATCommand(args[2], session);
            cat.Execute();
            Assert.AreEqual("cat: TestCatalog\\directory: Is a directory", cat.GetErrorMessage());

            cat = new CATCommand(args[3], session);
            cat.Execute();
            Assert.AreEqual("cat: TestCatalog\\notRealfile.txt: No such file or directory", cat.GetErrorMessage());

            cat = new CATCommand(args[4], session);
            cat.SetStdIn("some stdin");
            cat.Execute();
            Assert.AreEqual("some stdin", cat.GetStdOut());

            File.Delete(current + @"\TestCatalog\file.txt");
            File.Delete(current + @"\TestCatalog\file1.txt");
            File.Delete(current + @"\TestCatalog\file2.txt");
            Directory.Delete(current + @"\TestCatalog\directory");
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
                "exit",
            };
            string[] expected = new string[]
            {
                "",
                "",
                "",
                "cd: too many arguments",
                "",
                @"C:\Gits\spsu-mm-se-programming\Suldin_Vyacheslav\SecondTerm\Task8Folder\Task8.UnitTests\bin\Debug\net5.0\TestCatalog\NewDirectory",
                "",
                "",
                "",
                "file1.txt file2.txt NewFileStringaddString ",
                "1 4 20",
                "1 5 43",
                "rm: cannot remove 'file3.txt': No such file or directory",
                "rmdir: failed to remove 'NewDirectory': No such file or directory",
                "",
                "exit",
            };
            Session session = new Session();
            var testReader = new TestReader(commands);
            session.Reader = testReader;
            session.Start();
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], testReader.Commands[i]);
            }

            session.SetLocalVar("a", "newValue");

            Assert.AreEqual("newValue", session.GetLocalVar("$a"));
        }

    }

}