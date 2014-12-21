namespace SqlEditor.Tests.Models
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SqlEditor.Models.FileSystem;

    [TestClass]
    public class FileFolderTreeTest
    {
        [TestMethod]
        [DeploymentItem(@".\TestData")]
        public void FileFolderTreeAddsContainsRootSqlFile()
        {
            var tree = new FileFolderTree("FileTree", new[] { ".sql" });
            var root = from item in tree.ToList() where item.Name == "Root.sql" select item;
            Assert.IsNotNull(root.FirstOrDefault());
        }
    }
}
