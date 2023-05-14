using DBRudder.Model;
using DBTelegraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DBRudder.ActionsFactories
{
    public class DropDatabaseActionFactory : ActionFactory
    {
        public ConfigClass ConfigClass { get; set; }
        public Database Database { get; set; }

        public DropDatabaseActionFactory(ConfigClass configClass)
        {
            Properties.Add(nameof(Database), typeof(Database), null);
            this.ConfigClass = configClass;
        }

        public override Core.Model.Action CreateCoreAction()
        {
            var ca = new Core.Actions.DropDatabaseAction();
            return ca;
        }
    }
}
