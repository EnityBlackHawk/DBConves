using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public abstract class ActionFactory
    {
        public Tools.Matrix3<string, Type, object> Properties { get; protected set; } = new();
        public string Name { get; protected set; }

        public abstract Action CreateCoreAction();
        public virtual void AddValue(string prop, object value)
        {
            var tuple = Properties.GetByCol1(prop);
            if (tuple.Value2 != value.GetType())
                throw new Exception("Invalid value");
            Properties.SetCol3ByCol1(prop, value);
        }

    }
}
