namespace SqlEditor.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using SqlEditor.Models.Logger;

    /// <summary>
    /// Manages dynamically loading database drivers.
    /// </summary>
    public class Drivers
    {
        /// <summary>
        /// A value indicating whether the drivers have been loaded.
        /// </summary>
        private static bool isLoaded;

        /// <summary>
        /// Loads the database drivers.
        /// </summary>
        /// <param name="drivers">
        /// The driver paths.
        /// </param>
        public static void LoadDrivers(IEnumerable<string> drivers)
        {
            if (isLoaded)
            {
                return;
            }

            isLoaded = true;
            foreach (var driver in drivers)
            {
                try
                {
                    Assembly.LoadFile(driver.Trim());
                }
                catch (Exception ex)
                {
                   Log.Error(ex.Message);
                }
            }
        }
    }
}