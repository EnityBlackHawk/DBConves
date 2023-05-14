using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Model
{
    public abstract class ActionFactory
    {
        public Tools.Matrix3<string, Type, object?> Properties { get; protected set; } = new();
        public string Name { get; protected set; }
        
        public abstract Core.Model.Action CreateCoreAction();
        
    }
}
