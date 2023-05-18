using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class ActionCreatedEventArgs
    {
        public Action Action { get; set; }


        public ActionCreatedEventArgs(Action action)
        {
            this.Action = action;
        }
    }
}
