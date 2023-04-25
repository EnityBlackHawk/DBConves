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
    public class DataAccess
    {

        public void GetAll(Table table, Database database)
        {
            using (IDbConnection conn = new SqlConnection(ConfigClass.GetConnectionStringForDataBase(database)))
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

            using IDbConnection conn = new SqlConnection(ConfigClass.GetConnectionStringForDataBase(database));

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
            using (IDbConnection conn = new SqlConnection(ConfigClass.GetConnectionStringForDataBase(dataBase)))
            {
                StringBuilder sb = new StringBuilder($"Create table {table.Name} (\n");
                List<string> pks = new List<string>();
                foreach (var c in table.Columns)
                {
                    if (c.Constraints.Contains(Constraints.PRIMARY_KEY))
                        pks.Add(c.Name);

                    sb.AppendLine($"\t {c.Name} {TypeTable.getSQLType(c)},");
                }
                sb.Append("\t PRIMARY KEY(");
                for (int i = 0; i < pks.Count; i++)
                {
                    sb.Append(pks[i]);
                    if (i < pks.Count - 1)
                        sb.Append(", ");
                }
                sb.Append("));");
                conn.Query(sb.ToString());

                dataBase.AddTable(table);
            }
        }

        public void CreateDatabase(Database database)
        {
            using (IDbConnection conn = new SqlConnection(ConfigClass.ConnectionString))
            {
                conn.Query("CREATE DATABASE " + database);
            }
        }

        public void DropDatabase(Database database)
        {
            using IDbConnection cnn = new SqlConnection(ConfigClass.ConnectionString);
            
            if(ConfigClass.SGBD == SGBD.SQL_SERVER)
                cnn.Query($"alter database {database} set single_user with rollback immediate");

            try
            {
                cnn.Query("DROP DATABASE " + database);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
