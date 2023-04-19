using Dapper;
using DBConnect.Model;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using static Dapper.SqlMapper;

namespace DBConnect
{
    public class DataAccess
    {
        public List<T> GetAll<T>(T entity) where T : Model.ModelBase
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString))
            {
                return conn.Query<T>($"SELECT * FROM {entity.tableName()}").ToList();
            }
        }

        public void GetAll(Table table, Database database)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.GetConnectionStringForDataBase(database)))
            {
                var c = conn.Query($"SELECT * FROM {table.Name}");
                Console.WriteLine("Done query");
                foreach(var r in c)
                {
                    var d = (IDictionary<string, object>)r;
                    table.AddRegister(d);
                }
            }
        }

        public void Insert<T>(T entity) where T : Model.ModelBase
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString))
            {
                string e = entity.ToString();
                conn.Query($"INSERT INTO {entity.tableName()}{entity.GetColumnsName()} VALUES {e}");
            }
        }
        public void Insert<T>(T entity, string dataBaseName) where T : Model.ModelBase
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.GetConnectionStringForDataBase(dataBaseName)))
            {
                string e = entity.ToString();
                conn.Query($"INSERT INTO {entity.tableName()}{entity.GetColumnsName()} VALUES {e}");
            }
        }


        public void CreateTable(Type type)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString))
            {
                StringBuilder sb = new StringBuilder($"Create table {type.Name} (\n");
                var props = type.GetProperties();
                List<String> pks = new List<String>();
                foreach ( var prop in props )
                {
                    if (prop.GetCustomAttribute(typeof(Annotations.IdAttribute)) != null)
                    {
                        pks.Add(prop.Name);
                    }
                    
                    sb.AppendLine($"\t {prop.Name} {TypeTable.getSQLType(prop.PropertyType)},");
                }
                sb.Append("\t PRIMARY KEY(");
                for(int i = 0; i < pks.Count; i++)
                {
                    sb.Append(pks[i]);
                    if(i < pks.Count - 1)
                        sb.Append(", ");
                }
                sb.Append("));");
                conn.Query(sb.ToString());
            }
        }

        public void CreateTable(Model.Table table)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString))
            {
                StringBuilder sb = new StringBuilder($"Create table {table.Name} (\n");
                List<String> pks = new List<String>();
                foreach (var c in table.Columns)
                {
                    if (c.Constraints.Contains(Model.Constraints.PRIMARY_KEY))
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
            }
        }

        public void CreateTable(Model.Table table, Database dataBase)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.GetConnectionStringForDataBase(dataBase)))
            {
                StringBuilder sb = new StringBuilder($"Create table {table.Name} (\n");
                List<String> pks = new List<String>();
                foreach (var c in table.Columns)
                {
                    if (c.Constraints.Contains(Model.Constraints.PRIMARY_KEY))
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

                dataBase.Tables.Add(table);
            }
        }

        public void CreateDatabase(Database database)
        {
            using (IDbConnection conn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString))
            {
                conn.Query("CREATE DATABASE " + database);
            }
        }

        public void DropDatabase(Database database)
        {
            using IDbConnection cnn = new System.Data.SqlClient.SqlConnection(ConfigClass.ConnectionString);
            try
            {
                cnn.Query("DROP DATABASE " + database);
            }catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
