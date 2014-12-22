namespace SqlEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;

    using SqlEditor.Models.Database;
    using SqlEditor.Models.Dojo.Dgrid;

    using StructureMap;

    /// <inheritdoc />
    public class DGridModel : IDGridModel
    {
        /// <summary>
        /// The IOC container.
        /// </summary>
        private readonly IContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DGridModel"/> class. 
        /// </summary>
        /// <param name="container">The IOC container.</param>
        public DGridModel(IContainer container)
        {
            this.container = container;
        }

        /// <inheritdoc />
        public QueryResult Query(string sql, ConnectionStringSettings settings)
        {
            var database = this.container.With("settings").EqualTo(settings).GetInstance<IDatabase>();
            using (var connection = database.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    connection.Open();
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    using (var reader = command.ExecuteReader())
                    {
                        var results = FetchAll(reader).ToList();
                        stopWatch.Stop();
                        var time = stopWatch.Elapsed;
                        var queryResult = new QueryResult
                                               {
                                                   Data = results,
                                                   ElapsedTime = time,
                                                   RowCount = results.Count,
                                                   Columns = GetColumns(results)
                                               };
                        this.AddIdColumn(queryResult);
                        return queryResult;
                    }
                }
            }
        }

        /// <summary>
        /// Fetches all records in the reader.
        /// </summary>
        /// <param name="reader">The data reader.</param>
        /// <returns>A list where each item is a dictionary of column field and value.</returns>
        private static IEnumerable<Dictionary<string, dynamic>> FetchAll(IDataReader reader)
        {
            var list = new List<Dictionary<string, dynamic>>();
            while (reader.Read())
            {
                list.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
            }

            return list;
        }

        /// <summary>
        /// Returns a list of column names.
        /// </summary>
        /// <param name="queryResult">The result of a query.</param>
        /// <returns>The columns names in the query.</returns>
        private static IEnumerable<Column> GetColumns(IEnumerable<Dictionary<string, dynamic>> queryResult)
        {
            var result = queryResult.FirstOrDefault();
            if (result == null)
            {
                return new List<Column>();
            }

            return
                result.Select(
                    column =>
                    new Column
                        {
                            Formatter = column.Value is DateTime ? "dotNetDate" : string.Empty,
                            Field = column.Key
                        });
        }

        /// <summary>
        /// Adds an "id" column to the result set.
        /// This is needed by the dojo grid.
        /// </summary>
        /// <param name="result">The query result.</param>
        private void AddIdColumn(QueryResult result)
        {
            const string IdField = "_id";
            result.IdField = IdField;
            var list = result.Data.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                list[i].Add(IdField, i);
            }

            result.Data = list;

            var columns = result.Columns.ToList();
            columns.Add(new Column
                            {
                                Field = IdField,
                                Label = "ID()",
                                Hidden = true
                            });

            result.Columns = columns;
        }
    }
}
