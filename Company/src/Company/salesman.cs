using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    class salesman : employer
    {
        private int sales;
        public salesman(string worker_id, string first_name, string last_name, 
            string address, string EmployerId, string department,int sales)
            :base(worker_id, first_name, last_name, address, EmployerId, department)
        {
            this.sales = sales;
        }
        public void SetSales(int sales)
        {
            this.sales = sales;
        }
        public int GetSales()
        {
            return this.sales;
        }
        public override string ToString()
        {
            return base.ToString() + "sales: "+ sales;
        }
        public override double CalcSalary()
        {
            return (sales * 6.0)/100;
        }
    }
}
