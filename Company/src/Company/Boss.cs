namespace Company
{
    class Boss:employer
    {
        private int MonthlyIncome;
        public Boss(string worker_id, string first_name, string last_name, string address, 
                    string EmployerId, string department, int MonthlyIncome) 
                     : base(worker_id, first_name, last_name, address, EmployerId, department)
        {
            this.MonthlyIncome = MonthlyIncome;
        }
        public void SetMonthlyIncome(int MonthlyIncome)
        {
            this.MonthlyIncome = MonthlyIncome;
        }
         public int GetMonthlyIncome()
         {
            return this.MonthlyIncome;
         }
        public override string ToString()
        {
            return base.ToString() + "monthly income: " + MonthlyIncome;
        }
        public override double CalcSalary()
        {
            return MonthlyIncome;
        }
        public string adress()
        {
            return this.GetAddress();
        }

    }
}
