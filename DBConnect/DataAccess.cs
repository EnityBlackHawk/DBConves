using Dapper;
using DBTelegraph.Model;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using static Dapper.SqlMapper;

namespace DBTelegraph
{
    public readonly struct Result
    {
        /// <summary>
        /// Message
        /// </summary>
        public string Status { get; }
        /// <summary>
        /// Status (Error, Success)
        /// </summary>
        public string StatusResult { get; }
        public Result(string status, string statusResult)
        {
            Status = status;
            StatusResult = statusResult;
        }
    }


    public class DataAccess
    {
        internal struct FK
        {
            public string? this_prop;
            public string? target_table;
            public string? target_prop;

            public FK(string? this_prop, string? target_table, string? target_prop)
            {
                this.this_prop = this_prop;
                this.target_table = target_table;
                this.target_prop = target_prop;
            }

            public static implicit operator string(FK fK) => 
                $"FOREIGN KEY ({fK.this_prop}) REFERENCES {fK.target_table}({fK.target_prop})";
            
        }

        private ConfigClass _config;

        public DataAccess(ConfigClass config)
        {
            _config = config;
        }


        public void GetAll(Table table, Database database)
        {
            using (IDbConnection conn = new SqlConnection(_config.GetConnectionStringForDataBase(database)))
            {
                var c = conn.Query($"SELECT * FROM {table.Name}");
                Console.WriteLine("Done query");
                foreach (var r in c)
                {
                    var d = (IDictionary<string, object>)r;
                    table.AddRegister(d);
                }
            }
        }

        public void Insert(Table table, Database database, List<Register> registers)
        {
            var columns = table.GetColumnsName().ToList();
            foreach (var reg in registers)
            {
                var except = reg.ColumnsName().Except(columns);
                if (except.Count() > 0)
                    throw new Exception($"Register column does not match with the table's columns: {except.ElementAt(0)}");
            }

            using IDbConnection conn = new SqlConnection(_config.GetConnectionStringForDataBase(database));

            string sql = $"INSERT INTO {table.Name}{table.GetColumnsNameToString()} VALUES ";
            foreach(var r in registers)
            {
                sql += r.ToString();
                sql += r != registers.Last() ? ",\n" : "\n";

            }
            try
            {   
                conn.Query(sql);
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public void CreateTable(Table table, Database dataBase)
        {
            using (IDbConnection conn = new SqlConnection(_config.GetConnectionStringForDataBase(dataBase)))
            {
                StringBuilder sb = new StringBuilder($"Create table {table.Name} (\n");
                List<string> pks = new List<string>();
                List<FK> fks = new List<FK>();
                foreach (var c in table.Columns)
                {
                    if (c.Constraints.Contains(Constraints.PRIMARY_KEY))
                        pks.Add(c.Name);

                    if (c.Constraints.Contains(Constraints.FOREIGN_KEY))
                        fks.Add(new FK(c.Name, c.ReferencesTable!, c.ReferecesColumn!));

                    sb.AppendLine($"\t {c.Name} {TypeTable.getSQLType(c)},");
                }
                sb.Append("\t PRIMARY KEY(");
                for (int i = 0; i < pks.Count; i++)
                {
                    sb.Append(pks[i]);
                    if (i < pks.Count - 1)
                        sb.Append(", ");
                }
                sb.AppendLine(")" + (fks.Count != 0 ? "," : ""));

                foreach(var f in fks)
                {
                    sb.Append(f);
                    if (f != fks.Last())
                        sb.AppendLine(",");
                    else
                        sb.AppendLine();
                }
                sb.Append(");");
                conn.Query(sb.ToString());
            }
        }
        public void CreateDatabase(Database database)
        {
            using (SqlConnection conn = new SqlConnection(_config.ConnectionString))
            {

                //conn.Query("CREATE DATABASE " + database);
                SqlCommand command = new SqlCommand($"CREATE DATABASE {database}", conn);
                //SqlParameter param = new();
                //param.ParameterName = "@database";
                //param.Value = (string)database;

                //command.Parameters.Add(param);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }


        public Task<Result> CreateDatabaseAndTablesAsync(Database dataBase)
        {
            return Task.Run(() =>
            {
                try
                {
                    CreateDatabase(dataBase);
                    foreach (var table in dataBase.Tables)
                    {
                        CreateTable(table, dataBase);
                    }
                    return new Result("Done", "Success");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    return new Result(ex.Message, "Error");
                }
                
            });
        }

        public Result CreateDatabaseAndTables(Database dataBase)
        {
            try
            {
                CreateDatabase(dataBase);
                foreach (var table in dataBase.Tables)
                {
                    CreateTable(table, dataBase);
                }
                return new Result("Done", "Success");
            }
            catch (Exception ex)
            {
                return new Result(ex.Message, "Error");
            }
        }

        public Result DropDatabase(Database database)
        {
            using IDbConnection cnn = new SqlConnection(_config.ConnectionString);
            
            if(_config.SGBD == SGBD.SQL_SERVER)
                cnn.Query($"alter database {database} set single_user with rollback immediate");

            try
            {
                cnn.Query("DROP DATABASE " + database);
            }
            catch (SqlException ex)
            {
                return new Result(ex.Message, "Error");
            }
            return new Result("Done", "Success");
        }

        public Result DropDatabase(string database)
        {
            using IDbConnection cnn = new SqlConnection(_config.ConnectionString);

            if (_config.SGBD == SGBD.SQL_SERVER)
                cnn.Query($"alter database {database} set single_user with rollback immediate");

            try
            {
                cnn.Query("DROP DATABASE " + database);
            }
            catch (SqlException ex)
            {
                return new Result(ex.Message, "Error");
            }
            return new Result("Done", "Success");
        }
    }
}
