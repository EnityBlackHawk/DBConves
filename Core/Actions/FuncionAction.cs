﻿using Core.Attributes;
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

        public FunctionAction()
        {
        }

        protected override void OnRun()
        {
            System.Diagnostics.Debug.WriteLine(Message!);
        }
    }
}
