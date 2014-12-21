namespace SqlEditor.Tests.Models
{
    using System.Configuration;   
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SqlEditor.Models;
    using SqlEditor.Models.Config;
    using SqlEditor.Models.Database;

    using StructureMap;

    [TestClass]
    public class DatabaseTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [DeploymentItem(@".\TestDatabase\TestDb.sdf")]
        public void SqlCeQueryReturnsData()
        {
            const string Sql = "select * from person";
            var connection = ConfigurationManager.ConnectionStrings["SqlCE"];
            var container = new Container(x => x.For<IDatabase>().Use<Database>());
            var model = new DGridModel(container);
            var results = model.Query(Sql, connection);
            Assert.IsTrue(results.Data.Any());
        }

        [TestMethod]
        public void MySqlConnection()
        {
            var db = new Database(new Config(), "MySql");
            var results = db.TryConnect("root1", "password");
            Assert.IsTrue(results);
        }
    }
}
