using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Book
    {
        public Book() { }
        public Book(DataLayer.Models.Book DBbook) 
        {
            city = DBbook.City;
            name = DBbook.name;
            writer = DBbook.writer;
            subject = DBbook.subject;
            classNum = DBbook.classNum;
        }
        
        public string city { get; set; }
        public string name { get; set; }
        public string writer { get; set; }
        public string subject { get; set; }
        public int classNum { get; set; }

        public DataLayer.Models.Book GetBookDB()
        {
            return new DataLayer.Models.Book() { id = name, City = city, name = name, writer = writer, subject = subject, classNum = classNum };
        }
    }
}
