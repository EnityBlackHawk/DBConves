using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBTelegraph;
using DBTelegraph.Model;

namespace Core.Actions
{
    public class CreateDatabaseAction : Model.Action
    {
        public Database Database { get; }
        public ConfigClass ConfigClass { get; }

        public CreateDatabaseAction(Database database, ConfigClass configClass)
        {
            Database = database;
            ConfigClass = configClass;
            
        }

        protected override void OnRun()
        {
            DataAccess acc = new DataAccess(ConfigClass);
            var result = acc.CreateDatabaseAndTables(Database);
            Status = result.Status;
            StatusReport = result.StatusResult;
            Thread.Sleep(5000);
            Console.WriteLine("Task 1");
        }
    }
}
