namespace SqlEditor.Models.Dojo.Dgrid
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the result of a query in a friendly format
    /// format to the Dojo grid.
    /// </summary>
    public class QueryResult
    {
        /// <summary>
        /// Gets or sets a list of columns in the query result.
        /// </summary>
        public IEnumerable<Column> Columns { get; set; }

        /// <summary>
        /// Gets or sets the elapsed time of the query.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }
 
        /// <summary>
        /// Gets a message describing the query.
        /// </summary>
        public string Message 
        {
            get
            {
                return string.Format(
                    "Query executed in {0} seconds and returned {1} rows.",
                    this.ElapsedTime,
                    this.RowCount);
            } 
        }

        /// <summary>
        /// Gets or sets the number of rows in the result set.
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier column in the query results.
        /// </summary>
        public string IdField { get; set; }

        /// <summary>
        /// Gets or sets the query results.
        /// </summary>
        public IEnumerable<Dictionary<string, dynamic>> Data { get; set; }       
    }
}