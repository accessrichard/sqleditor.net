namespace SqlEditor.Models.Exceptions
{
    using System;

    /// <summary>
    /// A site specific exception for error messages
    /// that can be sent to a client.
    /// </summary>
    public class SqlEditorException : Exception
    {
        /// <inheritdoc />
        public SqlEditorException()
            // ReSharper disable once RedundantBaseConstructorCall
            : base()
        {
        }

        /// <inheritdoc />
        public SqlEditorException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public SqlEditorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}