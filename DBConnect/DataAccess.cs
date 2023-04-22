﻿using Dapper;
using DBTelegraph.Annotations;
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
        public List<T> GetAll<T>(T entity) where T : ModelBase
        {
            using (IDbConnection conn = new SqlConnection(ConfigClass.ConnectionString))
            {
                return conn.Query<T>($"SELECT * FROM {entity.tableName()}").ToList();
            }
        }

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

        public void Insert<T>(T entity) where T : ModelBase
        {
            using (IDbConnection conn = new SqlConnection(ConfigClass.ConnectionString))
            {
                string e = entity.ToString();
                conn.Query($"INSERT INTO {entity.tableName()}{entity.GetColumnsName()} VALUES {e}");
            }
        }
        public void Insert<T>(T entity, string dataBaseName) where T : ModelBase
        {
            using (IDbConnection conn = new SqlConnection(ConfigClass.GetConnectionStringForDataBase(dataBaseName)))
            {
                string e = entity.ToString();
                conn.Query($"INSERT INTO {entity.tableName()}{entity.GetColumnsName()} VALUES {e}");
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

        public void CreateTable(Type type)
        {
            using (IDbConnection conn = new SqlConnection(ConfigClass.ConnectionString))
            {
                StringBuilder sb = new StringBuilder($"Create table {type.Name} (\n");
                var props = type.GetProperties();
                List<string> pks = new List<string>();
                foreach (var prop in props)
                {
                    if (prop.GetCustomAttribute(typeof(IdAttribute)) != null)
                    {
                        pks.Add(prop.Name);
                    }

                    sb.AppendLine($"\t {prop.Name} {TypeTable.getSQLType(prop.PropertyType)},");
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

        //public void CreateTable(Table table)
        //{
        //    using (IDbConnection conn = new SqlConnection(ConfigClass.ConnectionString))
        //    {
        //        StringBuilder sb = new StringBuilder($"create table {table.Name} (\n");
        //        List<string> pks = new List<string>();
        //        foreach (var c in table.Columns)
        //        {
        //            if (c.Constraints.Contains(Constraints.PRIMARY_KEY))
        //                pks.Add(c.Name);

        //            sb.AppendLine($"\t {c.Name} {TypeTable.getSQLType(c)},");
        //        }
        //        sb.Append("\t PRIMARY KEY(");
        //        for (int i = 0; i < pks.Count; i++)
        //        {
        //            sb.Append(pks[i]);
        //            if (i < pks.Count - 1)
        //                sb.Append(", ");
        //        }
        //        sb.Append("));");
        //        conn.Query(sb.ToString());
        //    }
        //}

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
