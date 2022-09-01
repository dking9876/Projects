using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company
{
    class BigBoss : Boss
    {
        private double bonous;
        public BigBoss(string worker_id, string first_name, string last_name, string address, 
            string EmployerId, string department, int MonthlyIncome, double bonous) 
            : base(worker_id, first_name, last_name, address, EmployerId, department, MonthlyIncome)
        {
            this.bonous = bonous;
        }
        public override double CalcSalary()
        {
            return base.CalcSalary() + bonous;
        }
       
    }
}
