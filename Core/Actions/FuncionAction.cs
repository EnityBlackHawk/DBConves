using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actions
{
    public class FunctionAction : Model.Action
    {
        protected override void OnRun()
        {
            Console.WriteLine("Task 2 ok");
        }
    }
}
