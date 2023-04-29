using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class UserBook
    {
        public UserUpdateModel user { get; set; }
        public int price { get; set; }
        public Book book { get; set; }
        public string condition { get; set; }
    }
    public class UserBookSearchParams
    {
        public Book book { get; set; }
        public int price { get; set; }
        public string condition { get; set; }
    }
}
