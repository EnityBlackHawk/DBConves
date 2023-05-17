using Microsoft.UI.Xaml.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Model
{
    public class ActionViewManager
    {
        private Dictionary<Type, IActionView> _values = new Dictionary<Type, IActionView>();
        public void Add(IActionView actionView)
        {
            _values.Add(actionView.CoreActionType, actionView);
        }

        public ActionViewManager()
        {
        }

        private Type[] GetDefinedActionViews(Assembly assembly)
        {
            var views = assembly.GetTypes().Where((t) =>
            {
                return t.GetInterface("IActionView") != null ? true : false;
            });
            return views.ToArray();
        }

        public bool Get(Type actionType, out IActionView actionView) => _values.TryGetValue(actionType, out actionView);
        public bool Get<T>(out IActionView actionView) => _values.TryGetValue(typeof(T), out actionView);

    }
}
