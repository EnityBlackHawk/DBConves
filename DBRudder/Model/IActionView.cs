using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRudder.Model
{
    public interface IActionView
    {
        public Type CoreActionType { get; }
        public Type ViewModelType { get; }
        public Object ViewModelObject { get; }
    }
}
