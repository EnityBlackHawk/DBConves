using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBTelegraph.Model
{
    public abstract class ModelBase
    {
        public abstract string tableName();

        public override string ToString()
        {
            var props = GetType().GetProperties();
            var sb = new StringBuilder();
            sb.Append("( ");
            foreach (var p in props)
            {
                sb.Append(p.GetValue(this));
                if (p != props[props.Length - 1])
                    sb.Append(", ");
            }
            sb.Append(" )");
            return sb.ToString();
        }

        public virtual string GetColumnsName()
        {
            var props = GetType().GetProperties();
            var sb = new StringBuilder();
            sb.Append("( ");
            foreach (var p in props)
            {
                if (p.Name == "id")
                    continue;
                sb.Append(p.Name);
                if (p != props[props.Length - 1])
                    sb.Append(", ");
            }
            sb.Append(" )");
            return sb.ToString();
        }
    }
}
