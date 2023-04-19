using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Annotations
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ReferenceAttribute : Attribute
    {
    }
}
