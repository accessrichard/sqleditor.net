namespace SqlEditor.Models
{
    using System.Configuration;

    using SqlEditor.Models.Dojo.Dgrid;

    /// <summary>
    /// A dojo "dGrid" model.
    /// </summary>
    public interface IDGridModel
    {
        /// <summary>
        /// Queries a database an returns results as a list of dictionaries 
        /// container column name and column values.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="settings">The connection string settings.</param>
        /// <returns>The result set of a query.</returns>
        QueryResult Query(string sql, ConnectionStringSettings settings);
    }
}