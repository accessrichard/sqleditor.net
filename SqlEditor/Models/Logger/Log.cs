namespace SqlEditor.Models.Logger
{
    using log4net;

    /// <summary>
    /// The error logger.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// An instance of the log 4 net logger.
        /// </summary>
        private static readonly ILog Log4Net = LogManager.GetLogger(typeof(MvcApplication));

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(string message)
        {
            Log4Net.Debug(message);
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">The message</param>
        public static void Error(string message)
        {
            Log4Net.Error(message);
        }
    }
}