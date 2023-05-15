using Core.Attributes;
using DBTelegraph;
using DBTelegraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actions
{
    [CoreAction]
    public class DropDatabaseAction : Model.Action
    {

        public Database? Database { get; private set; }
        public ConfigClass? ConfigClass { get; private set; }

        public DropDatabaseAction()
        {
        }

        protected override void OnRun()
        {
            DataAccess dataAccess = new(ConfigClass!);
            dataAccess.DropDatabase(Database!);
        }

        public override void Settup([ObjectInject(typeof(Database), typeof(ConfigClass))]params object[] args)
        {
            Database = args[0] as Database;
            ConfigClass = args[1] as ConfigClass;
        }
    }
}
