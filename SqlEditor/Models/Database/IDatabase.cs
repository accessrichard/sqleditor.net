namespace SqlEditor.Models.Database
{
    using System.Configuration;
    using System.Data;

    /// <summary>
    /// Manages database connections.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Gets a database connection.
        /// </summary>
        /// <returns>The database connection.</returns>
        IDbConnection GetConnection();

        /// <summary>
        /// Tries to connect to a database.
        /// </summary>
        /// <param name="username"> The database username. </param>
        /// <param name="password"> The database password. </param>
        /// <returns>
        /// Whether the connection was successful.
        /// </returns>
        bool TryConnect(string username, string password);

        /// <summary>
        /// Gets a value indicating whether the database connection string
        /// requires a username or password.
        /// </summary>
        /// <returns>A value indicating whether a username or password is required for a database.</returns>
        bool IsLoginRequired();

        /// <summary>
        /// The get user connection string.
        /// </summary>
        /// <param name="username"> The database username. </param>
        /// <param name="password"> The database password. </param>
        /// <returns> The connection settings. </returns>
        ConnectionStringSettings GetUserConnectionString(string username, string password);
    }
}