using Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Actions
{
    [CoreAction]
    public class FunctionAction : Model.Action
    {
        [UserOption]
        public string? Message { get; set; }

        public override string Name => "Fuction Action";

        public FunctionAction()
        {
        }

        protected override void OnRun()
        {
            System.Diagnostics.Debug.WriteLine(Message!);
            Status = new Model.Result("Done", "Success");
        }
    }
}
