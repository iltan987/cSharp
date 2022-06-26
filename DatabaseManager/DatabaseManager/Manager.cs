using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace DatabaseManager
{
    public class Manager : IDisposable
    {
        private SqlConnection conn;
        private bool disposedValue;

        public Manager(string fileName)
        {
            fileName = Path.GetFullPath(fileName);
            if (!File.Exists(fileName))
                CreateDatabase(fileName);
            conn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={fileName};Integrated Security=True");
        }

        private void CreateDatabase(string fileName)
        {
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=true"))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"CREATE DATABASE db ON PRIMARY (NAME=db, FILENAME='{fileName}')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"EXEC sp_detach_db 'db', 'true'";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CreateTable(string tableName, Dictionary<string, DatabaseColumnProperty> columns, string primaryKeyColumn = null, string identityColumn = null, bool keepConnectionOpen = false)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                if (IsTableExists(tableName, true))
                    throw new Exception($"Table '{tableName}' is already exists");

                if (columns == null || !columns.Any())
                    throw new Exception("At least 1 column is required.");

                string cmdText = $"CREATE TABLE {tableName}";
                List<string> strings = new List<string>();
                List<string> current;
                foreach (var item in columns)
                {
                    current = new List<string>
                    {
                        item.Key,
                        item.Value.dataType
                    };
                    if (item.Value.notNull)
                        current.Add("NOT NULL");
                    if (item.Value.unique)
                        current.Add("UNIQUE");
                    if (item.Value.foreignKey != null)
                        current.Add(item.Value.foreignKey.ToString());
                    if (item.Value.check != null)
                        current.Add(item.Value.check.ToString());
                    if (item.Value.@default != null)
                        current.Add(item.Value.@default.ToString());
                    if (primaryKeyColumn == item.Key)
                        current.Add("PRIMARY KEY");
                    if (identityColumn == item.Key)
                        current.Add("IDENTITY(1,1)");
                    strings.Add(string.Join(" ", current));
                }
                cmdText += $"({string.Join(", ", strings)})";

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public void DropTable(string tableName, bool keepConnectionOpen = false)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                if (!IsTableExists(tableName, true))
                    throw new Exception($"Table '{tableName}' is not exists");

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DROP TABLE {tableName}";
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public DataTable Select(string tableName, List<string> columns = null, string conditions = null, Dictionary<string, object> parametersOfConditions = null, bool keepConnectionOpen = false)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                if (!IsTableExists(tableName, true))
                    throw new Exception($"Table '{tableName}' is not exists");

                string cmdText = "SELECT ";

                if (columns == null || !columns.Any())
                    cmdText += "*";
                else
                {
                    string cols = string.Join(", ", columns);
                    if (!IsColumnExists(tableName, cols, true))
                        throw new Exception("All columns must be exists.");
                    else
                        cmdText += cols;
                }
                cmdText += $" FROM {tableName}{(conditions == null ? "" : " WHERE " + conditions)}";


                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    if (parametersOfConditions != null)
                        foreach (var item in parametersOfConditions)
                            cmd.Parameters.AddWithValue(item.Key, item.Value);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        sda.Fill(dt);
                }
            }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return dt;
        }

        public void Insert(string tableName, List<string> columns, Dictionary<string, object> parameterValues, bool keepConnectionOpen = false)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                if (!IsTableExists(tableName, true))
                    throw new Exception($"Table '{tableName}' is not exists");

                string cmdText = $"INSERT INTO {tableName} ";

                if (columns != null && columns.Any())
                {
                    string cols = string.Join(", ", columns);
                    if (!IsColumnExists(tableName, cols, true))
                        throw new Exception("All columns must be exists.");
                    else
                        cmdText += "(" + cols + ") ";
                }
                cmdText += $"VALUES ({string.Join(", ", parameterValues.Keys)})";

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = cmdText;
                    foreach (var item in parameterValues)
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        public void Update(string tableName, Dictionary<string,(string, object)> columName_parameterName_value, string condition, Dictionary<string, object> conditionParameters, bool keepConnectionOpen = false)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                if (!IsTableExists(tableName, true))
                    throw new Exception($"Table '{tableName}' is not exists");

                List<string> hmmm = new List<string>(), columns = new List<string>();

                foreach (var item in columName_parameterName_value)
                {
                    hmmm.Add(item.Key + "=" + item.Value.Item1);
                    columns.Add(item.Key);
                }

                if (!IsColumnExists(tableName, string.Join(", ", columns), true))
                    throw new Exception("All columns must be exists.");

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    foreach (var item in columName_parameterName_value)
                        cmd.Parameters.AddWithValue(item.Value.Item1, item.Value.Item2);
                    foreach (var item in conditionParameters)
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    cmd.CommandText = $"UPDATE {tableName} SET " + string.Join(", ", hmmm) + " WHERE " + condition;
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }
        
        public void Delete(string tableName, string condition, Dictionary<string, object> conditionParameters, bool keepConnectionOpen = false)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                if (!IsTableExists(tableName, true))
                    throw new Exception($"Table '{tableName}' is not exists");

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {tableName} WHERE {condition}";
                    foreach (var item in conditionParameters)
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
        }

        private bool IsTableExists(string tableName, bool keepConnectionOpen = false)
        {
            bool res = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT 1 FROM {tableName}";
                    cmd.ExecuteNonQuery();
                }
                res = true;
            }
            catch { }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return res;
        }

        private bool IsColumnExists(string tableName, string columns, bool keepConnectionOpen = false)
        {
            bool res = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"SELECT {columns} FROM {tableName}";
                    cmd.ExecuteNonQuery();
                }
                res = true;
            }
            catch { }
            finally
            {
                if (!keepConnectionOpen && conn.State != ConnectionState.Closed)
                    conn.Close();
            }
            return res;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                /*if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                */
                if (conn != null)
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                    conn.Dispose();
                    conn = null;
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~Manager()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}