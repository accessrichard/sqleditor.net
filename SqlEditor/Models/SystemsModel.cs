namespace SqlEditor.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using SqlEditor.Models.Config;
    using SqlEditor.Models.Dojo.Form;

    /// <inheritdoc />
    public class SystemsModel : ISystemsModel
    {
        /// <summary>
        /// The site configuration.
        /// </summary>
        private readonly IConfig config;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemsModel"/> class. 
        /// </summary>
        /// <param name="config">The site configuration.</param>
        public SystemsModel(IConfig config)
        {
            this.config = config;
        }

        /// <inheritdoc />    
        public IEnumerable<Select> GetSystems()
        {
            return from system in this.config.GetConnectionNames() select new Select(system, system);
        }
    }
}