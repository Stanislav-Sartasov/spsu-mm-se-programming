using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryManager;

namespace DirectoryManagerTests
{
    [TestClass]
    public class DirectoryInfoTests
    {
        [TestMethod]
        public void FilesInDirectoryTest()
        {
            DirectoryManager.DirectoryInfo directoryInfo = new DirectoryManager.DirectoryInfo();

            Assert.IsNotNull(directoryInfo.CurrentDirectory);
            Assert.IsNotNull(directoryInfo.FullFileNamesInDirectory);
            Assert.IsNotNull(directoryInfo.FileNamesInDirectory);
        }
    }
}