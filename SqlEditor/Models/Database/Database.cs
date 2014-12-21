namespace SqlEditor.Models.Database
{
    using System.Configuration;
    using System.Data;
    using System.Data.Common;

    using SqlEditor.Models.Config;
    using SqlEditor.Models.Exceptions;

    using StructureMap;

    /// <inheritdoc />
    public class Database : IDatabase
    {
        /// <summary>
        /// The connection string settings for the database.
        /// </summary>
        private readonly ConnectionStringSettings settings;

        /// <summary>
        /// The site configuration.
        /// </summary>
        private readonly IConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="settings">
        /// The connection string settings settings.
        /// </param>
        /// <param name="config">
        /// The config.
        /// </param>
        [DefaultConstructor]
        public Database(ConnectionStringSettings settings, IConfig config)
        {
            this.settings = settings;
            this.config = config;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="config"> The config. </param>
        /// <param name="name"> The connection string settings name. </param>
        public Database(IConfig config, string name)
        {
            this.settings = config.GetConnectionSettings(name);
            this.config = config;
        }

        /// <inheritdoc />
        public IDbConnection GetConnection()
        {
            Drivers.LoadDrivers(this.config.GetDrivers());
            var factory = DbProviderFactories.GetFactory(this.settings.ProviderName);
            var connection = factory.CreateConnection();
            if (connection == null)
            {
                throw new SqlEditorException(
                    "Can not get iDbConnection from configuration settings. " +
                    "Please check provider name of " + this.settings.ProviderName);
            }

            connection.ConnectionString = this.settings.ConnectionString;

            return connection;
        }

        /// <inheritdoc />
        public bool TryConnect(string username, string password)
        {
            Drivers.LoadDrivers(this.config.GetDrivers());
            IDbConnection conn = null;

            try
            {
                conn = this.GetConnection();
                if (this.IsLoginRequired())
                {
                    conn.ConnectionString = this.GetUserConnectionString(username, password).ConnectionString;
                }

                conn.Open();
                return true;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        /// <inheritdoc />
        public bool IsLoginRequired()
        {
            return this.settings.ConnectionString.ToLower().Trim().Contains("{username}")
                || this.settings.ConnectionString.ToLower().Trim().Contains("{password}");
        }

        /// <inheritdoc />
        public ConnectionStringSettings GetUserConnectionString(string username, string password)
        {
            var connectionString = this.settings.ConnectionString
                .Replace("{username}", username)
                .Replace("{password}", password);
            return new ConnectionStringSettings(this.settings.Name, connectionString, this.settings.ProviderName);
        }
    }
}