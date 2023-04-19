using DBConnect.Model;
using System;
using System.Net.Http.Headers;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DBConnect.DataAccess dataAccess = new DBConnect.DataAccess();
            Table table = new Table(
                "customer",
                new Column("id", typeof(int), Constraints.PRIMARY_KEY),
                new Column("name", typeof(string)),
                new Column("budget", typeof(float)),
                new Column("birth", typeof(DateTime))
                );

            Database db = new("db_test");
            dataAccess.GetAll(table, db);

            Console.WriteLine(table[1]["name"]);

        }
    }
}