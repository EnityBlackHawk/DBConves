using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Attributes;
using DBTelegraph;
using DBTelegraph.Model;

namespace Core.Actions
{
    [CoreAction]
    public class CreateDatabaseAction : Model.Action
    {
        public Database Database { get; }
        public ConfigClass? ConfigClass { get; private set; }

        public CreateDatabaseAction(Database database)
        {
            Database = database;
            ResultType = typeof(Database);   
        }

        public override void Settup([ObjectInject(typeof(ConfigClass))]params object[] args)
        {
            ConfigClass = (args[0] as ConfigClass);
        }

        protected override void OnRun()
        {
            DataAccess acc = new DataAccess(ConfigClass!);
            var result = acc.CreateDatabaseAndTables(Database);
            Status = result.Status;
            StatusReport = result.StatusResult;
            Result = Database;
        }
    }
}
