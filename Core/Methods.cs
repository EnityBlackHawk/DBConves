using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Methods
    {
        public static event EventHandler<ActionCreatedEventArgs>? ActionCreated;
        
        public static void OnActionCreated(object sender, ActionCreatedEventArgs e)
        {
            ActionCreated?.Invoke(sender, e);
        }

    }
}
