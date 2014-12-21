namespace SqlEditor.Models.Config
{
    using System.Collections.Generic;
    using System.Configuration;

    /// <summary>
    /// Site configuration settings.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Gets the base directory where SQL files are stored.
        /// </summary>
        string BaseDirectory { get; }

        /// <summary>
        /// Gets the allowable extensions for files in the file system tree.
        /// </summary>
        string[] AllowableExtensions { get; }

        /// <summary>
        /// Gets the connection string settings for a connection name.
        /// </summary>
        /// <param name="name">The name of the connection string.</param>
        /// <returns>The connection string settings.</returns>
        ConnectionStringSettings GetConnectionSettings(string name);

        /// <summary>
        /// Gets the names of each database connection settings.
        /// </summary>
        /// <returns>A list of database system names for connection settings.</returns>
        IEnumerable<string> GetConnectionNames();

        /// <summary>
        /// Gets the database driver location.
        /// </summary>
        /// <returns>
        /// A path to the drivers location.
        /// </returns>
        IEnumerable<string> GetDrivers();
    }
}