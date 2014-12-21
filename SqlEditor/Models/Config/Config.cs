namespace SqlEditor.Models.Config
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Site configuration settings.
    /// </summary>
    public class Config : IConfig
    {     
        /// <inheritdoc/>
        public virtual string BaseDirectory
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.BaseDirectory))
                {
                    return Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "SqlFiles");
                }

                return Properties.Settings.Default.BaseDirectory;
            }
        }

        /// <inheritdoc/>
        public virtual string[] AllowableExtensions
        {
            get
            {
                return Properties.Settings.Default.AllowableExtensions.Split(',');
            }
        }

        /// <inheritdoc/>
        public virtual ConnectionStringSettings GetConnectionSettings(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];
        }

        /// <inheritdoc/>
        public virtual IEnumerable<string> GetConnectionNames()
        {
            var list = new List<string>();
            for (var i = 0; i < ConfigurationManager.ConnectionStrings.Count; i++)
            {
                list.Add(ConfigurationManager.ConnectionStrings[i].Name);
            }

            return list;
        }
        
        /// <inheritdoc />
        public IEnumerable<string> GetDrivers()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DriversList))
            {
                return new List<string>();
            }

            return Properties.Settings.Default.DriversList.Split(',').ToList();
        }
    }
}