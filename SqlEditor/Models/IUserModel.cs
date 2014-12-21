namespace SqlEditor.Models
{
    using System.Configuration;

    /// <summary>
    /// The user model.
    /// </summary>
    public interface IUserModel
    {
        /// <summary>
        /// Logs a user into a database.
        /// </summary>
        /// <param name="system">The database system name in the connection settings.</param>
        /// <param name="username">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>Whether the login was successful.</returns>
        bool Login(string system, string username, string password);

        /// <summary>
        /// Determines whether a database requires a logon username or password.
        /// </summary>
        /// <param name="system">The database system.</param>
        /// <returns>Whether a login is required.</returns>
        bool IsLoginRequired(string system);

        /// <summary>
        /// Gets a user specific connection string to a database.
        /// </summary>
        /// <param name="system">The database system.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The connection string for the database and user.</returns>
        ConnectionStringSettings GetUserConnectionString(string system, string username, string password);
    }
}