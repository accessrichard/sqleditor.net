namespace SqlEditor.Controllers
{
    using System.Configuration;
    using System.Web.Mvc;

    using SqlEditor.Json;
    using SqlEditor.Models;
    using SqlEditor.Models.Config;
    using SqlEditor.Models.Cryptography;

    /// <summary>
    /// The Query controller.
    /// </summary>
    public class QueryController : JsonController
    {
        /// <summary>
        /// The grid model for populating the JSON dojo grid.
        /// </summary>
        private readonly IDGridModel gridModel;

        /// <summary>
        /// The user model for logging into a database.
        /// </summary>
        private readonly IUserModel userModel;

        /// <summary>
        /// The site configuration.
        /// </summary>
        private readonly IConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryController"/> class. 
        /// </summary>
        /// <param name="gridModel"> The  grid model. </param>
        /// <param name="userModel"> The user model. </param>
        /// <param name="config"> The config. </param>
        public QueryController(IDGridModel gridModel, IUserModel userModel, IConfig config)
        {
            this.gridModel = gridModel;
            this.config = config;
            this.userModel = userModel;
        }

        /// <summary>
        /// Queries a database system.
        /// </summary>
        /// <param name="sql"> The SQL query. </param>
        /// <param name="system"> The provider name also known as the database system. </param>
        /// <returns> The result of the query. </returns>
        [HttpPost]
        [Route("query")]
        public JsonResult Query(string sql, string system)
        {
            if (!this.userModel.IsLoginRequired(system))
            {
                return this.Json(this.gridModel.Query(sql, this.config.GetConnectionSettings(system)));
            }

            var session = (byte[])this.Session[system];
            if (session == null)
            {
                return this.Json(new { isLoginRequired = true });
            }

            var cookie = this.Request.Cookies.Get("sqleditor");
            var userKey = cookie != null ? cookie.Value : string.Empty;
            var connString = Crypto.Decrypt(session, KeyProvider.GetUserSpecificSecretKey(userKey));
            var settings = new ConnectionStringSettings(
                system,
                connString,
                this.config.GetConnectionSettings(system).ProviderName);

            return this.Json(this.gridModel.Query(sql, settings));
        }
    }
}
