namespace SqlEditor.Controllers
{
    using System;
    using System.Web.Mvc;

    using SqlEditor.Json;
    using SqlEditor.Models;
    using SqlEditor.Models.Cryptography;

    /// <summary>
    /// The user controller.
    /// </summary>
    public class UserController : JsonController
    {
        /// <summary>
        /// The user model.
        /// </summary>
        private readonly IUserModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class. 
        /// </summary>
        /// <param name="model">The user model.</param>
        public UserController(IUserModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Logs a user into a database system.
        /// </summary>
        /// <param name="system"> The database system. </param>
        /// <param name="username"> The database username. </param>
        /// <param name="password"> The database password. </param> 
        /// <returns> Whether a login was successful. </returns>
        [HttpPost]
        public JsonResult Login(string system, string username, string password)
        {
            if (!this.model.IsLoginRequired(system))
            {
                return this.Json(new { isSuccess = true });
            }

            try
            {                
                if (this.model.Login(system, username, password))
                {
                    var cookie = this.HttpContext.Request.Cookies.Get("sqleditor");
                    var userKey = cookie != null ? cookie.Value : string.Empty;
                    var connString = this.model.GetUserConnectionString(system, username, password).ConnectionString;
                    var cryptoConnString = Crypto.Encrypt(connString, KeyProvider.GetUserSpecificSecretKey(userKey));
                    this.Session.Add(system, cryptoConnString);
                    return this.Json(new { isSuccess = true });
                }
            }
            catch (Exception ex)
            {
                return this.Json(new { isSuccess = false, message = ex.Message });
            }

            return this.Json(new { isSuccess = false });
        }

        /// <summary>
        /// Logs a user out of a database system.
        /// This does not actually do anything aside from
        /// clear out the users database credentials from the session.
        /// </summary>
        /// <returns>An empty object.</returns>
        [HttpPost]
        public JsonResult Logout()
        {
            this.Session.Clear();
            return this.Json(new { });
        }
    }
}
