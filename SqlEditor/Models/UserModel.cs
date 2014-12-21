namespace SqlEditor.Models
{
    using System.Configuration;

    using SqlEditor.Models.Config;
    using SqlEditor.Models.Database;

    using StructureMap;

    /// <summary>
    /// The user model.
    /// </summary>
    public class UserModel : IUserModel
    {
        /// <summary>
        /// The site configuration.
        /// </summary>
        private readonly IConfig config;

        /// <summary>
        /// The structure map container.
        /// </summary>
        private readonly IContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class. 
        /// </summary>
        /// <param name="config">The site configuration.</param>
        /// <param name="container">The IOC container.</param>
        public UserModel(IConfig config, IContainer container)
        {
            this.config = config;
            this.container = container;
        }

        /// <inheritdoc />
        public bool Login(string system, string username, string password)
        {
            var database = this.GetDatabase(system);
            return !database.IsLoginRequired() || database.TryConnect(username, password);
        }

        /// <inheritdoc />
        public bool IsLoginRequired(string system)
        {
            return this.GetDatabase(system).IsLoginRequired();
        }

        /// <inheritdoc />
        public ConnectionStringSettings GetUserConnectionString(string system, string username, string password)
        {
            return this.GetDatabase(system).GetUserConnectionString(username, password);
        }

        /// <summary>
        /// Gets the database for a system.
        /// </summary>
        /// <param name="system">The database system name.</param>
        /// <returns>The database.</returns>
        private IDatabase GetDatabase(string system)
        {
            var settings = this.config.GetConnectionSettings(system);
            return this.container.With("settings").EqualTo(settings).GetInstance<IDatabase>();        
        }
    }
}