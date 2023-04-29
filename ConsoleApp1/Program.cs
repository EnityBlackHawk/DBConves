#define SQL_SERVER

using Core.Actions;
using Core.Model;
using DBTelegraph;
using DBTelegraph.Model;
using System;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConfigClass configClass = new ConfigClass("Server=BLACKHAWKPC\\SQLSERVER;Trusted_Connection=True;", SGBD.SQL_SERVER);
            DataAccess dataAccess = new DBTelegraph.DataAccess(configClass);
            Table table = new Table(
                "table1",
                new Column("id", typeof(int), Constraints.PRIMARY_KEY),
                new Column("name", typeof(string)),
                new Column("budget", typeof(float)),
                new Column("birth", typeof(DateTime))
                );

            Database db = new("db_test");
            Database db2 = new("db_test2");

            dynamic r1 = new Register();
            r1["id"] = 4;
            r1["name"] = "Teste";
            r1["budget"] = 500.00;
            r1["birth"] = DateTime.Now;

            dynamic r2 = new Register();
            r2["id"] = 5;
            r2["name"] = "Teste2";
            r2["budget"] = 500.00;
            r2["birth"] = DateTime.Now;

            //dataAccess.DropDatabase(db);
            //dataAccess.CreateDatabase(db);
            //dataAccess.CreateTable(table, db);
            //dataAccess.Insert(table, db, new List<Register> { r1, r2 });

            CreateDatabaseAction create = new CreateDatabaseAction(db, configClass);
            FunctionAction function = new();
            Workflow workflow = new Workflow("teste", create);
            Workflow workflow2 = new Workflow("teste2", function);

            _ = workflow.StartAsync();
            _ = workflow2.StartAsync();

            await Task.Delay(int.MaxValue);
            Console.WriteLine(workflow[0].Status);
            //dataAccess.DropDatabase(db);


        }
    }
}