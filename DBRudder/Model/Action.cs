using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace DBRudder.Model
{
    public class Action
    {
        public string Name { get; }
        public Core.Model.Action CoreObject { get; }

        public Action(string name, Core.Model.Action coreObject)
        {
            Name = name;
            CoreObject = coreObject;
        }
    }
}
