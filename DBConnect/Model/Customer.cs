using DBConnect.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Model
{
    public class Customer : ModelBase
    {
        [Id]
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }

        public float Budget { get; set; }


        public override string tableName() => "tb_customers";

        public override string ToString() => $"( '{Name}', '{Birth.Year}-{Birth.Month}-{Birth.Day}', {Budget})";
        
    }
}
