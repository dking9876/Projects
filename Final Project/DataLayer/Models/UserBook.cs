using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserBook : DbItem
    {
        public User user { get; set; }
        public int price { get; set; }
        public Book book { get; set; }
        public string condition { get; set; }
    }
}
