using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserBook : DbItem
    {
        public string username { get; set; }
        public int price { get; set; }
        public string bookname { get; set; }
        public string condition { get; set; }
    }
}
