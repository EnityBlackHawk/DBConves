using Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Model
{
    public class RegisteredActions
    {
        public List<Type> Actions { get; set; }


        public RegisteredActions()
        {
            Actions = new List<Type>(GetDefinedCoreActions(Assembly.LoadFrom("Core.dll")));

        }

        public Type[] GetDefinedCoreActions(Assembly assembly)
        {
            var types = assembly.GetTypes().Where((t) =>
            {
                return t.GetCustomAttribute(typeof(Core.Attributes.CoreActionAttribute)) != null ? true : false;
            }).ToArray();
            return types;
        }

        public List<String> GetActionNames()
        {
            List<String> names = new List<String>();
            foreach (var t in Actions)
            {
                names.Add(t.Name);
            }
            return names;
        }

        public Action CreateActionInstance(string actionName)
        {
            Type target = null;
            foreach (var t in Actions)
            {
                if (t.Name == actionName)
                    target = t;
            }

            return (Action)Activator.CreateInstance(target);

        }
        
    }
}
