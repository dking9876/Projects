using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class Book : DbItem
    {
        public string name { get; set; }
        public string writer { get; set; }
        public string subject { get; set; }
        public int classNum { get; set; }
    }
}
