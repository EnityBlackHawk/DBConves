using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CoreActionAttribute : Attribute
    {
        public CoreActionAttribute()
        {
            
        }
    }
}
