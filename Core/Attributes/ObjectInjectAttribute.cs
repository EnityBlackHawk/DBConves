using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ObjectInjectAttribute : Attribute
    {
        public List<Type> TargetType { get; set; }
        public ObjectInjectAttribute(params Type[] type)
        {
            TargetType = new List<Type>(type);
        }
    }
}
