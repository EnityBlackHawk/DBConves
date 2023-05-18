using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.ActionsFactories
{
    public class AutoActionFactory : ActionFactory
    {
        private Type _coreActionType;

        public AutoActionFactory(Type actionType)
        {

            if (actionType.BaseType != typeof(Model.Action))
                throw new ArgumentException("Is not a valid action type");

            _coreActionType = actionType;
            Name = actionType.Name;

            foreach (var p in actionType.GetProperties())
            {
                if (p.GetCustomAttribute(typeof(Attributes.UserOptionAttribute), true) != null)
                {
                    base.Properties.Add(p.Name, p.PropertyType, null);
                }
            }
        }

        public override Model.Action CreateCoreAction()
        {
            Model.Action? obj = (Model.Action?)Activator.CreateInstance(_coreActionType) ?? throw new Exception("Error on creating action");

            foreach (var p in base.Properties)
            {
                var info = _coreActionType.GetProperty(p.Value1);
                info?.SetValue(obj!, p.Value3);
            }
            obj.OnActionCreation();
            return obj;
        }
    }
}
