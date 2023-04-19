using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Models
{
    public class Customer
    {
        private int id { get;set; }
        private string name { get;set; }
        private DateTime birth { get; set; }
        private float budget { get; set; }
    }
}
