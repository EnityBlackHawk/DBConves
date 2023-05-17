using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(Type t) : base($"No object in results matches if the required type for injection: {t.Name}")
        {}
    }
}
