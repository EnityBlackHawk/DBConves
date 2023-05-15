using DBRudder.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.ActionsFactories
{
    internal class AutoActionFactory : ActionFactory
    {
        private Type _coreActionType;

        public AutoActionFactory(Type actionType)
        {

            if (actionType.BaseType != typeof(Core.Model.Action))
                throw new ArgumentException("Is not a valid action type");

            _coreActionType = actionType;
            Name = actionType.Name;

            foreach(var p in actionType.GetProperties())
            {
                if(p.GetCustomAttribute(typeof(Core.Attributes.UserOptionAttribute), true) != null)
                {
                    base.Properties.Add(p.Name, p.PropertyType, null);
                }
            }
        }

        public override Core.Model.Action CreateCoreAction()
        {
            object obj = Activator.CreateInstance(_coreActionType);
            
            foreach(var p in base.Properties)
            {
                var info = _coreActionType.GetProperty(p.Value1);
                info?.SetValue(obj, p.Value3);
            }

            return (Core.Model.Action)obj;
        }
    }
}
