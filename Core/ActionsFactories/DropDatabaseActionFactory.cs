using Core.Model;
using DBTelegraph;
using DBTelegraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.ActionsFactories
{
    public class DropDatabaseActionFactory : ActionFactory
    {
        public Database Database { get; set; }

        public DropDatabaseActionFactory()
        {
            Properties.Add(nameof(Database), typeof(Database), null);
        }

        public override Core.Model.Action CreateCoreAction()
        {
            var ca = new Core.Actions.DropDatabaseAction();
            return ca;
        }
    }
}
