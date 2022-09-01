using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    class Worker:employer
    {
        private int salary;
        private int Numhours;
        public Worker(string worker_id, string first_name, string last_name, string address, 
            string EmployerId, string department, int salary, int Numhours) 
            : base(worker_id, first_name, last_name, address, EmployerId, department)
        {
            this.salary = salary;
            this.Numhours = Numhours;
        }
        public override double CalcSalary()
        {
            return salary * Numhours;
        }
    }
}
